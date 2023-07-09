using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace BackPuppy.Entity
{
    [Table(name: "roles", Schema = "public")]
        public class Rol
    {
            [Key]
            [Column("id")]
            public int Id { get; set; }

            [Column("id_persona")]
            public int IdPersona { get; set; }

            [Column("id_tipo_operador")]
            public String IdTipoOperador { get; set; }
        public string? api_estado { get; set; }
        public string? api_transaccion { get; set; }
        public DateTime? fecha_cre { get; set; }
        public DateTime? fecha_mod { get; set; }
        public String? usuario_mod { get; set; }

        // Propiedades de navegación para las llaves foráneas
        public persona Persona { get; set; }

            public tipoOperador TipoOperador { get; set; }
        }
}
