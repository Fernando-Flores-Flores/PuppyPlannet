using AutoMapper;
using BackPuppy.Dtos;
using BackPuppy.Entity;
using BackPuppy.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public PersonasController(AplicationDbContext context, IConfiguration config,IMapper mapper , HttpClient httpClient)
        {
            this.context = context;
            _config = config;
            this.mapper = mapper;
            this.httpClient = httpClient;
        }

     
        [HttpPost("CrearPersona")]
        public async Task<ActionResult<ResponseDto<PersonaDto>>> CrearPersona([FromBody] PersonaDto persona)
        {
            try
            {
                if(persona.idCuentaIdentity != "FRONT")
                {
                    DateTime localDateTime = DateTime.Now;
                    DateTime utcDateTime = localDateTime.ToUniversalTime();
                    var personaBase = mapper.Map<persona>(persona);
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
