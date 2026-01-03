using FuscaFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmesRepo.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        // Simulando um banco de dados em memória
        public DbSet<Filme> Filmes { get; set; } // colecao de objetos do tipo filme
        public DbSet<Diretor> Diretores { get; set; } // colecao de objetos do tipo diretor

        protected override void OnModelCreating(ModelBuilder modelBuilder) //serve para configurar o modelo de dados, simplificando a criação do banco de dados
        {
            modelBuilder.Entity<Diretor>()
                .HasMany(e => e.Filmes) // Um filme tem um diretor
                .WithOne(e => e.Diretor) // Um diretor pode ter muitos filmes
                .HasForeignKey(e => e.DiretorId); // Chave estrangeira no filme


            //Insert de dados iniciais
            modelBuilder.Entity<Diretor>().HasData(
                new Diretor { Id = 1, Name = "Steven Spielberg" },
                new Diretor { Id = 2, Name = "Christopher Nolan" },
                new Diretor { Id = 3, Name = "Quentin Tarantino" },
                new Diretor { Id = 4, Name = "James Cameron" }
            );

            modelBuilder.Entity<Filme>().HasData(
                new Filme { Id = 1, Titulo = "TinTin", Ano = 2010, DiretorId = 1 },
                new Filme { Id = 2, Titulo = "Jurassic Park", Ano = 1993, DiretorId = 1 },
                new Filme { Id = 3, Titulo = "Inception", Ano = 2010, DiretorId = 2 },
                new Filme { Id = 4, Titulo = "Interstellar", Ano = 2014, DiretorId = 2 },
                new Filme { Id = 5, Titulo = "Pulp Fiction", Ano = 1994, DiretorId = 3 },
                new Filme { Id = 6, Titulo = "Kill Bill", Ano = 2003, DiretorId = 3 },
                new Filme { Id = 7, Titulo = "Avatar", Ano = 2009, DiretorId = 4 },
                new Filme { Id = 8, Titulo = "Titanic", Ano = 1997, DiretorId = 4 }
            );
        }
    }
}
