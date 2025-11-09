
using Dapper;
using MySqlConnector;

namespace CP3.Repository
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly IConfiguration _configuration;
        public RelatorioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private MySqlConnection Conexao() => new(_configuration.GetConnectionString("DefaultConnection"));
        public async Task<IEnumerable<int>> ObterEmprestimosAtrasadosAsync()
        {
           using var conexao = Conexao();
            var sql = @"SELECT Id 
                        FROM Emprestimos 
                        WHERE DataDevolucaoEsperada < CURDATE() 
                        AND DataDevolucao IS NULL";
            return await conexao.QueryAsync<int>(sql);

        }

        public Task<IEnumerable<(int LivroIsbn, int TotalEmprestimos)>> ObterLivrosMaisEmprestadosAsync()
        {
           using var conexao = Conexao();
            var sql = @"SELECT LivroIsbn, COUNT(*) AS TotalEmprestimos 
                        FROM Emprestimos 
                        GROUP BY LivroIsbn 
                        ORDER BY TotalEmprestimos DESC";
            return conexao.QueryAsync<(int LivroIsbn, int TotalEmprestimos)>(sql);
        }

        public Task<IEnumerable<(int UsuarioId, int TotalEmprestimos)>> ObterUsuariosComMaisEmprestimosAsync()
        {
            using var conexao = Conexao();
            var sql = @"SELECT UsuarioId, COUNT(*) AS TotalEmprestimos 
                        FROM Emprestimos 
                        GROUP BY UsuarioId 
                        ORDER BY TotalEmprestimos DESC";
            return conexao.QueryAsync<(int UsuarioId, int TotalEmprestimos)>(sql);
        }
    }
}
