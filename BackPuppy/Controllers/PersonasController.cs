using AutoMapper;
using BackPuppy.Dtos;
using BackPuppy.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;

namespace BackPuppy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : Controller
    {
        private readonly AplicationDbContext context;
        private readonly IConfiguration _config;
        private readonly IMapper mapper;

        public PersonasController(AplicationDbContext context, IConfiguration config,IMapper mapper )
        {
            this.context = context;
            _config = config;
            this.mapper = mapper;
        }

     
        [HttpPost("CrearPersona")]
        public async Task<ActionResult<PersonaDto>> CrearPersona([FromBody] PersonaDto persona)
        {
            try
            {
                var personaBase = mapper.Map<persona>(persona);

                context.Add(personaBase);
              await context.SaveChangesAsync();
                return Ok(personaBase);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
