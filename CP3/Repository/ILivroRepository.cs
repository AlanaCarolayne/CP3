using CP3.Domain;

namespace CP3.Repository
{
    public interface ILivroRepository
    {
        Task AddLivroAsync(Livro l);
        Task UpdateLivroAsync(Livro l);
        Task UpdateStatusAsync(int isbn, StatusL status);
        Task DeleteLivroAsync(int isbn);
        Task<Livro?> GetLivroByIsbnAsync(int isbn);
        Task<IEnumerable<Livro>> GetAllLivrosAsync();
    }
}
