using CP3.Domain;
using CP3.Repository;

namespace CP3.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task AddLivroAsync(Livro l)
        {
            await _livroRepository.AddLivroAsync(l);
        }

        public async Task UpdateLivroAsync(Livro l)
        {
            await _livroRepository.UpdateLivroAsync(l);
        }

        public async Task AtualizarStatusAsync(int isbn, StatusL novoStatus)
        {
            await _livroRepository.UpdateStatusAsync(isbn, novoStatus);
        }

        public async Task<Livro?> GetLivroByISBNAsync(int livroIsbn)
        {
           return await _livroRepository.GetLivroByIsbnAsync(livroIsbn);
        }
    }
}
