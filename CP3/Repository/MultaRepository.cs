
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace CP3.Repository
{
    public class MultaRepository : IMultaRepository
    {
        private readonly IConfiguration _configuration; 

        public MultaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private MySqlConnection Conexao() => new(_configuration.GetConnectionString("DefaultConnection"));

        public async Task<decimal> CalcularMultaAsync(int emprestimoId)
        {
            using var conexao = Conexao();
            var sql = @"SELECT 
                            CASE 
                                WHEN DATEDIFF(CURDATE(), DataDevolucaoEsperada) > 0 
                                THEN DATEDIFF(CURDATE(), DataDevolucaoEsperada) * 2.00 
                                ELSE 0 
                            END AS Multa 
                        FROM Emprestimos 
                        WHERE Id = @EmprestimoId";
            return await conexao.QueryFirstAsync<decimal>(sql, new { EmprestimoId = emprestimoId });

        }

        public Task RegistrarPagamentoMultaAsync(int emprestimoId, decimal valorPago)
        {
           using var conexao = Conexao();
            var sql = @"UPDATE Emprestimos 
                        SET MultaPaga = @ValorPago 
                        WHERE Id = @EmprestimoId";
            return conexao.ExecuteAsync(sql, new { ValorPago = valorPago, EmprestimoId = emprestimoId });
        }
    }
}
