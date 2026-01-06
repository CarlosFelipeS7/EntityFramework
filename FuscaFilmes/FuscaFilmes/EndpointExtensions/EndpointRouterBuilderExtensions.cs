using FuscaFilmes.EndpointHandlers;

namespace FuscaFilmes.Extensions
{
    public static class EndpointRouterBuilderExtensions //SOLID - OPEN CLOSED PRINCIPLE
    {
        public static void DiretoresEndpoints(this IEndpointRouteBuilder app) //método de extensão do app para mapear os endpoints de diretores
        {
            app.MapGet("/diretor", DiretoresHandlers.GetDiretoresAsync);
            app.MapGet("/diretor/agregacao/{DiretorId}", DiretoresHandlers.GetDiretorByIdAsync);
            app.MapPost("/diretor", DiretoresHandlers.CreateDiretorAsync);
            app.MapPut("/diretor/{diretorId}", DiretoresHandlers.UpdateDiretorAsync);
            app.MapDelete("/diretor/{diretorId}", DiretoresHandlers.DeleteDiretorAsync);
        }


        public static void FilmesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/filmes", FilmesHandlers.GetFilmes);
            app.MapGet("/filmes/{id}", FilmesHandlers.GetFilmesById);
            app.MapGet("/filmesEFFunction/byName/{titulo}", FilmesHandlers.GetFilmesByNameEFFunctionAsync);
            app.MapGet("/filmesLinQ/byName/{titulo}", FilmesHandlers.GetFilmesByNameLINQAsync);
            app.MapDelete("/filmes/{filmeId}", FilmesHandlers.DeleteFilmeAsync);
            app.MapPatch("/filmes", FilmesHandlers.UpdateFilme);

        }
    }
}