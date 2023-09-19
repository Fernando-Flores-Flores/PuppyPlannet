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
    public class VacunaController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public VacunaController(AplicationDbContext context, IMapper mapper, ILogger<VacunaController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("RegistrarVacuna")]
        public async Task<ActionResult<ResponseDto<vacunas>>> RegistrarVacuna([FromBody] vacunaInDto datos)
        {
            try
            {
                DateTime localDateTime = DateTime.Now;
                DateTime utcDateTime = localDateTime.ToUniversalTime();

                DateTime fechaNacimiento = DateTime.Now;
                string format = "yyyy-MM-dd";

                var datosFormulario = mapper.Map<vacunas>(datos);
                datosFormulario.id_mascota = datos.id_mascota;
                datosFormulario.api_estado = "ELABORADO";
                datosFormulario.api_transaccion = "ELABORAR";
                datosFormulario.fecha_cre = utcDateTime;
                datosFormulario.fecha_mod = utcDateTime;
                datosFormulario.usuario_mod = "LocalDBA";

                context.Add(datosFormulario);
                await context.SaveChangesAsync();

                var response = new ResponseDto<vacunas>()
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

        [HttpDelete("EliminarVacuna/{mascotaId}")]
        public async Task<ActionResult<ResponseDto<int>>> EliminarVacuna([FromQuery]int mascotaId , [FromQuery] int vacunaId)
        {
            try
            {
                // Buscar la mascota por su ID en la base de datos
                var mascotaBase = await context.vacunas.FindAsync(mascotaId);

                if (mascotaBase == null)
                {
                    return NotFound("No se encontro a la mascota a eliminar"); // Puedes personalizar el mensaje de acuerdo a tus necesidades
                }

                // Eliminar la mascota de la base de datos

                mascotaBase.api_estado = "ELIMINADO";
                mascotaBase.api_transaccion = "ELIMINAR";
                context.vacunas.Update(mascotaBase);

                await context.SaveChangesAsync();

                var response = new ResponseDto<int>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1, // Código de respuesta para eliminación exitosa (puedes personalizarlo)
                    MensajeRespuesta = "Mascota eliminada exitosamente",
                    datos = mascotaId // Devuelve el ID de la mascota eliminada
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
