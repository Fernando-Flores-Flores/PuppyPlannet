using BackPuppy.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class MascotaDto
    {
        public string color { get; set; }

        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "El formato de la fecha de nacimiento es inválido. Debe ser 'yyyy-MM-dd'.")]
        public string? fecha_nacimiento { get; set; }
        public string? nombreMascota { get; set; }
        public string sexo { get; set; }
        public string? tatuaje { get; set; }
        public string? conducta { get; set; }

        public string? foto { get; set; }
        public int idDueno { get; set; }
        public int idRaza { get; set; }

        //public virtual duenos Dueno { get; set; }
        //public virtual razas Raza { get; set; }

        //public virtual especies Especie { get; set; }
    
    }
}
