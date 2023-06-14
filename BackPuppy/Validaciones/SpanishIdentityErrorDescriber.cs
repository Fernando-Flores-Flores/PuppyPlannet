using Microsoft.AspNetCore.Identity;

namespace BackPuppy.Validaciones
{
    public class SpanishIdentityErrorDescriber: IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
        {
            return new IdentityError { Code = nameof(DefaultError), Description = "Se ha producido un error." };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code = nameof(DuplicateEmail), Description = $"El email '{email}' ya está en uso." };
        }

        // Agrega otros métodos de acuerdo a tus necesidades, traduciendo los mensajes de error según corresponda.
    }
}
