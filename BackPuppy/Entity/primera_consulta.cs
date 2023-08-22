using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackPuppy.Entity
{
    [Table(name: "primera_consulta", Schema = "public")]
    public class primera_consulta : ApiEstadosDto
    {
        [Key]
        [Column("id_primera_consulta")]
        public int id_primera_consulta { get; set; }
        public string? condicionCorporal { get; set; }
        public string? habitad { get; set; }
        public string? estadoReproductivo { get; set; }
        public string? detalleConvivencia { get; set; }
        public string? detalleVacunas { get; set; }
        public string? fechaUltimaVacunacion { get; set; }
        public string? detalleDesparacitacion { get; set; }
        public string? fechaUltimadesparacitacion { get; set; }

        [ForeignKey("mascota")]
        public int? id_mascota { get; set; }
        public mascota mascota { get; set; }

    }
}
