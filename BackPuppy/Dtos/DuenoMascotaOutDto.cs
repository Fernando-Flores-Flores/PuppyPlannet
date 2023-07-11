using BackPuppy.Entity;

namespace BackPuppy.Dtos
{
    public class DuenoMascotaOutDto
    {
        public string nombres { get; set; }
        public string? apellidoPaterno { get; set; }
        public string? apellidoMaterno { get; set; }
        public int? telefono { get; set; }
        public string? correo { get; set; }
        public string? direccion { get; set; }
        public virtual List<MascotaOutDto>  listaMascotas { get; set; }
    }
}
