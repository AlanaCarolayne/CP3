using CP3.Domain;

namespace CP3.Services
{
    public interface ILivroService
    {
        Task AddLivroAsync(Livro l);
        Task UpdateLivroAsync(Livro l);
        Task AtualizarStatusAsync(int isbn, StatusL novoStatus);
        Task <Livro?> GetLivroByISBNAsync(int livroIsbn);
    }
}
