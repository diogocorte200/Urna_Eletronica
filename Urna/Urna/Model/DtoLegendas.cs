using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Urna.Model
{
    public class DtoLegendas
    {
        public Guid Id { get; set; }
        public string NomeCandidato { get; set; }
        public string ViceCandidato { get; set; }
        public int Legenda { get; set; }

        public DtoLegendas()
        {

        }
    }
}
