using AutoMapper;
using BackPuppy.Dtos;
using BackPuppy.Entity;
using BackPuppy.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrimeraConsultaController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<PrimeraConsultaController> logger;

        public PrimeraConsultaController(AplicationDbContext context, IMapper mapper, ILogger<PrimeraConsultaController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("RegistrarPrimeraConsulta")]
        public async Task<ActionResult<ResponseDto<primera_consulta>>> RegistrarPrimeraConsulta([FromBody] PrimeraCOnsultaInDto datos)
        {
            try
            {

                DateTime localDateTime = DateTime.Now;
                DateTime utcDateTime = localDateTime.ToUniversalTime();

                DateTime fechaNacimiento = DateTime.Now;
                string format = "yyyy-MM-dd";

                var datosFormulario = mapper.Map<primera_consulta>(datos);
                datosFormulario.id_mascota = datos.id_mascota;
                datosFormulario.api_estado = "ELABORADO";
                datosFormulario.api_transaccion = "ELABORAR";
                datosFormulario.fecha_cre = utcDateTime;
                datosFormulario.fecha_mod = utcDateTime;
                datosFormulario.usuario_mod = "LocalDBA";
                
                context.Add(datosFormulario);
                await context.SaveChangesAsync();

                var response = new ResponseDto<primera_consulta>()
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


        [HttpGet("obtenerPrimeraConsulta")]
        public async Task<ActionResult<primera_consulta>> obtenerPrimeraConsulta([FromQuery, Required] int idMascota)
        {
            try
            {
                var primerValor = await context.PrimeraConsulta.FirstOrDefaultAsync();

                if (primerValor != null)
                {

                    var response = new ResponseDto<primera_consulta>()
                    {
                        statusCode = StatusCodes.Status200OK,
                        fechaConsulta = DateTime.Now,
                        codigoRespuesta = 1001,
                        MensajeRespuesta = "CORRECTO",
                        datos = primerValor
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new ResponseDto<String>()
                    {
                        statusCode = StatusCodes.Status404NotFound,
                        fechaConsulta = DateTime.Now,
                        codigoRespuesta = 1001,
                        MensajeRespuesta = "INCORRECTO",
                        datos = "NO SE ENCONTRO UN REGISTRO ASOCIADO A ESTE ID MASCOTA"
                    };
                    return NotFound(response);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message, mensaje: e.InnerException.ToString());
            }
        }
    }
}
