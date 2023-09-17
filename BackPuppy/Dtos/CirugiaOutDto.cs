using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class CirugiaOutDto
    {
        public int? id_cirugia { get; set; }
        public string descripcion_cirugia { get; set; }
        public string fecha_cirugia { get; set; }
        public string observaciones_cirugia { get; set; }
        public string tipo_cirugia { get; set; }
        public int? id_control_fisico { get; set; }
        public int? id_mascota { get; set; }
        public int? id_anamnesis { get; set; }
        public ControlFisicoDtoOut datosControlFisico { get; set; }
        public AnamnecisDtoOut datosAnamnecis { get; set; }


    }
}
