namespace CP3.Domain
{
    public enum StatusL
    {
        Disponivel,
        Emprestado,
        Reservado
    }

    public class Livro
    {
        public int Isbn { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
        public StatusL Statusl { get; set; } = StatusL.Disponivel;
        public DateTime DataCadastro { get; set; }

        public bool PodeEmprestar()
        {
            return Statusl == StatusL.Disponivel;
        }

        public void Emprestar()
        {
            if (!PodeEmprestar())
                throw new Exception("Livro indisponível para empréstimo.");

            Statusl = StatusL.Emprestado;
        }
    }
}
