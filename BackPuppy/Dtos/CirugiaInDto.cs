using BackPuppy.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackPuppy.Dtos
{
    public class CirugiaInDto
    {
        public string descripcion_cirugia { get; set; }
        public string fecha_cirugia { get; set; }
        public string observaciones_cirugia { get; set; }
        public string tipo_cirugia { get; set; }

        [Required(ErrorMessage = "El campo Id Control Fisico es requerido")]
        public int? id_control_fisico { get; set; }

        [Required(ErrorMessage = "El campo Id Mascota es requerido")]
        public int? id_mascota { get; set; }

        [Required(ErrorMessage = "El campo Id Anamnecis es requerido")]
        public int? id_anamnesis { get; set; }
    }
}
