namespace FuscaFilmes.Entities
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }

        public int DiretorId { get; set; } // Chave estrangeira para o diretor
        public Diretor Diretor { get; set; }
    }
}
