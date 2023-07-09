using AutoMapper;
using BackEnd2023.Utilitarios;
using BackPuppy.Dtos;
using BackPuppy.Entity;
using BackPuppy.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace BackPuppy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuenosController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<PersonasController> logger;

        public DuenosController(AplicationDbContext context, IMapper mapper,ILogger<PersonasController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }


        [HttpPost("RegistrarDuenos")]
        public async Task<ActionResult<ResponseDto<PersonaDto>>> RegistrarDuenos([FromBody] PersonaDto persona)
        {
            try
            {
                if (persona.idCuentaIdentity != "FRONT")
                {
                    DateTime localDateTime = DateTime.Now;
                    DateTime utcDateTime = localDateTime.ToUniversalTime();
                    var personaBase = mapper.Map<persona>(persona);

                    personaBase.idCuentaIdentity = "D-" + persona.carnet;
                    personaBase.api_estado = "ELABORADO";
                    personaBase.api_transaccion = "ELABORAR";
                    personaBase.fecha_cre = utcDateTime;
                    personaBase.fecha_mod = utcDateTime;
                    personaBase.usuario_mod = "LocalDBA";


                    context.Add(personaBase);
                    await context.SaveChangesAsync();

                    int idGenerado = personaBase.Id;
                    var rol = new Rol();
                    rol.IdPersona = idGenerado;
                    rol.IdTipoOperador = "D";
                    rol.api_estado = "ELABORADO";
                    rol.api_transaccion = "ELABORAR";
                    rol.fecha_cre = utcDateTime;
                    rol.fecha_mod = utcDateTime;
                    rol.usuario_mod = "LocalDBA";
                    context.Add(rol);
                    await context.SaveChangesAsync();

                    //long guardarRol = await this.GuardarRolUsuario(rol);
                    //if (guardarRol == 0)
                    //{
                        var response = new ResponseDto<persona>()
                        {
                            statusCode = StatusCodes.Status200OK,
                            fechaConsulta = DateTime.Now,
                            codigoRespuesta = 1001,
                            MensajeRespuesta = "CORRECTO",
                            datos = personaBase
                        };
                        return Ok(response);
                    //}
                    //else
                    //{
                    //    var response = new ResponseDto<String>()
                    //    {
                    //        statusCode = StatusCodes.Status400BadRequest,
                    //        fechaConsulta = DateTime.Now,
                    //        codigoRespuesta = 1001,
                    //        MensajeRespuesta = "INCORRECTO",
                    //        datos = "No se registro el id Rol en base de datos"
                    //    };
                    //    return BadRequest(response);
                    //}
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

        [HttpGet("obtenerPersonaPorTipo")]
        public async Task<ActionResult<List<PersonaDto>>> obtenerPersonaPorTipo(string idOperador = "idOperador")
        {
            try
            {
                //List<persona> personasFiltradas;
                //if (idOperador != "idOperador")
                //{
                //    var idUsuario = await context.Roles.Where(x => x.IdTipoOperador == idOperador).ToListAsync();
                //    foreach (var id in idUsuario)
                //    {
                //        Console.WriteLine("Valor de idUsuario: " + id);
                //        personasFiltradas = await context.Personas.Where(x => x.Id == id.Id).ToListAsync();

                //    }
                //}
                List<persona> personasFiltradas = new List<persona>();
                if (idOperador != "idOperador")
                {
                    var idUsuario = await context.Roles.Where(x => x.IdTipoOperador == idOperador).ToListAsync();
                    foreach (var id in idUsuario)
                    {
                        Console.WriteLine("Valor de idUsuario: " + id);
                        var personas = await context.Personas.Where(x => x.Id == id.Id).ToListAsync();
                        personasFiltradas.AddRange(personas);
                    }
                }

                else
                {
                    personasFiltradas = await context.Personas.ToListAsync();
                }

                var response = new ResponseDto<List<persona>>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = personasFiltradas
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return NotFound(e.Message);
            }
        }



    }
}

