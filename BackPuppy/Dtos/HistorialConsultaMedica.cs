using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BackPuppy.Dtos
{
    public class HistorialConsultaMedica
    {
        public int id_mascota { get; set; }
        public List<ConsultaMedicaDtoOut> listaConsultaMedica{ get; set; }
        public List<VacunaOutDto> listaVacunas { get; set; }
        public List<DesparacitacionOutDto> listaDesparaciones { get; set; }
        public List<CirugiaOutDto> listaCirugias { get; set; }
    }
}
