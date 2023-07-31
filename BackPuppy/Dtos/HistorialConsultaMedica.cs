using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BackPuppy.Dtos
{
    public class HistorialConsultaMedica
    {
        public BigInteger id_mascota { get; set; }
  

        public List<AnamnecisDtoOut> listaAnamnecis{ get; set; }
        public List<ControlFisicoDtoOut> listaControlFisico { get; set; }

        

    }
}
