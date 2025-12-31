using System.Reflection;
using System.IO;
using FuscaFilmes.DbContexts;
using FuscaFilmes.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
{
    var conn = builder.Configuration.GetConnectionString("FuscaFilmesStr");
    options.UseSqlite(conn);
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
app.MapGet("/diretor", (Context context) => //injetando o contexto via parametro, ou seja ele ja cria o contexto para mim
{
  
    return context.Diretores.Include(diretor => diretor.Filmes).ToList(); //lambda para incluir os filmes relacionados ao diretor
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
    if (diretor == null)
    {
        context.Remove(diretor);
    }

  
    context.SaveChanges();
});


app.Run();

