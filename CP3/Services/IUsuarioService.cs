using CP3.Domain;

namespace CP3.Services
{
    public interface IUsuarioService
    {
        Task AddUsuarioAsync(Usuario usuario);
        Task<bool> ValidarLimiteEmprestimosAsync(int usuarioId);
        
    }
}
