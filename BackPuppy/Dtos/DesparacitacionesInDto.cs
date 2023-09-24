using BackPuppy.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class DesparacitacionesInDto
    {
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "El formato de la fecha desparacitacion es inválido. Debe ser 'yyyy-MM-dd'.")]
        public string? fecha_desparacitacion { get; set; }
        
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "El formato de la fecha proxima desparacitacion es inválido. Debe ser 'yyyy-MM-dd'.")]
        public string? fecha_proxima_desparacitacion { get; set; }
        public string? principio_activo { get; set; }
        public string? producto_desparacitacion { get; set; }
        public string? tipo_desparacitacion { get; set; }
        public string? via_desparacitcion { get; set; }

        [Required(ErrorMessage = "El campo Id mascota es requerido")]
        public int? id_mascota { get; set; }

        [Required(ErrorMessage = "El campo Id control físico es requerido")]
        public int? id_control_fisico { get; set; }
        public string? precio { get; set; }
    }
}
