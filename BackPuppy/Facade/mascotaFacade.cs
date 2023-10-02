using BackPuppy.Dtos;

namespace BackPuppy.Facade
{
    public class mascotaFacade
    {
        public string generarCodigoMascota(MascotaDto mascotaIn, string nombreDueño, int idMascotaFuturo)
        {
            var nombremascota = mascotaIn.nombreMascota.Length >= 3 ? mascotaIn.nombreMascota.Substring(0, 3).ToUpper() : mascotaIn.nombreMascota.Substring(0, 1).ToUpper();
            var nombreDueno = !string.IsNullOrEmpty(nombreDueño) && nombreDueño.Length >= 3 ? nombreDueño.Substring(0, 3).ToUpper() : nombreDueño.ToUpper();
            return nombremascota + nombreDueno + idMascotaFuturo;
        }
    }
}
