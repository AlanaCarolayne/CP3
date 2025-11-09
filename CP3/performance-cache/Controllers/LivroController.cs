using CP3.Domain;
using CP3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CP3.performance_cache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;
        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }
        [HttpPost]
        public async Task<IActionResult> AddLivro([FromBody] Livro livro)
        {
            await _livroService.AddLivroAsync(livro);
            return Ok(livro);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateLivro([FromBody] Livro livro, StatusL status)
        {
            await _livroService.AtualizarStatusAsync(livro.Isbn,  status );
            return Ok(livro);
        }
    }
}
