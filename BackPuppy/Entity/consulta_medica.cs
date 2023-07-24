using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackPuppy.Entity
{
    [Table(name: "consulta_medica", Schema = "public")]

    public class consulta_medica : ApiEstadosDto
    {
        [Key]
        public int id_consulta_medica { get; set; }
        public string? motivoConsu1ta { get; set; }
        public string? diagnosticoConsu1ta { get; set; }
        public string? tratamiento { get; set; }
        public string? proxVisita { get; set; }

        [ForeignKey("mascota")]
        public int id_mascota { get; set; }
        public mascota mascota { get; set; }

    }
}
