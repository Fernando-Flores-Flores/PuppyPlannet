using AutoMapper;
using BackPuppy.Dtos;
using BackPuppy.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clasificadoresController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<clasificadoresController> logger;

        public clasificadoresController(AplicationDbContext context, IMapper mapper, ILogger<clasificadoresController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }


        // El que hay que mejorar
        [HttpGet("obtenerRazas")]
        public async Task<ActionResult<List<razas>>> obtenerRazas([FromQuery, Required] string descripcionEspecie)
        {
            try
            {
                var especie = await this.context.Especie.Where(e => e.descripcion == descripcionEspecie)
            .ToListAsync();

              var idEspecie = especie[0].IdEspecie;
                var razas  = await this.context.Raza.Where(e => e.IdEspecie == idEspecie)
            .ToListAsync();

                var response = new ResponseDto<List<razas>>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = razas
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return NotFound(e.Message);
            }
        }

        //    [HttpGet("obtenerEspecies")]
        //    public async Task<ActionResult<List<especies>>> obtenerEspecies()
        //    {
        //        try
        //        {
        //            var razas = this.context.Especie.ToList();
        //            var personasFiltradas = razas
        //.GroupBy(p => p.descripcion)
        //.Select(g => g.First())
        //.ToArray();

        //            var response = new ResponseDto<List<especies>>()
        //            {
        //                statusCode = StatusCodes.Status200OK,
        //                fechaConsulta = DateTime.Now,
        //                codigoRespuesta = 1001,
        //                MensajeRespuesta = "CORRECTO",
        //                datos = personasFiltradas
        //            };
        //            return Ok(response);
        //        }
        //        catch (Exception e)
        //        {
        //            logger.LogError(e, e.Message);
        //            return NotFound(e.Message);
        //        }
        //    }

        [HttpGet("obtenerEspecies")]
        public async Task<ActionResult<List<especies>>> obtenerEspecies()
        {
            try
            {
                var especies = await this.context.Especie.ToListAsync();

                var especiesFiltradas = especies
                    .GroupBy(e => e.descripcion)
                    .Select(g => g.First())
                    .ToList();

                var response = new ResponseDto<List<especies>>()
                {
                    statusCode = StatusCodes.Status200OK,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1001,
                    MensajeRespuesta = "CORRECTO",
                    datos = especiesFiltradas
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
