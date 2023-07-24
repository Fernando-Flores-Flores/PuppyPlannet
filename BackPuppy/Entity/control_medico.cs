﻿using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackPuppy.Entity
{
    [Table(name: "control_medico", Schema = "public")]

    public class control_medico : ApiEstadosDto
    {
        [Key]
        public int id_control_medico { get; set; }
        public string? temperatura { get; set; }
        public string? frecCardiaca { get; set; }
        public string? frecRespiratoria { get; set; }
        public string? peso { get; set; }

        [ForeignKey("mascota")]
        public int id_mascota { get; set; }
        public mascota mascota { get; set; }

    }
}
