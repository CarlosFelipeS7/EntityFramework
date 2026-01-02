using FuscaFilmes.DbContexts;
using FuscaFilmes.Entities;
using FuscaFilmes.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace FuscaFilmes.EndpointHandlers
{
    public class FilmesHandlers
    {

        public static List<Filme>  GetFilmes(Context context)
        {
             return context.Filmes.Include(filme => filme.Diretor).OrderBy(filme => filme.Ano).ToList();
        }

        public static List<Filme> GetFilmesById (int id, Context context) 
        {

        return context.Filmes.Where(filme => filme.Id == id). Include(filme => filme.Diretor).ToList();
    }

        public static List<Filme> GetFilmesByNameEFFunction (string titulo, Context context) 
        {

            return context.Filmes
                .Where(filme =>
                    EF.Functions.Like(filme.Titulo, $"%{titulo}%") // Usando EF.Functions.Like para buscar se o título contém a string dada
                )
                .Include(filme => filme.Diretor)
                .ToList();
         }
        

        public static List<Filme> GetFilmesByNameLINQ (string titulo, Context context) 
        {

            return context.Filmes
                .Where(filme =>
                    EF.Functions.Like(filme.Titulo, $"%{titulo}%") 
                )
                .Include(filme => filme.Diretor)
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
