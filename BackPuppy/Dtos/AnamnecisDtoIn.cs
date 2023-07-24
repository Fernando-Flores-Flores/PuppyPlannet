using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class AnamnecisDtoIn
    {
        public string? apetito { get; set; }
        public string? agua { get; set; }
        public string? conducta { get; set; }
        public string? defecacion { get; set; }
        public string? alteracionesRes { get; set; }
        public string? alteracionesNeuro { get; set; }
        public string? problemasUr { get; set; }
        [Required(ErrorMessage = "El campo Id Mascota es requerido")]
        public int idMascota { get; set; }
    }
}
