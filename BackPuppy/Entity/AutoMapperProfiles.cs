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

        }
    }
}
