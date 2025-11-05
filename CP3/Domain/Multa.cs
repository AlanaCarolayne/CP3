namespace CP3.Domain
{
    public enum StatusM
    {
        Pendente,
        Paga
    }
    public class Multa
    {
        public int EmprestimoId { get; set; }
        public int ValorMulta { get; set; }
        public Status Statusm { get; set; }

    }

}
