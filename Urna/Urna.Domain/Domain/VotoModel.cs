using System;

namespace Urna.Domain.Domain
{
    public class VotoModel : BaseDomain
    {
        public DateTime DataVoto{ get; set; }
        public Guid IdCandidato{ get; set; }
    }
}
