using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Urna.Model
{
    public class DtoGetCandidato
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string NomeVice { get; set; }
        public DateTime DataRegistro { get; set; }
        public int Legenda { get; set; }
    }
}
