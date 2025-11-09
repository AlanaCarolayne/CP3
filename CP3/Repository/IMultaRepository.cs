namespace CP3.Repository
{
    public interface IMultaRepository
    {
        Task<decimal> CalcularMultaAsync(int emprestimoId);
        Task RegistrarPagamentoMultaAsync(int emprestimoId, decimal valorPago);
 
    }
}
