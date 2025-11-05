namespace CP3.Domain
{
    public enum Status
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
        public Status? StatusL { get; set; }  
        public DateTime DataCadastro {  get; set; }



    }
}
