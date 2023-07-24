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
        public string? fecha_prox_visita { get; set; }
        public string? fecha_registro_consulta { get; set; }

        [Required(ErrorMessage = "El campo Id id_anamnesis es requerido")]
        public int? id_anamnesis { get; set; }

        [Required(ErrorMessage = "El campo Id control_fisico es requerido")]
        public int? control_fisico { get; set; }

        [Required(ErrorMessage = "El campo Id id_mascota es requerido")]
        public int? id_mascota { get; set; }
    }
}
