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

        public PersonasController(AplicationDbContext context, IConfiguration config,IMapper mapper )
        {
            this.context = context;
            _config = config;
            this.mapper = mapper;
        }

     
        [HttpPost("CrearPersona")]
        public async Task<ActionResult<ResponseDto<PersonaDto>>> CrearPersona([FromBody] PersonaDto persona)
        {
            try
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
            catch (Exception e)
            {
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message);
            }
        }
    }
} 
