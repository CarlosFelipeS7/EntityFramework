using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuscaFilmes.Domain.Entities;

public class Filme
{
    [Key]
    [Column ("filme_id")]
    public int Id { get; set; }
    public string Titulo { get; set; }
    public int Ano { get; set; }


    public ICollection<Diretor> Diretores { get; set; }  = new List<Diretor>();


    public ICollection<DiretorFilme> DiretoresFilmes { get; set; } = new List<DiretorFilme>();
}
