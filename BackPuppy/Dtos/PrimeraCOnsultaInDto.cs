using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class PrimeraCOnsultaInDto
    {
        public string? condicionCorporal { get; set; }
        public string? habitad { get; set; }
        public string? estadoReproductivo { get; set; }
        public string? detalleConvivencia { get; set; }
        public string? detalleVacunas { get; set; }
        
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "El formato de la fecha ultima vacunación es inválido. Debe ser 'yyyy-MM-dd'.")]
        public string? fechaUltimaVacunacion { get; set; }
        public string? detalleDesparacitacion { get; set; }

        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "El formato de la fecha Ultinma Desparacitación es inválido. Debe ser 'yyyy-MM-dd'.")]
        public string? fechaUltimadesparacitacion { get; set; }

        [ForeignKey("mascota")]
        public int? id_mascota { get; set; }
    }
}
