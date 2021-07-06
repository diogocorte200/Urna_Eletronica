using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Urna.Model;
using Urna.Models;

namespace Urna.Controllers
{
    public class UrnaController : Controller
    {
        private readonly ILogger<UrnaController> _logger;
        private readonly IConfiguration _configuration;

        public UrnaController(ILogger<UrnaController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Votacao()
        {
            return View();
        }

        [HttpPost("VotoFinalizado")]
        public async Task<bool> VotoFinalizado([FromBody] DtoVoto voto)
        {
            using (var client = new HttpClient())
            {
                if (voto.IdCandidato == "" && voto.IdVoto == 0 && voto.NomeCompleto == null)
                {
                    voto.IdCandidato = "9f2fb9fc-25cf-4afa-85ec-ec94e3357619";
                    var jsonContentBranco = JsonConvert.SerializeObject(voto);
                    var contentStringBranco = new StringContent(jsonContentBranco, Encoding.UTF8, "application/json");
                    var responseBranco = await client.PostAsync(_configuration["BaseUrl"] + "api/Voto/Adicionar", contentStringBranco);
                    if (responseBranco.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                var getCandidato = await client.GetAsync(_configuration["BaseUrl"] + "api/Candidato/legenda/" + voto.IdCandidato);
                if (!getCandidato.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(getCandidato.ToString());
                }
                var IdCandidato = JsonConvert.DeserializeObject<DtoLegendas>(await getCandidato.Content.ReadAsStringAsync());
                if (IdCandidato == null)
                {
                    return false;
                }
                voto.IdCandidato = IdCandidato.Id.ToString();
                var resultCandidato = await client.GetAsync(_configuration["BaseUrl"] + "api/Voto/ListarTodas");
                var jsonContent = JsonConvert.SerializeObject(voto);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_configuration["BaseUrl"] + "api/Voto/Adicionar", contentString);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [HttpGet("ConsultaVotos")]
        public async Task<List<DtoVoto>> ConsultaVotos([FromBody] DtoVoto voto)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_configuration["BaseUrl"] + "api/Voto/ListarTodas");

                if (response.StatusCode.Equals(200))
                {
                    return new List<DtoVoto>();
                }
                else
                {
                    return new List<DtoVoto>();
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
