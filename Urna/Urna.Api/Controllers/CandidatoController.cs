using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Urna.Domain.Domain;
using Urna.Domain.Service;
using Urna.Entity.Entity;

namespace Urna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private readonly CandidatoService<CandidatoModel, Candidato> _candidato;
        
        public CandidatoController(CandidatoService<CandidatoModel, Candidato> candidatoService)
        {
            _candidato = candidatoService;
        }
        [HttpPost("Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] CandidatoCreateModel candidato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (candidato == null)
                return BadRequest();

            var candidatoResposta = await _candidato.AdicionarCandidato(candidato);

            if (candidatoResposta == null)
            {
                return StatusCode(500, "Erro ao adicionar Candidato!");
            }
            if (candidatoResposta.ExibicaoMensagem != null)
            {
                return StatusCode(candidatoResposta.ExibicaoMensagem.StatusCode, candidatoResposta);
            }

            return Ok(candidatoResposta);
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarTodas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var candidatos = await _candidato.ListarCandidatos();

            return Ok(candidatos);
        }

        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> DeletarCandidato(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            //Em ambiente de produção, isso nao existe, é apenas uma validação, para nao excluir o voto Branco.
            if (id != "9f2fb9fc-25cf-4afa-85ec-ec94e3357619")
            {
                var resultado = await _candidato.DeletarCandidato(id);
            }
            return Ok();
        }

        [HttpGet("legenda/{legendaId}")]
        public async Task<IActionResult> ListarPorLegenda( int legendaId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var candidato = await _candidato.BuscarCandidatoPorLegenda(legendaId);

            return Ok(candidato);
        }
    }
}
