using BackPuppy.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class RazasOutDto
    {
       
        public int IdRaza { get; set; }

        public string descripcion { get; set; }

        public especies Especie { get; set; }
    }
}
