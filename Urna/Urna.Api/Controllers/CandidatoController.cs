using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpDelete("Deletar")]
        public async Task<IActionResult> DeletarCandidato(Guid idCandidato)
        {
            if (idCandidato == null)
            {
                return BadRequest();
            }

            var resultado = await _candidato.DeletarCandidato(idCandidato);

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
