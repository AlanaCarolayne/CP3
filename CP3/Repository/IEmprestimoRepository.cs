using CP3.Domain;

namespace CP3.Repository
{
    public interface IEmprestimoRepository
    {
        Task AddEmprestimoAsync(Emprestimo e);
        Task  UpdateEmprestimoAsync(Emprestimo e);
        Task<Emprestimo?> GetEmprestimoByIdAsync(int id);
        Task<IEnumerable<Emprestimo>> GetAllEmprestimosAsync();
      

    }
}
