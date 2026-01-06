using System.Reflection;
using System.IO;
using Microsoft.EntityFrameworkCore;
using FuscaFilmes.Models;
using FuscaFilmes.EndpointHandlers;
using FuscaFilmes.Extensions;
using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Repo.Contratos;
using FuscaFilmes.Repo;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
{
    var conn = builder.Configuration.GetConnectionString("FuscaFilmesStr");
    options.UseSqlite(conn, b => b.MigrationsAssembly("FuscaFilmes"))

        .LogTo(Console.WriteLine, LogLevel.Information); // Log é o registro de atividades do EF Core
});

builder.Services.AddScoped<IDiretorRepository, DiretorRepository>();



/*Realizando ensure, ou seja , garantindo que o banco de dados seja criado
using (var db = new FuscaFilmes.DbContexts.Context())
{
    db.Database.EnsureCreated();
}*/



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});

var app = builder.Build();

// Seed database at startup (ensure created and insert data if empty)
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<Context>();
    ctx.Database.EnsureCreated();

    if (!ctx.Diretores.Any())
    {
        var diretores = new List<Diretor>
        {
            new Diretor { Id = 1, Name = "Steven Spielberg" },
            new Diretor { Id = 2, Name = "Christopher Nolan" },
            new Diretor { Id = 3, Name = "Quentin Tarantino" },
            new Diretor { Id = 4, Name = "James Cameron" }
        };

        var filmes = new List<Filme>
        {
            new Filme { Id = 1, Titulo = "TinTin", Ano = 2010},
            new Filme { Id = 2, Titulo = "Jurassic Park", Ano = 1993 },
            new Filme { Id = 3, Titulo = "Inception", Ano = 2010},
            new Filme { Id = 4, Titulo = "Interstellar", Ano = 2014},
            new Filme { Id = 5, Titulo = "Pulp Fiction", Ano = 1994 },
            new Filme { Id = 6, Titulo = "Kill Bill", Ano = 2003},
            new Filme { Id = 7, Titulo = "Avatar", Ano = 2009},
            new Filme { Id = 8, Titulo = "Titanic", Ano = 1997 }
        };

        ctx.Diretores.AddRange(diretores);
        ctx.Filmes.AddRange(filmes);
        ctx.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FuscaFilmes v1"));
}

app.UseHttpsRedirection();


app.MapGet("/CreateDB", (Context context) => //injetando o contexto via parametro, ou seja ele ja cria o contexto para mim
{

    context.Database.EnsureCreated();
});


// endpoints via metodo de extensao
app.DiretoresEndpoints();
app.FilmesEndpoints();


app.Run();

