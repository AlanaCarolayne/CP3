using CP3.Domain;
using Dapper;
using MySqlConnector;

namespace CP3.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly IConfiguration _configuration;

        public LivroRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MySqlConnection Conexao() =>
            new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        public Task AddLivroAsync(Livro l)
        {
            using var conexao = Conexao();
            var sql = @"INSERT INTO Livro 
                        (Isbn, Titulo, Autor, Categoria, Statusl, DataCadastro)
                        VALUES (@Isbn, @Titulo, @Autor, @Categoria, @Statusl, @DataCadastro)";
            return conexao.ExecuteAsync(sql, l);
        }

        public Task UpdateLivroAsync(Livro l)
        {
            using var conexao = Conexao();
            var sql = @"UPDATE Livro 
                        SET Titulo = @Titulo, Autor = @Autor, Categoria = @Categoria, Statusl = @Statusl
                        WHERE Isbn = @Isbn";
            return conexao.ExecuteAsync(sql, l);
        }

        public Task UpdateStatusAsync(int isbn, StatusL status)
        {
            using var conexao = Conexao();
            var sql = @"UPDATE Livro 
                        SET Statusl = @Statusl
                        WHERE Isbn = @Isbn";
            return conexao.ExecuteAsync(sql, new { Isbn = isbn, Statusl = status });
        }

        public Task DeleteLivroAsync(int isbn)
        {
            using var conexao = Conexao();
            var sql = "DELETE FROM Livro WHERE Isbn = @Isbn";
            return conexao.ExecuteAsync(sql, new { Isbn = isbn });
        }

        public Task<Livro?> GetLivroByIsbnAsync(int isbn)
        {
            using var conexao = Conexao();
            var sql = "SELECT * FROM Livro WHERE Isbn = @Isbn";
            return conexao.QueryFirstOrDefaultAsync<Livro>(sql, new { Isbn = isbn });
        }

        public Task<IEnumerable<Livro>> GetAllLivrosAsync()
        {
            using var conexao = Conexao();
            var sql = "SELECT * FROM Livro";
            return conexao.QueryAsync<Livro>(sql);
        }
    }
}
