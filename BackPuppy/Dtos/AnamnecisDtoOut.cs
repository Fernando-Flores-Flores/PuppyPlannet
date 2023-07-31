using System.Numerics;

namespace BackPuppy.Dtos
{
    public class AnamnecisDtoOut
    {
        public BigInteger id_ananmnecis { get; set; }
        public string? apetito { get; set; }
        public string? agua { get; set; }
        public string? conducta { get; set; }
        public string? defecacion { get; set; }
        public string? alteracionesRes { get; set; }
        public string? alteracionesNeuro { get; set; }
        public string? problemasUr { get; set; }
    }
}
