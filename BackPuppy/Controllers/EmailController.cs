using BackPuppy.Dtos;
using BackPuppy.Entity;
using BackPuppy.Facade;
using BackPuppy.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackPuppy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("envioCorreo")]
        public async Task<ActionResult<ResponseDto<DuenosDto>>> SendEmail([FromBody] EmailRequestModel emailRequest)
        {
            try
            {
                await _emailService.SendEmailAsync(emailRequest.correoDestinatrio, emailRequest.asunto, emailRequest.cuerpo);

                var response = new ResponseDto<String>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = "Correo enviado exitosamente."
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
