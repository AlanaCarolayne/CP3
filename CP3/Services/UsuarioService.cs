using CP3.Domain;
using CP3.Repository;
using Microsoft.EntityFrameworkCore;

namespace CP3.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmprestimoRepository _emprestimoRepository;
        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IEmprestimoRepository emprestimoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _emprestimoRepository = emprestimoRepository;
        }
        public async Task AddUsuarioAsync(Usuario usuario)  
        {
            
            await _usuarioRepository.AddAsync(usuario);
        }
        public async Task<bool> ValidarLimiteEmprestimosAsync(int usuarioId)
        {
         
            var emprestimos = await _emprestimoRepository.GetAllEmprestimosAsync();

            int qtdEmprestimosAtivos = emprestimos
                .Where(e => e.UsuarioId == usuarioId && e.Statuse == StatusE.Ativo)
                .Count();

            if (qtdEmprestimosAtivos >= 3)
            {
                throw new Exception("Usuário já possui 3 empréstimos ativos.");
            }
            else
            {

                return qtdEmprestimosAtivos < 3;
            }

        }

    }
}
