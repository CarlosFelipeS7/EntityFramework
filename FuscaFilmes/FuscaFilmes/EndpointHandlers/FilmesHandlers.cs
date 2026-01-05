using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Models;
using FuscaFilmesRepo.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace FuscaFilmes.EndpointHandlers
{
    public class FilmesHandlers
    {

        public static IEnumerable<Filme>  GetFilmes(Context context)
        {
             return context.Filmes.Include(filme => filme.Diretores).OrderBy(filme => filme.Ano).ToList();
        }

        public static IEnumerable<Filme> GetFilmesById (int id, Context context) 
        {

        return context.Filmes.Where(filme => filme.Id == id). Include(filme => filme.Diretores).ToList();
    }

        public static IEnumerable<Filme> GetFilmesByNameEFFunction (string titulo, Context context) 
        {

            return context.Filmes
                .Where(filme =>
                    EF.Functions.Like(filme.Titulo, $"%{titulo}%") // Usando EF.Functions.Like para buscar se o título contém a string dada
                )
                .Include(filme => filme.Diretores)
                .ToList();
         }
        

        public static IEnumerable<Filme> GetFilmesByNameLINQ (string titulo, Context context) 
        {

            return context.Filmes
                .Where(filme =>
                    EF.Functions.Like(filme.Titulo, $"%{titulo}%") 
                )
                .Include(filme => filme.Diretores)
                .ToList();
         }

        public static void DeleteFilme (Context context, int filmeId) 
        {
            context.Filmes
            .Where(filme => filme.Id == filmeId)
            .ExecuteDelete<Filme>();

        }

        public static void UpdateFilme (Context context, FilmeUpdate filmeUpdate) 
            {
                context.Filmes
                .Where(filme => filme.Id == filmeUpdate.Id)
                .ExecuteUpdate(setter => setter
                    .SetProperty(f => f.Titulo, filmeUpdate.Titulo)
                    .SetProperty(f => f.Ano, filmeUpdate.Ano)
                    );
                

}

}
}
