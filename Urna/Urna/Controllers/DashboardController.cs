using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Urna.Model;
using Urna.Models;

namespace Urna.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult ComputarVotos()
        {
            List<DtoApurarVotos> ListaApuracao = new List<DtoApurarVotos>();
            DtoApurarVotos candidato1 = new DtoApurarVotos();
            candidato1.NomeCompleto= "Gustavo Peixoto";
            //candidato1.ViceCandidato = "Roberto Carlos";
            candidato1.Legenda = "PT";
            candidato1.QtdVotos = 300;

            DtoApurarVotos candidato2 = new DtoApurarVotos();
            candidato2.NomeCompleto = "Gabriel Veloso";
            //candidato2.ViceCandidato = "Pedro Santos";
            candidato2.Legenda = "PSDB";
            candidato2.QtdVotos = 250;

            DtoApurarVotos candidato3 = new DtoApurarVotos();
            candidato3.NomeCompleto = "Gustavo Pedroti";
            //candidato3.ViceCandidato = "Claudio Cerqueira";
            candidato3.Legenda = "MBL";
            candidato3.QtdVotos = 200;

            DtoApurarVotos candidato4 = new DtoApurarVotos();
            candidato4.NomeCompleto = "Roberto Farias";
            //candidato4.ViceCandidato = "Pedrinho Lima";
            candidato4.Legenda = "PT";
            candidato4.QtdVotos = 150;

            ListaApuracao.Add(candidato1);
            ListaApuracao.Add(candidato2);
            ListaApuracao.Add(candidato3);
            ListaApuracao.Add(candidato4);

            ViewBag.ListaVotosApurados = ListaApuracao;

            return View();
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
