using BackPuppy.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class ControlFisicoDtoIn
    {
        public string? temperatura { get; set; }
        public string? frecCardiaca { get; set; }
        public string? frecRespiratoria { get; set; }
        public string? peso { get; set; }

        [Required(ErrorMessage = "El campo Id Mascota es requerido")]
        public int idMascota { get; set; }
    }
}
