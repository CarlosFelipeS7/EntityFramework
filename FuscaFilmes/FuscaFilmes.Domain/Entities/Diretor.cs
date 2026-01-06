using System.Collections.Generic;

namespace FuscaFilmes.Domain.Entities;
    public class Diretor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Filme> Filmes { get; set; } = new List<Filme>();

    public ICollection<DiretorFilme> DiretoresFilmes { get; set; } = new List<DiretorFilme>();

    public DiretorDetalhe DiretorDetalhe { get; set; }

}


