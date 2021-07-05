using System;
using System.Collections.Generic;
using System.Text;

namespace Urna.Domain.Domain
{
    public class ExibicaoMensagemViewModel
    {
        public string Cabecalho { get; set; }
        public string MensagemCurta { get; set; }
        public string Detalhes { get; set; }
        public int StatusCode { get; set; }
    }
}
