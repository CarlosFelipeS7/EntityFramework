using System.Collections.Generic;

namespace FuscaFilmes.Entities
{
    public class Diretor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Filme> Filmes { get; set; } = new List<Filme>(); //colecao de objetos do tipo filme
    }
}
