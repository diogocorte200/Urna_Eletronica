using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Urna.Model;
using Urna.Models;

namespace Urna.Controllers
{
    public class CandidatoController : Controller
    {
        private readonly ILogger<UrnaController> _logger;
        private readonly IConfiguration _configuration;

        public CandidatoController(ILogger<UrnaController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<IActionResult> CriarCandidato()
        {

            List<DtoCandidato> ListaCandidato = new List<DtoCandidato>();

            using (var client = new HttpClient())
            {
                var getCandidato = await client.GetAsync(_configuration["BaseUrl"] + "api/Candidato/Listar");
                if (!getCandidato.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(getCandidato.ToString());
                }
                var resultCandidato = JsonConvert.DeserializeObject<List<DtoCandidato>>(await getCandidato.Content.ReadAsStringAsync());
                if (resultCandidato != null)
                {


                    foreach (var item in resultCandidato)
                    {
                        ListaCandidato.Add(item);
                    }
                }
            }
            List<DtoLegenda> ListaLegenda = new List<DtoLegenda>();

            var DtoLegenda1 = new DtoLegenda(13, "PT");
            var DtoLegenda2 = new DtoLegenda(45, "PSDB");
            var DtoLegenda3 = new DtoLegenda(33, "PSOL");
            var DtoLegenda4 = new DtoLegenda(22, "PSL");
            var DtoLegenda5 = new DtoLegenda(15, "P2");
            var DtoLegenda6 = new DtoLegenda(18, "P1");

            ListaLegenda.Add(DtoLegenda1);
            ListaLegenda.Add(DtoLegenda2);
            ListaLegenda.Add(DtoLegenda3);
            ListaLegenda.Add(DtoLegenda4);
            ListaLegenda.Add(DtoLegenda5);
            ListaLegenda.Add(DtoLegenda6);


            List<SelectListItem> legendas;
            legendas = ListaLegenda.Select(c => new SelectListItem()
            {
                Text = c.NomeLegenda.ToUpper(),
                Value = c.IdLegenda.ToString()
            }).ToList();

            ViewBag.ListaCandidato = ListaCandidato;
            ViewBag.ListaLegenda = legendas;

            return View();
        }

        [HttpPost("CadastrarCandidato")]
        public async Task<bool> CadastrarCandidato([FromBody] DtoCandidato candidato)
        {
            using (var client = new HttpClient())
            {
                var getLegenda = await client.GetAsync(_configuration["BaseUrl"] + "api/Candidato/legenda/" + candidato.Legenda); ;
                if (!getLegenda.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(getLegenda.ToString());
                }
                var legenda = JsonConvert.DeserializeObject<DtoCandidato>(await getLegenda.Content.ReadAsStringAsync());
                if (legenda != null)
                {
                    return false;
                }

                var jsonContent = JsonConvert.SerializeObject(candidato);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_configuration["BaseUrl"] + "api/Candidato/Adicionar", contentString);

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

        [HttpPost("DeletarCandidato/{id}")]
        public async Task<bool> DeletarCandidato(string id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync(_configuration["BaseUrl"] + "api/Candidato/Deletar/" + id);

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

        [HttpPost("BuscarCandidato/{legenda}")]
        public async Task<DtoCandidato> BuscarCandidato(int legenda)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_configuration["BaseUrl"] + "api/Candidato/legenda/" + legenda);
                var jsonString = response.Content.ReadAsStringAsync();
                var candidato = JsonConvert.DeserializeObject<DtoCandidato>(jsonString.Result);

                return candidato;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
