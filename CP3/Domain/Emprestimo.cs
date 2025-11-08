namespace CP3.Domain
{
    public enum StatusE
    {
        Ativo,
        Finalizado,
        Atrasado
    }

    public class Emprestimo
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int LivroId { get; set; }

        public DateTime DataEmprestimo { get; set; }
        public DateTime DataPrevista { get; set; }
        public DateTime? DataDevolucaoReal { get; set; }

        public StatusE Statuse { get; set; }

        public string RealizarEmprestimo(Usuario usuario, Livro livro)
        {
            if (!usuario.PodeRealizarEmprestimo())
                return "Usuário já tem 3 empréstimos ativos.";

            if (!livro.PodeEmprestar())
                return "Livro indisponível.";

            livro.Emprestar();

            UsuarioId = usuario.Id;
            LivroId = livro.Isbn;
            DataEmprestimo = DateTime.Now;


            DataPrevista = usuario.Tipo switch
            {
                TipoUsuario.Aluno => DateTime.Now.AddDays(7),
                TipoUsuario.Professor => DateTime.Now.AddDays(15),
                TipoUsuario.Funcionario => DateTime.Now.AddDays(10),
                _ => DateTime.Now.AddDays(7)
            };

            Statuse = StatusE.Ativo;
            usuario.EmprestimosAtivos.Add(this);

            return "Empréstimo realizado com sucesso.";
        }
    }
}
