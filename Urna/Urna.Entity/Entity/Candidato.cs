using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Urna.Entity.Entity
{
    public class Candidato : BaseEntity
    {
        public string NomeCompleto { get; set; }
        public string NomeVice { get; set; }
        public DateTime DataRegistro { get; set; }
        [Key]
        public int Legenda { get; set; }
    }
}
