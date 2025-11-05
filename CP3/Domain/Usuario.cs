using System.ComponentModel.DataAnnotations;

namespace CP3.Domain
{
    public enum TipoUsuario
    {
        Aluno,
        Professor,
        Funcionario
    }
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string email { get; set; }
        public TipoUsuario TipoU { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
