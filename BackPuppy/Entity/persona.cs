using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Entity
{
    [Table(name: "personas", Schema = "public")]
    public class persona
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("carnet")]
        public string carnet { get; set; }
        public string nombres { get; set; }

        [Column("apellido_paterno")]
        public string? apellidoPaterno { get; set; }
        [Column("apellido_materno")]
        public string? apellidoMaterno { get; set; }
        public int? celular { get; set; }
        public string? correo { get; set; }
        public string? direccion { get; set; }

        public string? api_estado { get; set; }
        public string? api_transaccion { get; set; }
        public DateTime? fecha_cre { get; set; }
        public DateTime? fecha_mod { get; set; }
        public DateTime? usuario_mod { get; set; }


    }


}
