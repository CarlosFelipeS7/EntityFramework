using System;
using System.Collections.Generic;
using System.Text;

namespace FuscaFilmes.Domain.Entities
{
    public class DiretorFilme //tabela de relacionamento muitos para muitos entre diretores e filmes
    {
        public int DiretorId { get; set; }
        public Diretor Diretor { get; set; }


        public int FilmeId { get; set; }
        public Filme Filme { get; set; }

    }
}
