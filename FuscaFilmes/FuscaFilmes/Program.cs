using System.Reflection;
using System.IO;
using FuscaFilmes.DbContexts;
using FuscaFilmes.Entities;

var builder = WebApplication.CreateBuilder(args);

//Realizando ensure, ou seja , garantindo que o banco de dados seja criado
using (var db = new FuscaFilmes.DbContexts.Context())
{
    db.Database.EnsureCreated();
}



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FuscaFilmes v1"));
}

app.UseHttpsRedirection();

//criando verbos http 
app.MapGet("/Diretor", () =>
{
   using var context = new Context(); //using para garantir o descarte do contexto após o uso
    return context.Diretores.ToList();
});

app.MapPost("/Diretor", (Diretor diretor) => //post é um verbo que eu passo um corpo para ele
{
    using var context = new Context();
    context.Add(diretor);
    context.SaveChanges();
});

app.MapPut("/Diretor/{diretorId}", (int diretorId) =>
{

    using var context = new Context();
    var diretor = context.Diretores.Find(diretorId);

    if (diretor != null)
    {
        diretor.Name = "Diretor Atualizado";
        context.Update(diretor);
        context.SaveChanges();
    }

});

app.MapDelete("/Diretor/{diretorId}", (int diretorId) =>
{
    using var context = new Context();
    var diretor = context.Diretores.Find(diretorId);
    if (diretor == null)
    {
        context.Remove(diretor);
    }

  
    context.SaveChanges();
});


app.Run();

