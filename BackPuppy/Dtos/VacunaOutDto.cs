using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class VacunaOutDto
    {
        public int id_vacuna { get; set; }
        public string descripcion_vacuna { get; set; }

        public string? laboratorio { get; set; }
        public string fecha_vacunacion { get; set; }
        public string fecha_revacunacion { get; set; }
        public int? id_mascota { get; set; }
        public int? id_control_fisico { get; set; }
        public ControlFisicoDtoOut datosControlFisico { get; set; }
    }
}
