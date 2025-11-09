using CP3.Domain;
using Dapper;
using MySqlConnector;

namespace CP3.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration _configuration;
        public UsuarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MySqlConnection Conexao() => new(_configuration.GetConnectionString("DefaultConnection"));

        public async Task AddAsync(Usuario usuario)
        {
           using var conexao = Conexao(); 
            var sql = @"INSERT INTO Usuarios (Nome, Email, Tipo, DataCadastro) 
                        VALUES (@Nome, @Email, @Tipo, @DataCadastro)";
            await conexao.ExecuteAsync(sql, usuario);
        }

        public Task DeleteAsync(Usuario usuario)
        {
using var conexao = Conexao();
            var sql = "DELETE FROM Usuarios WHERE Id = @Id";
            return conexao.ExecuteAsync(sql, new { usuario.Id });
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            using var conexao = Conexao();
            return await conexao.QueryAsync<Usuario>("SELECT * FROM Usuarios");
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            using var conexao = Conexao();
            return await conexao.QueryFirstOrDefaultAsync<Usuario>("SELECT * FROM Usuarios where id = @id", new { id });
        }

        public Task UpdateAsync(Usuario usuario)
        {
           using var conexao = Conexao();
            var sql = @"UPDATE Usuarios 
                        SET Nome = @Nome, Email = @Email, Tipo = @Tipo, DataCadastro = @DataCadastro 
                        WHERE Id = @Id";
            return conexao.ExecuteAsync(sql, usuario);
        }
    }
}
