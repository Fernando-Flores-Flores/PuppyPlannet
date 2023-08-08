using AutoMapper;
using BackPuppy.Dtos;
using BackPuppy.Entity;
using BackPuppy.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackPuppy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesparacitacionController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public DesparacitacionController(AplicationDbContext context, IMapper mapper, ILogger<DesparacitacionController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("RegistrarDesparacitacion")]
        public async Task<ActionResult<ResponseDto<desparacitaciones>>> RegistrarDesparacitacion([FromBody] DesparacitacionesInDto datos)
        {
            try
            {

                DateTime localDateTime = DateTime.Now;
                DateTime utcDateTime = localDateTime.ToUniversalTime();

                DateTime fechaNacimiento = DateTime.Now;
                string format = "yyyy-MM-dd";

                var datosFormulario = mapper.Map<desparacitaciones>(datos);
                datosFormulario.api_estado = "ELABORADO";
                datosFormulario.api_transaccion = "ELABORAR";
                datosFormulario.fecha_cre = utcDateTime;
                datosFormulario.fecha_mod = utcDateTime;
                datosFormulario.usuario_mod = "LocalDBA";

                context.Add(datosFormulario);
                await context.SaveChangesAsync();

                var response = new ResponseDto<desparacitaciones>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = datosFormulario
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message, mensaje: e.InnerException.ToString());
            }
        }
    }
}
