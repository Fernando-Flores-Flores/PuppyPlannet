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

                    //var listaAnamnecis = new List<AnamnecisDtoOut>();
                    var listaAnam= await context.Anamnecis.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.ananmnecis a WHERE a.id_ananmnecis = {0}", consulta.id_anamnesis).ToListAsync();
                    var anamnecis = new AnamnecisDtoOut();

                    foreach (var item in listaAnam)
                    {
                        anamnecis.id_ananmnecis = item.id_ananmnecis;
                        anamnecis.apetito = item.apetito;
                        anamnecis.agua= item.agua;
                        anamnecis.conducta=item.conducta;
                        anamnecis.defecacion=item.defecacion;
                        anamnecis.alteracionesRes=item.alteracionesRes;
                        anamnecis.alteracionesNeuro=item.alteracionesNeuro;
                        anamnecis.problemasUr=item.problemasUr;
                        //listaAnamnecis.Add(anamnecis);
                    }
                    consulta.datosAnamnecis= anamnecis;
                  

                    //var listaControlFisico = new List<ControlFisicoDtoOut>();
                    var listaControlFis = await context.controlFisico.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.control_fisico a WHERE a.id_control_fisico = {0}", consulta.id_control_fisico).ToListAsync();
                    var controlFis = new ControlFisicoDtoOut();

                    foreach (var item in listaControlFis)
                    {
                        controlFis.id_control_fisico = item.id_control_fisico;
                        controlFis.temperatura = item.temperatura;
                        controlFis.frecCardiaca = item.frecCardiaca;
                        controlFis.frecRespiratoria = item.frecRespiratoria;
                        controlFis.peso = item.peso;
                       
                        //listaControlFisico.Add(controlFis);
                    }
                    consulta.datosControlFisico = controlFis;
                    listaConsultaMedica.Add(consulta);
                }

                historial.listaConsultaMedica = listaConsultaMedica;

                //vacunas
                var vacunas = await context.vacunas
    .FromSqlRaw("select * from vetmypuppyplanet.public.vacunas v where v.id_mascota in({0})", idMascota)
    .ToListAsync();
                var vacunasBase = new VacunaOutDto();
                vacunasBase.id_mascota = idMascota;
                var listaVacunas = new List<VacunaOutDto>();


                foreach (var vacuna in vacunas)
                {
                    var vacuna1 = new VacunaOutDto();
                    vacuna1.id_mascota = vacuna.id_mascota;
                    vacuna1.descripcion_vacuna = vacuna.descripcion_vacuna;
                    vacuna1.laboratorio = vacuna.laboratorio;
                    vacuna1.fecha_vacunacion = vacuna.fecha_vacunacion;
                    vacuna1.fecha_revacunacion = vacuna.fecha_revacunacion;
                    vacuna1.id_vacuna = vacuna.id_vacuna;
                    vacuna1.id_control_fisico = vacuna.id_control_fisico;

                    //var listaControlFisico = new List<ControlFisicoDtoOut>();
                    var listaControlFis = await context.controlFisico.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.control_fisico a WHERE a.id_control_fisico = {0}", vacuna1.id_control_fisico).ToListAsync();

                    var controlFis = new ControlFisicoDtoOut();
                    foreach (var item in listaControlFis)
                    {
                        controlFis.id_control_fisico = item.id_control_fisico;
                        controlFis.temperatura = item.temperatura;
                        controlFis.frecCardiaca = item.frecCardiaca;
                        controlFis.frecRespiratoria = item.frecRespiratoria;
                        controlFis.peso = item.peso;

                        //listaControlFisico.Add(controlFis);
                    }
                    vacuna1.datosControlFisico = controlFis;
                    listaVacunas.Add(vacuna1);
                }
                historial.listaVacunas = listaVacunas;

                //Desparacitaciones
                var desparacitaciones = await context.Desparacitacion
    .FromSqlRaw("select * from vetmypuppyplanet.public.desparacitaciones v where v.id_mascota in({0})", idMascota)
    .ToListAsync();
                var desparacitacionBase = new DesparacitacionOutDto();
                desparacitacionBase.id_mascota = idMascota;
                var listaDesparacitacion = new List<DesparacitacionOutDto>();

                foreach (var desparacitacion in desparacitaciones)
                {
                    var vacuna2 = new DesparacitacionOutDto();
                    vacuna2.id_mascota = desparacitacion.id_mascota;
                    vacuna2.fecha_desparacitacion = desparacitacion.fecha_desparacitacion;
                    vacuna2.fecha_proxima_desparacitacion = desparacitacion.fecha_proxima_desparacitacion;
                    vacuna2.principio_activo = desparacitacion.principio_activo;
                    vacuna2.producto_desparacitacion = desparacitacion.producto_desparacitacion;
                    vacuna2.tipo_desparacitacion = desparacitacion.tipo_desparacitacion;
                    vacuna2.via_desparacitcion = desparacitacion.via_desparacitcion;

                    vacuna2.id_control_fisico = desparacitacion.id_control_fisico;
                    vacuna2.id_desparacitacion = desparacitacion.id_desparacitacion;


                    //var listaControlFisico = new List<ControlFisicoDtoOut>();
                    var listaControlFis = await context.controlFisico.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.control_fisico a WHERE a.id_control_fisico = {0}", vacuna2.id_control_fisico).ToListAsync();
                    var controlFis = new ControlFisicoDtoOut();

                    foreach (var item in listaControlFis)
                    {
                        controlFis.id_control_fisico = item.id_control_fisico;
                        controlFis.temperatura = item.temperatura;
                        controlFis.frecCardiaca = item.frecCardiaca;
                        controlFis.frecRespiratoria = item.frecRespiratoria;
                        controlFis.peso = item.peso;

                        //listaControlFisico.Add(controlFis);
                    }
                    vacuna2.datosControlFisico = controlFis;
                    listaDesparacitacion.Add(vacuna2);
                }
                historial.listaDesparaciones = listaDesparacitacion;

                //Cirugias
                
                var cirugias = await context.Cirugia
    .FromSqlRaw("select * from vetmypuppyplanet.public.cirugia c where c.id_mascota in({0});", idMascota)
    .ToListAsync();
                var cirugiaBase = new CirugiaOutDto();
                cirugiaBase.id_mascota = idMascota;
                var listaCirugias = new List<CirugiaOutDto>();


                foreach (var cirug in cirugias)
                {
                    var cirugia1 = new CirugiaOutDto();
                    cirugia1.id_cirugia = cirug.id_cirugia;
                    cirugia1.id_mascota = cirug.id_mascota;
                    cirugia1.descripcion_cirugia = cirug.descripcion_cirugia;
                    cirugia1.fecha_cirugia = cirug.fecha_cirugia;
                    cirugia1.observaciones_cirugia = cirug.observaciones_cirugia;
                    cirugia1.tipo_cirugia = cirug.tipo_cirugia;
                    cirugia1.id_control_fisico = cirug.id_control_fisico;
                    cirugia1.id_anamnesis = cirug.id_anamnesis;

                    var listaControlFis = await context.controlFisico.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.control_fisico a WHERE a.id_control_fisico = {0}", cirugia1.id_control_fisico).ToListAsync();

                    var controlFis = new ControlFisicoDtoOut();
                    foreach (var item in listaControlFis)
                    {
                        controlFis.id_control_fisico = item.id_control_fisico;
                        controlFis.temperatura = item.temperatura;
                        controlFis.frecCardiaca = item.frecCardiaca;
                        controlFis.frecRespiratoria = item.frecRespiratoria;
                        controlFis.peso = item.peso;
                    }
                    cirugia1.datosControlFisico = controlFis;

                    var listaAnam = await context.Anamnecis.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.ananmnecis a WHERE a.id_ananmnecis = {0}", cirugia1.id_anamnesis).ToListAsync();
                    var anamnecis = new AnamnecisDtoOut();

                    foreach (var item in listaAnam)
                    {
                        anamnecis.id_ananmnecis = item.id_ananmnecis;
                        anamnecis.apetito = item.apetito;
                        anamnecis.agua = item.agua;
                        anamnecis.conducta = item.conducta;
                        anamnecis.defecacion = item.defecacion;
                        anamnecis.alteracionesRes = item.alteracionesRes;
                        anamnecis.alteracionesNeuro = item.alteracionesNeuro;
                        anamnecis.problemasUr = item.problemasUr;
                    }
                    cirugia1.datosAnamnecis = anamnecis;
                    listaCirugias.Add(cirugia1);
                }
                historial.listaCirugias = listaCirugias;

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


        [HttpGet("obtenerVacunas")]
        public async Task<ActionResult<HistorialConsultaMedica>> obtenerVacunasMascota()
        {
            try
            {


                //vacunas
                var listavacunasBD = await context.vacunas
    .FromSqlRaw("select * from vetmypuppyplanet.public.vacunas v").ToListAsync();
              
                var listaVacunasRs = new List<VacunaOutDto>();


                foreach (var vacuna in listavacunasBD)
                {
                    var vacuna1 = new VacunaOutDto();
                    vacuna1.id_mascota = vacuna.id_mascota;
                    vacuna1.descripcion_vacuna = vacuna.descripcion_vacuna;
                    vacuna1.laboratorio = vacuna.laboratorio;
                    vacuna1.fecha_vacunacion = vacuna.fecha_vacunacion;
                    vacuna1.fecha_revacunacion = vacuna.fecha_revacunacion;
                    vacuna1.id_vacuna = vacuna.id_vacuna;
                    vacuna1.id_control_fisico = vacuna.id_control_fisico;

                    var listaControlFisBD = await context.controlFisico.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.control_fisico a WHERE a.id_control_fisico = {0}", vacuna1.id_control_fisico).ToListAsync();

                    var controlFis = new ControlFisicoDtoOut();
                    foreach (var item in listaControlFisBD)
                    {
                        controlFis.id_control_fisico = item.id_control_fisico;
                        controlFis.temperatura = item.temperatura;
                        controlFis.frecCardiaca = item.frecCardiaca;
                        controlFis.frecRespiratoria = item.frecRespiratoria;
                        controlFis.peso = item.peso;

                        //listaControlFisico.Add(controlFis);
                    }
                    vacuna1.datosControlFisico = controlFis;
                    listaVacunasRs.Add(vacuna1);
                }
                            

                var response = new ResponseDto<List<VacunaOutDto>>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = listaVacunasRs
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message);
            }
        }

        [HttpGet("obtenerConsultas")]
        public async Task<ActionResult<HistorialConsultaMedica>> obtenerConsultasMascota()
        {
            try
            {


                //vacunas
                var listaconsultasBD = await context.consultaMedica
    .FromSqlRaw("select * from vetmypuppyplanet.public.consulta_medica v").ToListAsync();

                var listaConsultas = new List<ConsultaMedicaDtoOut>();


                foreach (var consulta1 in listaconsultasBD)
                {
                    var consulta = new ConsultaMedicaDtoOut();
                    consulta.id_mascota = consulta1.id_mascota;
                    consulta.id_consulta_medica = consulta1.id_consulta_medica;
                    consulta.motivo_consulta = consulta1.motivo_consulta;
                    consulta.diagnostico_consulta = consulta1.diagnostico_consulta;
                    consulta.tratamiento = consulta1.tratamiento;
                    consulta.fecha_prox_visita = consulta1.fecha_prox_visita;
                    consulta.fecha_registro_consulta = consulta1.fecha_registro_consulta;
                    consulta.id_anamnesis = consulta1.id_anamnesis;
                    consulta.id_control_fisico = consulta1.id_control_fisico;

                    var listaAnam = await context.Anamnecis.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.ananmnecis a WHERE a.id_ananmnecis = {0}", consulta1.id_anamnesis).ToListAsync();
                    var anamnecis = new AnamnecisDtoOut();

                    foreach (var item in listaAnam)
                    {
                        anamnecis.id_ananmnecis = item.id_ananmnecis;
                        anamnecis.apetito = item.apetito;
                        anamnecis.agua = item.agua;
                        anamnecis.conducta = item.conducta;
                        anamnecis.defecacion = item.defecacion;
                        anamnecis.alteracionesRes = item.alteracionesRes;
                        anamnecis.alteracionesNeuro = item.alteracionesNeuro;
                        anamnecis.problemasUr = item.problemasUr;
                        //listaAnamnecis.Add(anamnecis);
                    }
                    consulta.datosAnamnecis = anamnecis;

                    var listaControlFisBD = await context.controlFisico.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.control_fisico a WHERE a.id_control_fisico = {0}", consulta1.id_control_fisico).ToListAsync();

                    var controlFis = new ControlFisicoDtoOut();
                    foreach (var item in listaControlFisBD)
                    {
                        controlFis.id_control_fisico = item.id_control_fisico;
                        controlFis.temperatura = item.temperatura;
                        controlFis.frecCardiaca = item.frecCardiaca;
                        controlFis.frecRespiratoria = item.frecRespiratoria;
                        controlFis.peso = item.peso;

                        //listaControlFisico.Add(controlFis);
                    }
                    consulta.datosControlFisico = controlFis;
                    listaConsultas.Add(consulta);
                }


                var response = new ResponseDto<List<ConsultaMedicaDtoOut>>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = listaConsultas
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message);
            }
        }

        [HttpGet("obtenerCirugias")]
        public async Task<ActionResult<HistorialConsultaMedica>> obtenerCirugiasMascota()
        {
            try
            {


                //vacunas
                var listacirugiasBD = await context.Cirugia
    .FromSqlRaw("select * from vetmypuppyplanet.public.cirugia v").ToListAsync();

                var listaCirugia= new List<CirugiaOutDto>();


                foreach (var cirug in listaCirugia)
                {
                    var cirugia1 = new CirugiaOutDto();
                    cirugia1.id_cirugia = cirug.id_cirugia;
                    cirugia1.id_mascota = cirug.id_mascota;
                    cirugia1.descripcion_cirugia = cirug.descripcion_cirugia;
                    cirugia1.fecha_cirugia = cirug.fecha_cirugia;
                    cirugia1.observaciones_cirugia = cirug.observaciones_cirugia;
                    cirugia1.tipo_cirugia = cirug.tipo_cirugia;
                    cirugia1.id_control_fisico = cirug.id_control_fisico;
                    cirugia1.id_anamnesis = cirug.id_anamnesis;

                    var listaAnam = await context.Anamnecis.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.ananmnecis a WHERE a.id_ananmnecis = {0}", cirug.id_anamnesis).ToListAsync();
                    var anamnecis = new AnamnecisDtoOut();

                    foreach (var item in listaAnam)
                    {
                        anamnecis.id_ananmnecis = item.id_ananmnecis;
                        anamnecis.apetito = item.apetito;
                        anamnecis.agua = item.agua;
                        anamnecis.conducta = item.conducta;
                        anamnecis.defecacion = item.defecacion;
                        anamnecis.alteracionesRes = item.alteracionesRes;
                        anamnecis.alteracionesNeuro = item.alteracionesNeuro;
                        anamnecis.problemasUr = item.problemasUr;
                        //listaAnamnecis.Add(anamnecis);
                    }
                    cirugia1.datosAnamnecis = anamnecis;

                    var listaControlFisBD = await context.controlFisico.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.control_fisico a WHERE a.id_control_fisico = {0}", cirug.id_control_fisico).ToListAsync();

                    var controlFis = new ControlFisicoDtoOut();
                    foreach (var item in listaControlFisBD)
                    {
                        controlFis.id_control_fisico = item.id_control_fisico;
                        controlFis.temperatura = item.temperatura;
                        controlFis.frecCardiaca = item.frecCardiaca;
                        controlFis.frecRespiratoria = item.frecRespiratoria;
                        controlFis.peso = item.peso;

                        //listaControlFisico.Add(controlFis);
                    }
                    cirugia1.datosControlFisico = controlFis;
                    listaCirugia.Add(cirugia1);
                }


                var response = new ResponseDto<List<CirugiaOutDto>>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = listaCirugia
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message);
            }
        }

        [HttpGet("obtenerDesparasitaciones")]
        public async Task<ActionResult<HistorialConsultaMedica>> obtenerDesparacitacionessMascota()
        {
            try
            {


                //vacunas
                var listaDesBD = await context.Desparacitacion
    .FromSqlRaw("select * from vetmypuppyplanet.public.desparacitaciones v").ToListAsync();

                var listaDespaRs = new List<DesparacitacionOutDto>();


                foreach (var desparacitacion in listaDesBD)
                {
                    var desparacitacion1 = new DesparacitacionOutDto();
                    desparacitacion1.id_mascota = desparacitacion.id_mascota;
                    desparacitacion1.fecha_desparacitacion = desparacitacion.fecha_desparacitacion;
                    desparacitacion1.fecha_proxima_desparacitacion = desparacitacion.fecha_proxima_desparacitacion;
                    desparacitacion1.principio_activo = desparacitacion.principio_activo;
                    desparacitacion1.producto_desparacitacion = desparacitacion.producto_desparacitacion;
                    desparacitacion1.tipo_desparacitacion = desparacitacion.tipo_desparacitacion;
                    desparacitacion1.via_desparacitcion = desparacitacion.via_desparacitcion;

                    desparacitacion1.id_control_fisico = desparacitacion.id_control_fisico;
                    desparacitacion1.id_desparacitacion = desparacitacion.id_desparacitacion;

                    var listaControlFisBD = await context.controlFisico.FromSqlRaw("SELECT * FROM vetmypuppyplanet.public.control_fisico a WHERE a.id_control_fisico = {0}", desparacitacion1.id_control_fisico).ToListAsync();

                    var controlFis = new ControlFisicoDtoOut();
                    foreach (var item in listaControlFisBD)
                    {
                        controlFis.id_control_fisico = item.id_control_fisico;
                        controlFis.temperatura = item.temperatura;
                        controlFis.frecCardiaca = item.frecCardiaca;
                        controlFis.frecRespiratoria = item.frecRespiratoria;
                        controlFis.peso = item.peso;

                        //listaControlFisico.Add(controlFis);
                    }
                    desparacitacion1.datosControlFisico = controlFis;
                    listaDespaRs.Add(desparacitacion1);
                }


                var response = new ResponseDto<List<DesparacitacionOutDto>>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = listaDespaRs
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
