using BackPuppy.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class ConsultaMedicaInDto
    {
        public string? motivo_consulta { get; set; }
        public string? diagnostico_consulta { get; set; }
        public string? tratamiento { get; set; }
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "El formato de la fecha de prox. visita es inválido. Debe ser 'yyyy-MM-dd'.")]
        public string? fecha_prox_visita { get; set; }

        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "El formato de la fecha de registro de consulta es inválido. Debe ser 'yyyy-MM-dd'.")]
        public string? fecha_registro_consulta { get; set; }

        [Required(ErrorMessage = "El campo Id id_anamnesis es requerido")]
        public int? id_anamnesis { get; set; }

        [Required(ErrorMessage = "El campo Id control_fisico es requerido")]
        public int? id_control_fisico { get; set; }

        [Required(ErrorMessage = "El campo Id id_mascota es requerido")]
        public int? id_mascota { get; set; }
    }
}
