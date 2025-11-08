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
        public string Email { get; set; }
        public TipoUsuario Tipo { get; set; }
        public DateTime DataCadastro { get; set; }

        public List<Emprestimo> EmprestimosAtivos { get; set; } = new();

        public bool PodeRealizarEmprestimo()
        {
            return EmprestimosAtivos.Count < 3;
        }
    }
}
