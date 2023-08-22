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
    public class CirugiaController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<CirugiaController> logger;

        public CirugiaController(AplicationDbContext context, IMapper mapper, ILogger<CirugiaController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("RegistrarCirugia")]
        public async Task<ActionResult<ResponseDto<cirugia>>> RegistrarCirugia([FromBody] CirugiaInDto datos)
        {
            try
            {

                DateTime localDateTime = DateTime.Now;
                DateTime utcDateTime = localDateTime.ToUniversalTime();

                DateTime fechaNacimiento = DateTime.Now;
                string format = "yyyy-MM-dd";

                var datosFormulario = mapper.Map<cirugia>(datos);
                datosFormulario.id_mascota = datos.id_mascota;
                datosFormulario.api_estado = "ELABORADO";
                datosFormulario.api_transaccion = "ELABORAR";
                datosFormulario.fecha_cre = utcDateTime;
                datosFormulario.fecha_mod = utcDateTime;
                datosFormulario.usuario_mod = "LocalDBA";

                context.Add(datosFormulario);
                await context.SaveChangesAsync();

                var response = new ResponseDto<cirugia>()
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
