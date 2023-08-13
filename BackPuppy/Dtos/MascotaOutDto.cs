using BackPuppy.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class MascotaOutDto
    {
        public int idMascota { get; set; }
        public string color { get; set; }
        public string? fecha_nacimiento { get; set; }
        public string? nombreMascota { get; set; }
        public string sexo { get; set; }
        public string? tatuaje { get; set; }
        public string? conducta { get; set; }

        public string? foto { get; set; }

        public int idDueno { get; set; }
        public int idEspecie { get; set; }

        //public virtual duenos Dueno { get; set; }
        public virtual razas Raza { get; set; }

        //public virtual especies Especie { get; set; }
    
    }
}
