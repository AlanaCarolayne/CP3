using CP3.Domain;
using CP3.Repository;

namespace CP3.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public RelatorioService(
            IEmprestimoRepository emprestimoRepository,
            ILivroRepository livroRepository,
            IUsuarioRepository usuarioRepository)
        {
            _emprestimoRepository = emprestimoRepository;
            _livroRepository = livroRepository;
            _usuarioRepository = usuarioRepository;
        }

      
        public async Task<IEnumerable<Emprestimo>> EmprestimosAtrasadosAsync()
        {
            var emprestimos = await _emprestimoRepository.GetAllEmprestimosAsync();

            return emprestimos
                .Where(e => e.DataPrevista < DateTime.Now &&
                            e.DataDevolucaoReal == null)
                .ToList();
        }

  
        public async Task<IEnumerable<Livro>> LivrosMaisEmprestadosAsync()
        {
            var emprestimos = await _emprestimoRepository.GetAllEmprestimosAsync();

      
            var ranking = emprestimos
                .GroupBy(e => e.LivroId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .ToList();

            var livrosRankeados = new List<Livro>();

            foreach (var livroId in ranking)
            {
                var livro = await _livroRepository.GetLivroByIsbnAsync(livroId);
                if (livro != null)
                    livrosRankeados.Add(livro);
            }

            return livrosRankeados;
        }

      
        public async Task<IEnumerable<Usuario>> UsuariosComMaisEmprestimosAsync()
        {
            var emprestimos = await _emprestimoRepository.GetAllEmprestimosAsync();

        
            var ranking = emprestimos
                .GroupBy(e => e.UsuarioId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .ToList();

            var usuariosRankeados = new List<Usuario>();

            foreach (var usuarioId in ranking)
            {
                var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
                if (usuario != null)
                    usuariosRankeados.Add(usuario);
            }

            return usuariosRankeados;
        }
    }
}
