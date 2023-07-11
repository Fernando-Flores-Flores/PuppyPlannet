using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackPuppy.Entity
{
    [Table(name: "razas", Schema = "public")]
    public class razas
    {
        [Key]
        [Column("id_razas")]
        public int IdRaza { get; set; }

        public string descripcion { get; set; }
    }
}
