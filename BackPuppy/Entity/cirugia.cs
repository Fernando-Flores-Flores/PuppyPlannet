using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackPuppy.Entity
{
    [Table(name: "cirugia", Schema = "public")]
    public class cirugia: ApiEstadosDto
    {
        [Key]
        [Column("id_cirugia")]
        public int id_cirugia { get; set; }
        public string descripcion_cirugia {get; set;}
        public string fecha_cirugia {get; set;}
        public string observaciones_cirugia {get; set;}
        public string tipo_cirugia {get; set;}
     
        [ForeignKey("controlFisico")]
        public int? id_control_fisico { get; set; }
        public control_fisico controlFisico { get; set; }
       
        [ForeignKey("mascota")]
        public int? id_mascota { get; set; }
        public mascota mascota { get; set; }

        [ForeignKey("ananmnecis")]
        public int? id_anamnesis { get; set; }
        public ananmnecis ananmnecis { get; set; }

        public string? precio { get; set; }
    }
}
