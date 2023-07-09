using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Entity
{
    [Table(name: "duenos", Schema = "public")]
    public class duenos
    {
        [Key]
        [Column("id")]
        public int IdDuenos { get; set; }

        public string nombres { get; set; }

        [Column("apellido_paterno")]
        public string? apellidoPaterno { get; set; }
        [Column("apellido_materno")]
        public string? apellidoMaterno { get; set; }
        public int? telefono { get; set; }
        public string? correo { get; set; }
        public string? direccion { get; set; }
      

        public string? api_estado { get; set; }
        public string? api_transaccion { get; set; }
        public DateTime? fecha_cre { get; set; }
        public DateTime? fecha_mod { get; set; }
        public String? usuario_mod { get; set; }
     
    }


}
