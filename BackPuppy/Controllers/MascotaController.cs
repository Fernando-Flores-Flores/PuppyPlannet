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
    public class MascotaController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;

        public MascotaController(AplicationDbContext context,IMapper mapper )
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost("RegistrarMascota")]
        public async Task<ActionResult<ResponseDto<DuenosDto>>> RegistrarMascota([FromBody] MascotaDto mascota)
        {
            try { 

                DateTime localDateTime = DateTime.Now;
                DateTime utcDateTime = localDateTime.ToUniversalTime();

                DateTime fechaNacimiento = DateTime.Now;
                string format = "yyyy-MM-dd";
                if (mascota.fecha_nacimiento != null)
                {
                    fechaNacimiento = DateTime.ParseExact(mascota.fecha_nacimiento, format, CultureInfo.InvariantCulture);
                    DateTime fecha = fechaNacimiento;
                   fechaNacimiento = fecha.ToUniversalTime();
                }
                var mascotaBase = mapper.Map<mascota>(mascota);
                mascotaBase.fecha_nacimiento = fechaNacimiento;
                mascotaBase.api_estado = "ELABORADO";
                mascotaBase.api_transaccion = "ELABORAR";
                mascotaBase.fecha_cre = utcDateTime;
                mascotaBase.fecha_mod = utcDateTime;
                mascotaBase.usuario_mod = "LocalDBA";

                context.Add(mascotaBase);
                await context.SaveChangesAsync();

                var response = new ResponseDto<mascota>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = mascotaBase
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
