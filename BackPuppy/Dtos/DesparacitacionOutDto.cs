using BackPuppy.Entity;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class DesparacitacionOutDto
    {
        public int id_desparacitacion { get; set; }
        public string? fecha_desparacitacion { get; set; }
        public string? fecha_proxima_desparacitacion { get; set; }
        public string? principio_activo { get; set; }
        public string? producto_desparacitacion { get; set; }
        public string? tipo_desparacitacion { get; set; }
        public string? via_desparacitcion { get; set; }
        public int? id_mascota { get; set; }
        public int? id_control_fisico { get; set; } 
        public ControlFisicoDtoOut datosControlFisico { get; set; }
    }
}
