using System.Reflection;
using System.IO;
using FuscaFilmes.DbContexts;
using FuscaFilmes.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
{
    var conn = builder.Configuration.GetConnectionString("FuscaFilmesStr");
    options.UseSqlite(conn)
    .LogTo(Console.WriteLine, LogLevel.Information); // Log é o registro de atividades do EF Core
});





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
            new Filme { Id = 1, Titulo = "TinTin", Ano = 2010, DiretorId = 1 },
            new Filme { Id = 2, Titulo = "Jurassic Park", Ano = 1993, DiretorId = 1 },
            new Filme { Id = 3, Titulo = "Inception", Ano = 2010, DiretorId = 2 },
            new Filme { Id = 4, Titulo = "Interstellar", Ano = 2014, DiretorId = 2 },
            new Filme { Id = 5, Titulo = "Pulp Fiction", Ano = 1994, DiretorId = 3 },
            new Filme { Id = 6, Titulo = "Kill Bill", Ano = 2003, DiretorId = 3 },
            new Filme { Id = 7, Titulo = "Avatar", Ano = 2009, DiretorId = 4 },
            new Filme { Id = 8, Titulo = "Titanic", Ano = 1997, DiretorId = 4 }
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



//criando verbos http 
app.MapGet("/diretor", (Context context) => //injetando o contexto via parametro, ou seja ele ja cria o contexto para mim,
{
  
    return context.Diretores.Include(diretor => diretor.Filmes).ToList(); //lambda para incluir os filmes relacionados ao diretor
});


app.MapGet("/diretor/agregacao/{DiretorId}", (int DiretorId,Context context) => 
{

    return context.Diretores.Where(diretor => diretor.Id == DiretorId).

    Include(diretor => diretor.Filmes)
    .Select(diretor=>diretor.Name); // Seleciona apenas o nome do diretor


});




app.MapGet("/diretor/where/{DiretorId}", (int DiretorId, Context context) =>
{

    return context.Diretores.Where(diretor => diretor.Id == DiretorId).

    Include(diretor => diretor.Filmes)
    .OrderBy(diretor => diretor.Name) // Ordena os diretores pelo nome)
    .FirstOrDefault(); // Retorna o primeiro diretor que corresponde ao ID fornecido ou nulo se não encontrado
});



app.MapGet("/filmes", ( Context context) =>
{

    return context.Filmes.
    Include(filme => filme.Diretor)
    .OrderBy(filme => filme.Ano) // Ordena os filmes pelo ano de lançamento
    .ToList();
});

app.MapGet("/filmes/{id}", (int id, Context context) => 
{

    return context.Filmes.Where(filme => filme.Id == id).

    Include(filme => filme.Diretor).ToList();
});


app.MapGet("/filmesEFFunction/byName/{titulo}", (string titulo, Context context) =>
{

    return context.Filmes
        .Where(filme =>
            EF.Functions.Like(filme.Titulo, $"%{titulo}%") // Usando EF.Functions.Like para buscar se o título contém a string dada
        )
        .Include(filme => filme.Diretor)
        .ToList();
}); 


app.MapGet("/filmesLinQ/byName/{titulo}", (string titulo, Context context) =>
{

    return context.Filmes
        .Where(filme =>
            EF.Functions.Like(filme.Titulo, $"%{titulo}%") 
        )
        .Include(filme => filme.Diretor)
        .ToList();
});



app.MapPost("/diretor", (Context context, Diretor diretor) => //post é um verbo que eu passo um corpo para ele
{

    context.Add(diretor);
    context.SaveChanges();
});

app.MapPut("/diretor/{diretorId}", (Context context, int diretorId) =>
{

  
    var diretor = context.Diretores.Find(diretorId);

    if (diretor != null)
    {
        diretor.Name = "Diretor Atualizado";
        context.Update(diretor);
        context.SaveChanges();
    }

});

app.MapDelete("/diretor/{diretorId}", (Context context, int diretorId) =>
{
   
    var diretor = context.Diretores.Find(diretorId);
    if (diretor != null)
    {
        context.Remove(diretor);
        context.SaveChanges();
    }

});


app.Run();

