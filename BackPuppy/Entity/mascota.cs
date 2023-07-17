using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackPuppy.Entity
{
    [Table(name:"mascota", Schema="public" )]

    public class mascota: ApiEstadosDto
    {
        [Key]
        [Column("id_mascota")]
        public int idMascota { get; set; }

        public string color { get; set; }
        public DateTime? fecha_nacimiento { get; set; }
        public string? nombreMascota { get; set; }
        public string sexo{ get; set; }
        public string? tatuaje { get; set; }
        public string? conducta { get; set; }

        public string? foto{ get; set; }

        [ForeignKey("Dueno")]
        public int idDueno { get; set; }
        
        [ForeignKey("Especie")]
        public int idEspecie { get; set; }


        public virtual duenos Dueno{ get; set; }
        public virtual especies Especie { get; set; }
    }
}
