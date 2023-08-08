using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Entity
{
    [Table(name: "desparacitaciones", Schema = "public")]
    public class desparacitaciones : ApiEstadosDto
    {
        [Key]
        public int id_desparacitacion { get; set; }
        public string? fecha_desparacitacion { get; set; }
        public string? fecha_proxima_desparacitacion { get; set; }
        public string? frec_cardiaca_despara { get; set; }
        public string? frec_respiratoria_despara { get; set; }
        public string? peso_desparactacion { get; set; }
        public string? principio_activo { get; set; }
        public string? producto_desparacitacion { get; set; }
        public string? temperatura_desparacitacion { get; set; }
        public string? tipo_desparacitacion { get; set; }
        public string? via_desparacitcion { get; set; }

        [ForeignKey("mascota")]
        public int? id_mascota { get; set; }
        public mascota mascota { get; set; }

        [ForeignKey("controlFisico")]
        public int? id_control_fisico { get; set; }
        public control_fisico controlFisico { get; set; }


    }
}
