namespace BackPuppy.Dtos
{
    public class EmailRequestModel
    {
        public string correoDestinatrio { get; set; } // Dirección de correo electrónico del destinatario
        public string asunto { get; set; } // Asunto del correo
        public string cuerpo { get; set; } // Cuerpo del correo (puede ser HTML o texto sin formato)
                                         // Otras propiedades opcionales según tus necesidades, como CC, BCC, adjuntos, etc.
    }
}
