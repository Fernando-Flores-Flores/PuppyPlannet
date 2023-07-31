using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class ConsultaMedicaDtoOut
    {
        public int id_consulta_medica { get; set; }
        public string? motivo_consulta { get; set; }
        public string? diagnostico_consulta { get; set; }
        public string? tratamiento { get; set; }
        public string? fecha_prox_visita { get; set; }
        public string? fecha_registro_consulta { get; set; }
        public int? id_anamnesis { get; set; }
        public int? id_control_fisico { get; set; }
        public int? id_mascota { get; set; }
    }
}
