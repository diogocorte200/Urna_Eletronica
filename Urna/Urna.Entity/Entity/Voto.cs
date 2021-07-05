using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Urna.Entity.Entity
{
    public class Voto : BaseEntity
    {
        public DateTime DataVoto { get; set; }
        public Guid IdCandidato { get; set; }
    }
}
