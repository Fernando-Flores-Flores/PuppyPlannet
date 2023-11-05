using AutoMapper;
using BackPuppy.Dtos;
using BackPuppy.Entity;
using BackPuppy.Facade;
using BackPuppy.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace BackPuppy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public MascotaController(AplicationDbContext context,IMapper mapper, ILogger<MascotaController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("RegistrarMascota")]
        public async Task<ActionResult<ResponseDto<DuenosDto>>> RegistrarMascota([FromBody] MascotaDto mascota)
        {
            try
            {
                mascotaFacade mascotaFacade = new mascotaFacade();
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
                int nuevoId = mascotaBase.idMascota;

                var dueno = await context.Duenos.FirstOrDefaultAsync(m => m.IdDuenos == mascota.idDueno);

                string codigoGenerado;

                if (dueno != null)
                {
                    codigoGenerado = mascotaFacade.generarCodigoMascota(mascota, dueno.nombres, nuevoId);
                }
                else
                {
                    codigoGenerado = "SN";
                }

                mascotaBase.cod_mascota = codigoGenerado;
                context.Update(mascotaBase); 
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


        [HttpGet("obtenerMascotas")]
        public async Task<ActionResult<List<mascota>>> obtenerMascotas()
        {
            try
            {
                //List<mascota> mascotasFiltradas = await context.Mascota.ToListAsync();
                //List<mascota> mascotasFiltradas = await context.Mascota.Include(m => m.Dueno).Include(x=>x.Raza).ToListAsync();
                List<mascota> mascotasFiltradas = await context.Mascota
                    .Where(m => m.api_estado != "ELIMINADO")
                    .Include(m => m.Dueno)
                    .Include(m => m.Raza)
                    .ToListAsync();

                var response = new ResponseDto<List<mascota>>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = mascotasFiltradas
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message, mensaje: e.InnerException.ToString());
            }
        }

        [HttpPut("ActualizarMascota/{idMascota}")]
        public async Task<ActionResult<ResponseDto<MascotaDto>>> ActualizarMascota(int idMascota, [FromBody] MascotaDto mascotaActualizada)
        {
            //if (idMascota != mascotaActualizada.idMascota)
            //{
            //    return BadRequest("El Id del autor no coincide con el id de la URL");
            //}

            try
            {
                var mascotaExistente = await context.Mascota.FindAsync(idMascota);

                if (mascotaExistente == null)
                {
                    return NotFound(); // Mascota no encontrada
                }

                //DateTime FechaO = (DateTime)mascotaActualizada.fecha_nacimiento;
                string fechaTexto = mascotaActualizada.fecha_nacimiento;
                DateTime FechaO = DateTime.ParseExact(fechaTexto, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                DateTime FechaOrden = FechaO.ToUniversalTime();
                DateTime localDateTime = DateTime.Now;
                DateTime utcDateTime = localDateTime.ToUniversalTime();
                // Actualizar las propiedades de la mascota existente
                mascotaExistente.fecha_nacimiento = FechaOrden;
                mascotaExistente.foto = mascotaActualizada.foto;
                mascotaExistente.color = mascotaActualizada.color;
                mascotaExistente.nombreMascota = mascotaActualizada.nombreMascota;
                mascotaExistente.sexo = mascotaActualizada.sexo;
                mascotaExistente.tatuaje = mascotaActualizada.tatuaje;
                mascotaExistente.conducta = mascotaActualizada.conducta;
                //mascotaExistente.idDueno = mascotaActualizada.idDueno;
                //mascotaExistente.idRaza = mascotaActualizada.idRaza;
                mascotaExistente.api_estado = "EDITADO";
                mascotaExistente.fecha_mod = utcDateTime;

                await context.SaveChangesAsync();

                var response = new ResponseDto<mascota>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = mascotaExistente
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message, mensaje: e.InnerException.ToString());
            }
        }
       
        [HttpDelete("EliminarMascota/{mascotaId}")]
        public async Task<ActionResult<ResponseDto<int>>> EliminarMascota(int mascotaId)
        {
            try
            {
                // Buscar la mascota por su ID en la base de datos
                var mascotaBase = await context.Mascota.FindAsync(mascotaId);

                if (mascotaBase == null)
                {
                    return NotFound("No se encontro a la mascota a eliminar"); // Puedes personalizar el mensaje de acuerdo a tus necesidades
                }

                // Eliminar la mascota de la base de datos

                mascotaBase.api_estado = "ELIMINADO";
                mascotaBase.api_transaccion = "ELIMINAR";
                context.Mascota.Update(mascotaBase);

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
