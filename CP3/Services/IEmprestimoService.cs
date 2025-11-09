using CP3.Domain;

namespace CP3.Services
{
    public interface IEmprestimoService
    {
        Task<string> RegistrarEmprestimoAsync(int usuarioId, int livroIsbn);
        Task<string> RegistrarDevolucaoAsync(int emprestimoId);
        Task AtualizarStatusEmprestimoAsync(int emprestimoId);
        Task<bool> LivroDisponivelAsync(int livroIsbn);
    }
}
