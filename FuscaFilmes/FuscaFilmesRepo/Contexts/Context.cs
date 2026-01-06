using FuscaFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.Repo.Contexts

{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        // Simulando um banco de dados em memória
        public DbSet<Filme> Filmes { get; set; } // colecao de objetos do tipo filme
        public DbSet<Diretor> Diretores { get; set; } // colecao de objetos do tipo diretor

        public DbSet<DiretorFilme> DiretoresFilmes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //serve para configurar o modelo de dados, simplificando a criação do banco de dados
        {

            modelBuilder.Entity<Diretor>()
                .HasMany(d => d.Filmes) 
                .WithMany(f => f.Diretores) 
                .UsingEntity<DiretorFilme>(
                    df => df.HasOne(e => e.Filme)
                        .WithMany(e => e.DiretoresFilmes),
                    df => df
                        .HasOne(e => e.Diretor)
                        .WithMany(e => e.DiretoresFilmes)
                   
                 
                 
                );

      


            //Insert de dados iniciais
            modelBuilder.Entity<Diretor>().HasData(
                new Diretor { Id = 1, Name = "Steven Spielberg" },
                new Diretor { Id = 2, Name = "Christopher Nolan" },
                new Diretor { Id = 3, Name = "Quentin Tarantino" },
                new Diretor { Id = 4, Name = "James Cameron" }
            );

            modelBuilder.Entity<Filme>().HasData(
                new Filme { Id = 1, Titulo = "TinTin", Ano = 2010},
                new Filme { Id = 2, Titulo = "Jurassic Park", Ano = 1993},
                new Filme { Id = 3, Titulo = "Inception", Ano = 2010 },
                new Filme { Id = 4, Titulo = "Interstellar", Ano = 2014 },
                new Filme { Id = 5, Titulo = "Pulp Fiction", Ano = 1994 },
                new Filme { Id = 6, Titulo = "Kill Bill", Ano = 2003},
                new Filme { Id = 7, Titulo = "Avatar", Ano = 2009},
                new Filme { Id = 8, Titulo = "Titanic", Ano = 1997 }
            );


            modelBuilder.Entity<DiretorFilme>().HasData(
               new { DiretorId = 1, FilmeId = 1 },
               new { DiretorId = 1, FilmeId = 2 },
               new { DiretorId = 2, FilmeId = 3 },
               new { DiretorId = 2, FilmeId = 4 },
               new { DiretorId = 3, FilmeId = 5 },
               new { DiretorId = 3, FilmeId = 6 },
               new { DiretorId = 4, FilmeId = 7 },
                 new { DiretorId = 4, FilmeId = 8 }
   );

        }
    
   


    }

}
