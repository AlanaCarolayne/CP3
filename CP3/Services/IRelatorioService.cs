using CP3.Domain;

namespace CP3.Services
{
    public interface IRelatorioService
    {
        Task<IEnumerable<Livro>> LivrosMaisEmprestadosAsync();

        Task<IEnumerable<Usuario>> UsuariosComMaisEmprestimosAsync();

        Task<IEnumerable<Emprestimo>> EmprestimosAtrasadosAsync();
    }
}
