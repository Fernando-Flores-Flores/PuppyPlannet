using AutoMapper;
using BackEnd2023.Utilitarios;
using BackPuppy.Dtos;
using BackPuppy.Entity;
using BackPuppy.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;
using System.Text.Json;
using System.Text;

namespace BackPuppy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : Controller
    {
        private readonly AplicationDbContext context;
        private readonly IConfiguration _config;
        private readonly IMapper mapper;
        private readonly HttpClient httpClient;
        private readonly ILogger<PersonasController> logger;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "personas";

        public PersonasController(AplicationDbContext context, IConfiguration config,IMapper mapper , HttpClient httpClient, ILogger<PersonasController> logger, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            _config = config;
            this.mapper = mapper;
            this.httpClient = httpClient;
            this.logger = logger;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet("obtenerPersonas")]
        public async Task<ActionResult<List<PersonaDto>>> GetPersonas(string idUsuario = "idUsuario")
        {
            try
            {
                var claims = "CORRECTO";
                List<persona> personasFiltradas= new List<persona>();
                if (idUsuario == "usuarios")
                {
                    List<persona> personas;
                    personas = await context.Personas.ToListAsync();
                    for (int i = 0; i < personas.Count; i++)
                    {
                        if (personas[i].idCuentaIdentity != "a")
                        {
                            personasFiltradas.Add(personas[i]);
                        }
                    }
                }
                else if (idUsuario == "personas")
                {
                    personasFiltradas = await context.Personas.ToListAsync();
                }
                else
                {
                    personasFiltradas = await context.Personas.Where(d => d.idCuentaIdentity == idUsuario).OrderByDescending(p => p.fecha_cre).ToListAsync();

                   claims = await context.UserClaims
    .Where(c => c.UserId == idUsuario).Select(c => c.ClaimValue)
    .FirstOrDefaultAsync();
                }
                var response = new ResponseDto<List<persona>>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = personasFiltradas,
                    base64 = claims
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return NotFound(e.Message);
            }
        }


        [HttpPost("CrearPersona")]
        public async Task<ActionResult<ResponseDto<PersonaDto>>> CrearPersona([FromForm] PersonaDto persona)
        {
            try
            {
                if(persona.idCuentaIdentity != "FRONT")
                {
                    DateTime localDateTime = DateTime.Now;
                    DateTime utcDateTime = localDateTime.ToUniversalTime();
                    var personaBase = mapper.Map<persona>(persona);
                    if (persona.fotografia != null)
                    {
                        personaBase.fotografia = await almacenadorArchivos.GuardarArchivo(contenedor, persona.fotografia);
                    }
                    personaBase.api_estado = "ELABORADO";
                    personaBase.api_transaccion = "ELABORAR";
                    personaBase.fecha_cre = utcDateTime;
                    personaBase.fecha_mod = utcDateTime;
                    personaBase.usuario_mod = "LocalDBA";

                    context.Add(personaBase);
                    await context.SaveChangesAsync();
                    var response = new ResponseDto<persona>()
                    {
                        statusCode = StatusCodes.Status200OK,
                        fechaConsulta = DateTime.Now,
                        codigoRespuesta = 1001,
                        MensajeRespuesta = "CORRECTO",
                        datos = personaBase
                    };

                    return Ok(response);
                }
                else
                {
                    var response = new ResponseDto<String>()
                    {
                        statusCode = StatusCodes.Status400BadRequest,
                        fechaConsulta = DateTime.Now,
                        codigoRespuesta = 1001,
                        MensajeRespuesta = "INCORRECTO",
                        datos = "No se encontro un id de la cuenta, no se registro la persona"
                    };
                    return BadRequest(response);
                }
               
            }
            catch (Exception e)
            {
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message);
            }
        }

        [HttpPut("ActualizarPersona/{idCuentaIdentity}")]
        public async Task<ActionResult<ResponseDto<PersonaDto>>> ActualizarPersona(
            string idCuentaIdentity,
            [FromBody] PersonaDto personaActualizada,
            bool actualizaCredenciales,
            string password = "opcional"
        )
        {
            if (idCuentaIdentity != personaActualizada.idCuentaIdentity)
            {
                return BadRequest("El idCuentaIdentity del Dueño no coincide con el id de la URL");
            }

            try
            {
                var personaExistente = await ObtenerPersonaExistenteAsync(idCuentaIdentity);

                if (personaExistente == null)
                {
                    return NotFound();
                }

                ActualizarDatosPersona(personaExistente, personaActualizada);

                if (actualizaCredenciales)
                {
                    var exitoActualizacionCredenciales = await ActualizarCredencialesAsync(idCuentaIdentity, personaActualizada.correo, password);

                    if (!exitoActualizacionCredenciales)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar credenciales");
                    }
                }

                await context.SaveChangesAsync();

                return Ok(new ResponseDto<string>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = actualizaCredenciales ? "CORRECTO ACTUALIZACIÓN PERSONA Y CREDENCIALES" : "CORRECTO ACTUALIZACIÓN PERSONA",
                    datos = personaExistente.idCuentaIdentity
                });
            }
            catch (Exception e)
            {
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message, mensaje: e.InnerException?.ToString());
            }
        }

        private async Task<persona> ObtenerPersonaExistenteAsync(string idCuentaIdentity)
        {
            return await context.Personas.FirstOrDefaultAsync(p => p.idCuentaIdentity == idCuentaIdentity);
        }

        private void ActualizarDatosPersona(persona personaExistente, PersonaDto personaActualizada)
        {
            DateTime utcDateTime = DateTime.Now.ToUniversalTime();

            personaExistente.carnet = personaActualizada.carnet;
            personaExistente.celular = personaActualizada.celular;
            personaExistente.nombres = personaActualizada.nombres?.ToUpper();
            personaExistente.apellidoPaterno = personaActualizada.apellidoPaterno?.ToUpper();
            personaExistente.apellidoMaterno = personaActualizada.apellidoMaterno?.ToUpper();
            personaExistente.direccion = personaActualizada.direccion?.ToUpper();
            personaExistente.api_estado = "EDITADO";
            personaExistente.fecha_mod = utcDateTime;
            personaExistente.usuario_mod = "OBTENER TOKEN";
        }

        private async Task<bool> ActualizarCredencialesAsync(string idCuentaIdentity, string correo, string password)
        {
            var credenciales = new CredencialesDto()
            {
                email = correo,
                password = password
            };

            var jsonCredenciales = JsonSerializer.Serialize(credenciales);
            var content = new StringContent(jsonCredenciales, Encoding.UTF8, "application/json");

            HttpResponseMessage respuesta = await httpClient.PutAsync($"https://localhost:7101/api/Cuentas/ActualizarCuentaIdentity/{idCuentaIdentity}", content);

            if (respuesta.IsSuccessStatusCode)
            {
                string responseBody = await respuesta.Content.ReadAsStringAsync();
                var responseDto = JsonSerializer.Deserialize<ResponseDto<string>>(responseBody);

                return responseDto.statusCode == 200;
            }

            return false;
        }



        //[HttpPut("ActualizarPersona/{idCuentaIdentity}")]
        //public async Task<ActionResult<ResponseDto<PersonaDto>>> ActualizarDueno(string idCuentaIdentity, [FromBody] PersonaDto personaActualizada, bool actualizaCredenciales
        //    ,string password = "opcional")
        //{
        //    if (idCuentaIdentity != personaActualizada.idCuentaIdentity)
        //    {
        //        return BadRequest("El idCuentaIdentity del Dueño no coincide con el id de la URL");
        //    }
        //    try
        //    {
        //        var personaExistente = await context.Personas.FirstOrDefaultAsync(p=> p.idCuentaIdentity== idCuentaIdentity);

        //        if (personaExistente == null)
        //        {
        //            return NotFound();
        //        }

        //        DateTime localDateTime = DateTime.Now;
        //        DateTime utcDateTime = localDateTime.ToUniversalTime();

        //        personaExistente.carnet = personaActualizada.carnet;
        //        personaExistente.celular = personaActualizada.celular;
        //        personaExistente.nombres = personaActualizada.nombres?.ToUpper();
        //        personaExistente.apellidoPaterno = personaActualizada.apellidoPaterno?.ToUpper();
        //        personaExistente.apellidoMaterno = personaActualizada.apellidoMaterno?.ToUpper();
        //        personaExistente.direccion = personaActualizada.direccion?.ToUpper();
        //        personaExistente.api_estado = "EDITADO";
        //        personaExistente.fecha_mod = utcDateTime;
        //        personaExistente.usuario_mod = "OBTENER TOKEN";
        //        if (actualizaCredenciales)
        //        {
        //            personaExistente.correo = personaActualizada.correo;
        //            var credenciales = new CredencialesDto()
        //            {
        //                email = personaActualizada.correo,
        //                password = password
        //            };
        //            var jsonCredenciales = JsonSerializer.Serialize(credenciales);
        //            var content = new StringContent(jsonCredenciales, Encoding.UTF8, "application/json");
        //            HttpResponseMessage respuesta = await httpClient.PutAsync("https://localhost:7101/api/Cuentas/ActualizarCuentaIdentity/"+ idCuentaIdentity, content);
        //            Console.WriteLine(respuesta);
        //            if (respuesta.IsSuccessStatusCode)
        //            {
        //                string responseBody = await respuesta.Content.ReadAsStringAsync();
        //                var responseDto = JsonSerializer.Deserialize<ResponseDto<String>>(responseBody);

        //                if (responseDto.statusCode == 200)
        //                {
        //                    await context.SaveChangesAsync();

        //                    var response1 = new ResponseDto<string>()
        //                    {
        //                        statusCode = StatusCodes.Status200OK,
        //                        fechaConsulta = DateTime.Now,
        //                        codigoRespuesta = 1001,
        //                        MensajeRespuesta = "CORRECTO ACTUALIZCIÖN PERSONA Y CREDENCIALES",
        //                        datos = personaExistente.idCuentaIdentity
        //                    };

        //                    return Ok(response1);
        //                }
        //            }
        //        }

        //        await context.SaveChangesAsync();

        //        var response = new ResponseDto<string>()
        //        {
        //            statusCode = StatusCodes.Status200OK,
        //            fechaConsulta = DateTime.Now,
        //            codigoRespuesta = 1001,
        //            MensajeRespuesta = "CORRECTO ACTUALIZCIÖN PERSONA",
        //            datos = personaExistente.idCuentaIdentity
        //        };

        //        return Ok(response);
        //    }
        //    catch (Exception e)
        //    {
        //        return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message, mensaje: e.InnerException.ToString());
        //    }
        //}

    }
} 
