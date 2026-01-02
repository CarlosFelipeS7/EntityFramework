using FuscaFilmes.EndpointHandlers;

namespace FuscaFilmes.Extensions
{
    public static class EndpointRouterBuilderExtensions //SOLID - OPEN CLOSED PRINCIPLE
    {
        public static void DiretoresEndpoints(this IEndpointRouteBuilder app) //método de extensão do app para mapear os endpoints de diretores
        {
            app.MapGet("/diretor", DiretoresHandlers.GetDiretores);
            app.MapGet("/diretor/agregacao/{DiretorId}", DiretoresHandlers.GetDiretorById);
            app.MapPost("/diretor", DiretoresHandlers.CreateDiretor);
            app.MapPut("/diretor/{diretorId}", DiretoresHandlers.UpdateDiretor);
            app.MapDelete("/diretor/{diretorId}", DiretoresHandlers.DeleteDiretor);
        }


        public static void FilmesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/filmes", FilmesHandlers.GetFilmes);
            app.MapGet("/filmes/{id}", FilmesHandlers.GetFilmesById);
            app.MapGet("/filmesEFFunction/byName/{titulo}", FilmesHandlers.GetFilmesByNameEFFunction);
            app.MapGet("/filmesLinQ/byName/{titulo}", FilmesHandlers.GetFilmesByNameLINQ);
            app.MapDelete("/filmes/{filmeId}", FilmesHandlers.DeleteFilme);
            app.MapPatch("/filmes", FilmesHandlers.UpdateFilme);

        }
    }
}