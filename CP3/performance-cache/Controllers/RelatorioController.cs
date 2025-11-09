using CP3.Services;
using Microsoft.AspNetCore.Mvc;

namespace CP3.performance_cache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatorioController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

      
        [HttpGet("livros-mais-emprestados")]
        public async Task<IActionResult> LivrosMaisEmprestados()
        {
            var resultado = await _relatorioService.LivrosMaisEmprestadosAsync();

            if (!resultado.Any())
                return NoContent(); 

            return Ok(resultado); 
        }

        [HttpGet("usuarios-mais-emprestimos")]
        public async Task<IActionResult> UsuariosComMaisEmprestimos()
        {
            var resultado = await _relatorioService.UsuariosComMaisEmprestimosAsync();

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        
        [HttpGet("atrasados")]
        public async Task<IActionResult> EmprestimosAtrasados()
        {
            var resultado = await _relatorioService.EmprestimosAtrasadosAsync();

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
    }
}
