using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Urna.Model
{
    public class DtoLegenda
    {
        public DtoLegenda(int idLegenda, string nomeLegenda)
        {
            IdLegenda = idLegenda;
            NomeLegenda = nomeLegenda;
        }
        public int IdLegenda { get; set; }
        
        public String NomeLegenda { get; set; }        

    }
}
