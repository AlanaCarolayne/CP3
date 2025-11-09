using CP3.Services;
using Microsoft.AspNetCore.Mvc;

namespace CP3.performance_cache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly IEmprestimoService _emprestimoService;

        public EmprestimoController(IEmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

       
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarEmprestimo(int usuarioId, int livroIsbn)
        {
            var resultado = await _emprestimoService.RegistrarEmprestimoAsync(usuarioId, livroIsbn);

            if (!resultado.Contains("sucesso", StringComparison.OrdinalIgnoreCase))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        
        [HttpPost("devolver")]
        public async Task<IActionResult> RegistrarDevolucao(int emprestimoId)
        {
            var resultado = await _emprestimoService.RegistrarDevolucaoAsync(emprestimoId);

            if (!resultado.Contains("registrada", StringComparison.OrdinalIgnoreCase))
                return BadRequest(resultado);

            return Ok(resultado);
        }

      
        [HttpGet("disponivel/{isbn}")]
        public async Task<IActionResult> VerificarDisponibilidade(int isbn)
        {
            var disponivel = await _emprestimoService.LivroDisponivelAsync(isbn);

            return Ok(new
            {
                Isbn = isbn,
                Disponivel = disponivel
            });
        }

        [HttpPut("status/{emprestimoId}")]
        public async Task<IActionResult> AtualizarStatus(int emprestimoId)
        {
            await _emprestimoService.AtualizarStatusEmprestimoAsync(emprestimoId);
            return Ok("Status do empréstimo atualizado.");
        }
    }
}
