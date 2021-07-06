using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Urna.Domain.Domain;
using Urna.Domain.Service;
using Urna.Entity.Entity;

namespace Urna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotoController : ControllerBase
    {
        private readonly VotoService<VotoModel, Voto> _voto;
        
        public VotoController(VotoService<VotoModel, Voto> votoService)
        {
            _voto = votoService;
        }
        [HttpPost("Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] VotoModel voto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (voto == null)
                return BadRequest();

            var votoResposta = await _voto.AdicionarVoto(voto);

            if (votoResposta == null)
            {
                return StatusCode(500, "Erro ao adicionar voto!");
            }
            if (votoResposta.ExibicaoMensagem != null)
            {
                return StatusCode(votoResposta.ExibicaoMensagem.StatusCode, votoResposta);
            }

            return Ok(votoResposta);
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarTodas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var votos = await _voto.ListarVotos();

            return Ok(votos);
        }

        [HttpGet("ComputarVotos")]
        public async Task<IActionResult> ComputarVotos()
        {
            var votosComputados = await _voto.ComputarVotos();

            if (votosComputados == null)
            {
                return StatusCode(500, "Erro ao adicionar voto!");
            }

            return Ok(votosComputados);
        }
    }
}
