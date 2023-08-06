using AutoMapper;
using BackPuppy.Dtos;
using BackPuppy.Entity;
using BackPuppy.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [HttpGet("obtenerHistorialMascota")]
        public async Task<ActionResult<HistorialConsultaMedica>> obtenerHistorialMascota([FromQuery, Required] int idMascota)
        {
            try
            {
                var consultaMedica = await context.consultaMedica
     .FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.consulta_medica a WHERE a.id_mascota = {0}", idMascota)
     .ToListAsync();
                var historial = new HistorialConsultaMedica();
                historial.id_mascota = idMascota;
                var listaConsultaMedica = new List<ConsultaMedicaDtoOut>();

                foreach (var medica in consultaMedica)
                {
                    var consulta = new ConsultaMedicaDtoOut();
                    consulta.id_mascota = medica.id_mascota;
                    consulta.id_consulta_medica= medica.id_consulta_medica;
                    consulta.motivo_consulta = medica.motivo_consulta;
                    consulta.diagnostico_consulta=medica.diagnostico_consulta;
                    consulta.tratamiento=medica.tratamiento;
                    consulta.fecha_prox_visita = medica.fecha_prox_visita;
                    consulta.fecha_registro_consulta = medica.fecha_registro_consulta;
                    consulta.id_anamnesis = medica.id_anamnesis;
                    consulta.id_control_fisico = medica.id_control_fisico;

                    var listaAnamnecis = new List<AnamnecisDtoOut>();
                    var listaAnam= await context.Anamnecis.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.ananmnecis a WHERE a.id_ananmnecis = {0}", consulta.id_anamnesis).ToListAsync();

                    foreach (var item in listaAnam)
                    {
                        var anamnecis = new AnamnecisDtoOut();
                        anamnecis.id_ananmnecis = item.id_ananmnecis;
                        anamnecis.apetito = item.apetito;
                        anamnecis.agua= item.agua;
                        anamnecis.conducta=item.conducta;
                        anamnecis.defecacion=item.defecacion;
                        anamnecis.alteracionesRes=item.alteracionesRes;
                        anamnecis.alteracionesNeuro=item.alteracionesNeuro;
                        anamnecis.problemasUr=item.problemasUr;
                        listaAnamnecis.Add(anamnecis);
                    }
                    consulta.listaAnamnecis=listaAnamnecis;
                  

                    var listaControlFisico = new List<ControlFisicoDtoOut>();
                    var listaControlFis = await context.controlFisico.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.control_fisico a WHERE a.id_control_fisico = {0}", consulta.id_control_fisico).ToListAsync();
                    foreach (var item in listaControlFis)
                    {
                        var controlFis = new ControlFisicoDtoOut();
                        controlFis.id_control_fisico = item.id_control_fisico;
                        controlFis.temperatura = item.temperatura;
                        controlFis.frecCardiaca = item.frecCardiaca;
                        controlFis.frecRespiratoria = item.frecRespiratoria;
                        controlFis.peso = item.peso;
                       
                        listaControlFisico.Add(controlFis);
                    }
                    consulta.listaControlFisico = listaControlFisico;
                    listaConsultaMedica.Add(consulta);

                }


                historial.listaConsultaMedica = listaConsultaMedica;

                var response = new ResponseDto<HistorialConsultaMedica>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = historial
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message);
            }
        }

    }
}
