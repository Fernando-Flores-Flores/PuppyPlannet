namespace BackPuppy.Dtos
{
    public class ResponseDto<T>
    {
        public int? statusCode { get; set; }
        public int? IdConsulta { get; set; } = new Random().Next();
        public int? codigoRespuesta { get; set; }
        public string? MensajeRespuesta { get; set; }
        public DateTime? fechaConsulta { get; set; } = DateTime.Now;
        public T? datos { get; set; }
        public string? base64 { get; set; }

    }
}
