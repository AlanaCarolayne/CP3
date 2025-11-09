using CP3.Domain;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CP3.Repository
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly IConfiguration _configuration;
        public EmprestimoRepository(IConfiguration configuration)
        {
            _configuration = configuration; ;
        }
        private MySqlConnector.MySqlConnection Conexao() => new(_configuration.GetConnectionString("DefaultConnection"));

        public async Task AddEmprestimoAsync(Emprestimo e)
        {
            using var conexao = Conexao();
            var sql = @"INSERT INTO Emprestimos (UsuarioId, LivroId, DataEmprestimo, DataPrevista, DataDevolucaoReal, Statuse) 
                        VALUES (@UsuarioId, @LivroId, @DataEmprestimo, @DataPrevista, @DataDevolucaoReal, @Statuse)";
             await conexao.ExecuteAsync(sql, e);
        }

        public async Task<IEnumerable<Emprestimo>> GetAllEmprestimosAsync()
        {
            using var conexao = Conexao(); 
            var sql = "SELECT * FROM Emprestimos";
            return await conexao.QueryAsync<Emprestimo>(sql);

        }

        public async Task<Emprestimo?> GetEmprestimoByIdAsync(int id)
        {
            using var conexao = Conexao();
            var sql = "SELECT * FROM Emprestimos WHERE Id = @Id";
            return await conexao.QueryFirstOrDefaultAsync<Emprestimo>(sql, new { Id = id });

        }

        public async Task UpdateEmprestimoAsync(Emprestimo e)
        {
            using var conexao = Conexao();
            var sql = @"UPDATE Emprestimos 
                        SET UsuarioId = @UsuarioId, LivroId = @LivroId, DataEmprestimo = @DataEmprestimo, DataPrevista = @DataPrevista, DataDevolucaoReal = @DataDevolucaoReal, Statuse = @Statuse 
                        WHERE Id = @Id";
             await conexao.ExecuteAsync(sql, e);
        }

    }
}
