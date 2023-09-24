using BackPuppy.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class vacunaInDto
    {

        [Required(ErrorMessage = "El campo descripción es requerido")]
        public string descripcion_vacuna { get; set; }

        public string? laboratorio { get; set; }

        [Required(ErrorMessage = "El campo fecha de vacunación es requerido")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "El formato de la fecha de nacimiento es inválido. Debe ser 'yyyy-MM-dd'.")]
        public string fecha_vacunacion { get; set; }

        [Required(ErrorMessage = "El campo fecha de revacunación es requerido")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "El formato de la fecha de nacimiento es inválido. Debe ser 'yyyy-MM-dd'.")]
        public string fecha_revacunacion { get; set; }

        [Required(ErrorMessage = "El campo Id mascota es requerido")]
        public int? id_mascota { get; set; }

        [Required(ErrorMessage = "El campo Id control físico es requerido")]
        public int? id_control_fisico { get; set; }


        public string? precio { get; set; }
    }
}
