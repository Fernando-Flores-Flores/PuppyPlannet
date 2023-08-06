using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackPuppy.Entity
{
    [Table("razas", Schema = "public")]
    public class razas
    {
        [Key]
        [Column("id_razas")]
        public int IdRaza { get; set; }

        public string descripcion { get; set; }

        [ForeignKey("id_especie")]
        public int IdEspecie { get; set; }

        //public especies Especie { get; set; }
    }

}
