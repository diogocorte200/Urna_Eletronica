using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Urna.Model
{
    public class DtoApurarVotos
    {
        public string NomeCompleto{ get; set; }
        public string ViceCandidato { get; set; }
        public string Legenda { get; set; }
        public int QtdVotos { get; set; }

        public DtoApurarVotos()
        {

        }
    }
}
