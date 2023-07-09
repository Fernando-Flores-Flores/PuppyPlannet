using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackPuppy.Entity
{
    [Table(name: "tipo_operadores", Schema = "public")]
        public class tipoOperador
        {
            [Key]
            [Column("id_tipo_operador")]
            public String Id { get; set; }

            public string? descripcion { get; set; }
        public string? api_estado { get; set; }
        public string? api_transaccion { get; set; }
        public DateTime? fecha_cre { get; set; }
        public DateTime? fecha_mod { get; set; }
        public String? usuario_mod { get; set; }
    }
}
