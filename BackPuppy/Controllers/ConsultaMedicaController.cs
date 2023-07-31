using AutoMapper;
using BackPuppy.Dtos;
using BackPuppy.Entity;
using BackPuppy.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BackPuppy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaMedicaController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public ConsultaMedicaController(AplicationDbContext context, IMapper mapper, ILogger<ConsultaMedicaController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("RegistrarAnamnecis")]
        public async Task<ActionResult<ResponseDto<ananmnecis>>> RegistrarAnamnecis([FromBody] AnamnecisDtoIn datos)
        {
            try
            {

                DateTime localDateTime = DateTime.Now;
                DateTime utcDateTime = localDateTime.ToUniversalTime();

                DateTime fechaNacimiento = DateTime.Now;
                string format = "yyyy-MM-dd";
         
                var datosFormulario = mapper.Map<ananmnecis>(datos);
                datosFormulario.id_mascota = datos.idMascota;
                datosFormulario.api_estado = "ELABORADO";
                datosFormulario.api_transaccion = "ELABORAR";
                datosFormulario.fecha_cre = utcDateTime;
                datosFormulario.fecha_mod = utcDateTime;
                datosFormulario.usuario_mod = "LocalDBA";

                context.Add(datosFormulario);
                await context.SaveChangesAsync();

                var response = new ResponseDto<ananmnecis>()
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


        [HttpPost("RegistrarControlFisico")]
        public async Task<ActionResult<ResponseDto<control_fisico>>> RegistrarControlFisico([FromBody] ControlFisicoDtoIn datos)
        {
            try
            {
                DateTime localDateTime = DateTime.Now;
                DateTime utcDateTime = localDateTime.ToUniversalTime();

                DateTime fechaNacimiento = DateTime.Now;
                string format = "yyyy-MM-dd";

                var datosFormulario = mapper.Map<control_fisico>(datos);
                datosFormulario.id_mascota = datos.idMascota;
                datosFormulario.api_estado = "ELABORADO";
                datosFormulario.api_transaccion = "ELABORAR";
                datosFormulario.fecha_cre = utcDateTime;
                datosFormulario.fecha_mod = utcDateTime;
                datosFormulario.usuario_mod = "LocalDBA";

                context.Add(datosFormulario);
                await context.SaveChangesAsync();

                var response = new ResponseDto<control_fisico>()
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

        [HttpPost("RegistrarConsultaMedica")]
        public async Task<ActionResult<ResponseDto<ConsultaMedicaInDto>>> RegistrarConsultaMedica([FromBody] ConsultaMedicaInDto datos)
        {
            try
            {
                DateTime localDateTime = DateTime.Now;
                DateTime utcDateTime = localDateTime.ToUniversalTime();

                DateTime fechaNacimiento = DateTime.Now;
                string format = "yyyy-MM-dd";

                var datosFormulario = mapper.Map<consulta_medica>(datos);
                datosFormulario.id_mascota = datos.id_mascota;
                datosFormulario.id_control_fisico = datos.id_control_fisico;
                datosFormulario.id_anamnesis= datos.id_anamnesis;
                datosFormulario.api_estado = "ELABORADO";
                datosFormulario.api_transaccion = "ELABORAR";
                datosFormulario.fecha_cre = utcDateTime;
                datosFormulario.fecha_mod = utcDateTime;
                datosFormulario.usuario_mod = "LocalDBA";

                context.Add(datosFormulario);
                await context.SaveChangesAsync();

                var response = new ResponseDto<consulta_medica>()
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
