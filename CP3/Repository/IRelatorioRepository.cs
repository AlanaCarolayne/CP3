namespace CP3.Repository
{
    public interface IRelatorioRepository
    {
        Task<IEnumerable<(int LivroIsbn, int TotalEmprestimos)>> ObterLivrosMaisEmprestadosAsync();
        Task<IEnumerable<(int UsuarioId, int TotalEmprestimos)>> ObterUsuariosComMaisEmprestimosAsync();
        Task<IEnumerable<int>> ObterEmprestimosAtrasadosAsync();
    }
}
