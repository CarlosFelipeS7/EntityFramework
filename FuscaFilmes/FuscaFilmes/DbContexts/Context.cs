using FuscaFilmes.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.DbContexts
{
    public class Context : DbContext //Sempre precisa herdar de DbContext
    {
        // Simulando um banco de dados em memória
        public DbSet<Filme> Filmes { get; set; } // colecao de objetos do tipo filme
        public DbSet<Diretor> Diretores { get; set; } // colecao de objetos do tipo diretor

        protected  override void OnConfiguring(DbContextOptionsBuilder options) =>options.UseSqlite("Data Source = EFCoreConsole.db");
        
    }
}
