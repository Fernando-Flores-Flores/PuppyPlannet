using AutoMapper;
using BackEnd2023.Utilitarios;
using BackPuppy.Dtos;
using BackPuppy.Entity;
using BackPuppy.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuenosController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<DuenosController> logger;

        public DuenosController(AplicationDbContext context, IMapper mapper,ILogger<DuenosController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }


        [HttpPost("RegistrarDuenos")]
        public async Task<ActionResult<ResponseDto<DuenosDto>>> RegistrarDuenos([FromBody] DuenosDto persona)
        {
            try
            {
                
                    DateTime localDateTime = DateTime.Now;
                    DateTime utcDateTime = localDateTime.ToUniversalTime();
                    var personaBase = mapper.Map<duenos>(persona);
                
                    personaBase.api_estado = "ELABORADO";
                    personaBase.api_transaccion = "ELABORAR";
                    personaBase.fecha_cre = utcDateTime;
                    personaBase.fecha_mod = utcDateTime;
                    personaBase.usuario_mod = "LocalDBA";

                    context.Add(personaBase);
                    await context.SaveChangesAsync();

                        var response = new ResponseDto<duenos>()
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

        [HttpGet("obtenerDuenos")]
        public async Task<ActionResult<List<duenos>>> obtenerDuenos()
        {
            try
            {
                List<duenos> personasFiltradas = await context.Duenos.ToListAsync();


                var response = new ResponseDto<List<duenos>>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = personasFiltradas
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return NotFound(e.Message);
            }
        }


        [HttpGet("obtenerDuenosMascota")]
        public async Task<ActionResult<DuenoMascotaOutDto>> obtenerDuenosMascota([FromQuery, Required] int idDueno)
        {
            try
            {
                var duenos = await context.Duenos
                             .Where(d => d.IdDuenos == idDueno)
                             .ToListAsync();
                var duenoMascota = new DuenoMascotaOutDto();
                duenoMascota.nombres = duenos[0].nombres;
                duenoMascota.apellidoPaterno = duenos[0].apellidoPaterno;
                duenoMascota.apellidoMaterno = duenos[0].apellidoMaterno;
                duenoMascota.telefono = duenos[0].telefono;
                duenoMascota.correo = duenos[0].correo;
                duenoMascota.direccion = duenos[0].direccion;

                var listaMascotaDb = await context.Mascota
                         .Where(d => d.idDueno == idDueno).Include(m=>m.Raza)
                         .ToListAsync();
                var listaMascotaOutDto = mapper.Map<List<MascotaOutDto>>(listaMascotaDb);
                duenoMascota.listaMascotas = listaMascotaOutDto;

                var response = new ResponseDto<DuenoMascotaOutDto>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = duenoMascota
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message);
            }
        }

        [HttpPut("ActualizarDueno/{idDueno}")]
        public async Task<ActionResult<ResponseDto<DuenosDto>>> ActualizarDueno(int idDueno, [FromBody] duenos duenoActualizado)
        {
            if (idDueno != duenoActualizado.IdDuenos)
            {
                return BadRequest("El Id del Dueño no coincide con el id de la URL");
            }

            try
            {
                var duenoExistente = await context.Duenos.FindAsync(idDueno);

                if (duenoExistente == null)
                {
                    return NotFound(); // Mascota no encontrada
                }

                //DateTime FechaO = (DateTime)duenoActualizado.fe;
                //DateTime FechaOrden = FechaO.ToUniversalTime();
                DateTime localDateTime = DateTime.Now;
                DateTime utcDateTime = localDateTime.ToUniversalTime();
                // Actualizar las propiedades de la mascota existente
                duenoExistente.nombres = duenoActualizado.nombres;
                duenoExistente.apellidoPaterno = duenoActualizado.apellidoPaterno;
                duenoExistente.apellidoMaterno = duenoActualizado.apellidoMaterno;
                duenoExistente.telefono = duenoActualizado.telefono;
                duenoExistente.correo = duenoActualizado.correo;
                duenoExistente.direccion = duenoActualizado.direccion;
                duenoExistente.api_estado = "EDITADO";
                duenoExistente.fecha_mod = utcDateTime;

                await context.SaveChangesAsync();

                var response = new ResponseDto<duenos>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = duenoExistente
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

