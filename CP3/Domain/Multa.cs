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
        public StatusM Statusm { get; set; }

        public string GerarMulta(Emprestimo emprestimo)
        {
            if (emprestimo.DataDevolucaoReal == null)
                return "Livro ainda não devolvido.";

            if (emprestimo.DataDevolucaoReal <= emprestimo.DataPrevista)
                return "Devolução dentro do prazo, nenhuma multa gerada.";

            int diasAtraso = (emprestimo.DataDevolucaoReal.Value - emprestimo.DataPrevista).Days;

            ValorMulta = diasAtraso * 1;
            Statusm = StatusM.Pendente;
            EmprestimoId = emprestimo.Id;

            return $"Multa gerada: R$ {ValorMulta} ({diasAtraso} dias de atraso).";
        }
    }
}
