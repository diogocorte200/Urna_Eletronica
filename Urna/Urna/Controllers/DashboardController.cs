using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Urna.Model;
using Urna.Models;

namespace Urna.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IConfiguration _configuration;

        public DashboardController(ILogger<UrnaController> logger, IConfiguration configuration)
        {
            _logger = _logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> ComputarVotos()
        {
            List<DtoApurarVotos> ListaVotosApurados = new List<DtoApurarVotos>();

            using (var client = new HttpClient())
            {
                var getVotos = await client.GetAsync(_configuration["BaseUrl"] + "api/Voto/ComputarVotos");
                if (!getVotos.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(getVotos.ToString());
                }
                var resultCandidato = JsonConvert.DeserializeObject<List<DtoApurarVotos>>(await getVotos.Content.ReadAsStringAsync());
                if (resultCandidato != null)
                {


                    foreach (var item in resultCandidato)
                    {
                        ListaVotosApurados.Add(item);
                    }
                }
            }
            ViewBag.ListaVotosApurados = ListaVotosApurados;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
