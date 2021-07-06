using System;
using System.Collections.Generic;
using System.Text;

namespace Urna.Domain.Domain
{
    public class ComputarVotoModel
    {
        public string NomeCompleto { get; set; }
        public string ViceCandidato { get; set; }
        public string Legenda { get; set; }
        public int QtdVotos { get; set; }
    }
}
