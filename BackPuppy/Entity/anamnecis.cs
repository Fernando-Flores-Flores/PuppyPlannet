using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackPuppy.Entity
{
    [Table(name: "ananmnecis", Schema = "public")]
    public class ananmnecis: ApiEstadosDto
    {
        [Key]
        public int id_ananmnecis { get; set; }
        public string? apetito { get; set; }
        public string? agua { get; set; }
        public string? conducta { get; set; }
        public string? defecacion { get; set; }
        public string? alteracionesRes { get; set; }
        public string? alteracionesNeuro { get; set; }
        public string? problemasUr { get; set; }

        [ForeignKey("mascota")]
        public int id_mascota { get; set; }
        public mascota mascota { get; set; }


    }
}
