using CP3.Domain;
using CP3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CP3.performance_cache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> EmprestimosPorUsuario(Usuario usuario)
        {
            var usuarios = await _usuarioService.ValidarLimiteEmprestimosAsync(usuario.Id);
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> AddUsuario([FromBody] Usuario usuario)
        {
            await _usuarioService.AddUsuarioAsync(usuario);
            return Ok(usuario);
        }

    }
}
