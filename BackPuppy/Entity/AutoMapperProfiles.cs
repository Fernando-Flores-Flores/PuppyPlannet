using AutoMapper;
using BackPuppy.Dtos;

namespace BackPuppy.Entity
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PersonaDto, persona>();
            CreateMap<DuenosDto, duenos>();
            CreateMap<MascotaDto, mascota>();
            CreateMap<mascota, MascotaOutDto>();
            CreateMap<AnamnecisDtoIn, ananmnecis>();
            CreateMap<ControlFisicoDtoIn, control_fisico>();
            CreateMap<ConsultaMedicaInDto, consulta_medica>();
            CreateMap<vacunaInDto, vacunas>();
            CreateMap<DesparacitacionesInDto, desparacitaciones>();
            CreateMap<PrimeraCOnsultaInDto, primera_consulta>();
            CreateMap<CirugiaInDto, cirugia>();


        }
    }
}
