using System.Numerics;

namespace BackPuppy.Dtos
{
    public class ControlFisicoDtoOut
    {
        public int id_control_fisico { get; set; }
        public string? temperatura { get; set; }
        public string? frecCardiaca { get; set; }
        public string? frecRespiratoria { get; set; }
        public string? peso { get; set; }
    }
}
