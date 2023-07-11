using Microsoft.AspNetCore.Mvc;

namespace BackPuppy.Validaciones
{
    public class DetalleProblemaHelper
    {
        public static ObjectResult InternalServerError(HttpRequest request, string title = "Error interno del servidor", string detail = "Por favor intente nuevamente en algunos minutos.",string mensaje = "")
        {
            return Errores(request, StatusCodes.Status500InternalServerError, title, "validation-error", detail, mensaje);
        }
        public static ObjectResult ServiceUnavailable(HttpRequest request, string title = "El servicio está fuera de servicio temporalmente", string detail = "Por favor intente nuevamente en algunos minutos.", string mensaje = "")
        {
            return Errores(request, StatusCodes.Status403Forbidden, title, "validation-error", detail, mensaje);
        }

        public static ObjectResult BadRequest(HttpRequest request, string title = "Los parametros introducidos son incorrectos",string detail="Verifique los datos enviados", string mensaje = "")
        {
            return Errores(request, StatusCodes.Status400BadRequest, title, "validation-error", detail, mensaje);
        }
        public static ObjectResult NotFound(HttpRequest request, string title = "El recurso solicitado no existe", string detail = "El elemento que ha solicitado no se encuentra disponible en el sistema.", string mensaje = "")
        {
            return Errores(request, StatusCodes.Status404NotFound, title, "validation-error",detail, mensaje);
        }

        protected static ObjectResult Errores(HttpRequest request, int statusCode, string title, string type, string detalle, string mensaje)
        {
            var detalleProblema = new DetalleProblema
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Instance = request.Path,
                detalles = detalle,
                mensaje = mensaje
            };

            return new ObjectResult(detalleProblema)
            {
                ContentTypes = { "application/problem+json" },
                StatusCode = statusCode,
            };
        }
    }
}
