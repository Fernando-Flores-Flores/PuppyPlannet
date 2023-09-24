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
        public string? motivo_consulta { get; set; }
        public string? diagnostico_consulta { get; set; }
        public string? tratamiento { get; set; }
        public string? fecha_prox_visita { get; set; }
        public string? fecha_registro_consulta { get; set; }

        [ForeignKey("ananmnecis")]
        public int? id_anamnesis { get; set; }
        public ananmnecis ananmnecis { get; set; }


        [ForeignKey("controlFisico")]
        public int? id_control_fisico { get; set; }
        public control_fisico controlFisico { get; set; }


        [ForeignKey("mascota")]
        public int? id_mascota { get; set; }
        public mascota mascota { get; set; }

        public string? precio { get; set; }

    }
}
