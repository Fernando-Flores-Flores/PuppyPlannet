using System.ComponentModel.DataAnnotations.Schema;

namespace BackPuppy.Dtos
{
    public class DuenosDto
    {
        public string nombres { get; set; }
        public string? apellidoPaterno { get; set; }
        public string? apellidoMaterno { get; set; }
        public int? telefono { get; set; }
        public string? correo { get; set; }
        public string? direccion { get; set; }

    }
}
