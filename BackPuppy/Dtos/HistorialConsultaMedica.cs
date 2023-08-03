using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BackPuppy.Dtos
{
    public class HistorialConsultaMedica
    {
        public int id_mascota { get; set; }
        public List<ConsultaMedicaDtoOut> listaConsultaMedica{ get; set; }

        

    }
}
