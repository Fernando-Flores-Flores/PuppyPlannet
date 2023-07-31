using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackPuppy.Entity
{
    [Table(name: "vacunas", Schema = "public")]
    public class vacunas : ApiEstadosDto
    {
        [Key]
        public int id_vacuna { get; set; }

        [Required]
        public string descripcion_vacuna { get; set; }

        public string? laboratorio{ get; set; }

        [Required]
        public string fecha_vacunacion { get; set; }

        [Required]
        public string fecha_revacunacion{ get; set; }

        [ForeignKey("mascota")]
        public int? id_mascota { get; set; }
        public mascota mascota { get; set; }

        [ForeignKey("controlFisico")]
        public int? id_control_fisico { get; set; }
        public control_fisico controlFisico { get; set; }
    }
}
