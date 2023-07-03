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
                List<persona> personasFiltradas;
                if (idUsuario != "idUsuario")
                {
                    personasFiltradas = await context.Personas.Where(x => x.idCuentaIdentity == idUsuario).ToListAsync();
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
    }
} 
