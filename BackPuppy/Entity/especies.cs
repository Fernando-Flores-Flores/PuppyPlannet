using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Entity
{
    [Table(name: "especies", Schema = "public")]
    public class especies
    {
        [Key]
        [Column("id_especie")]
        public int IdEspecie { get; set; }
        [StringLength(30,ErrorMessage = "El campo descripción no puede tener más de 30 caracteres.")]
        public string descripcion { get; set; }

  
    }
}
