using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class PersonaDto
    {
        [Required]
        public String carnet { get; set; }
        [Required]
        public String nombres { get; set; }
        public String? apellidoPaterno { get; set; }
        public String? apellidoMaterno { get; set; }

        [Required]
        public int celular { get; set; }
        [Required]
        public String correo { get; set; }
        public String? direccion { get; set; }
        [Required]
        public String idCuentaIdentity { get; set; }

        public IFormFile? fotografia { get; set; }
    }
}
