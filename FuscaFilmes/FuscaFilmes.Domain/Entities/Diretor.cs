using System.Collections.Generic;

namespace FuscaFilmes.Domain.Entities;
    public class Diretor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Filme> Filmes { get; set; } = null!; 
    }


