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
        public int id { get; set; }
        public int UsuarioId { get; set; }
        public int LivroId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime Dataprevista { get; set; }
        public DateTime Datadevolução { get; set; }

        public StatusE Statuse { get; set; }

    }
}
