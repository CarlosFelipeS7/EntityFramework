using FuscaFilmes.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.DbContexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        // Simulando um banco de dados em memória
        public DbSet<Filme> Filmes { get; set; } // colecao de objetos do tipo filme
        public DbSet<Diretor> Diretores { get; set; } // colecao de objetos do tipo diretor

        
    }
}
