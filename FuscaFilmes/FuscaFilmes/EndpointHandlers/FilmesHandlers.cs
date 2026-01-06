using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Models;
using FuscaFilmes.Repo.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace FuscaFilmes.EndpointHandlers
{
    public class FilmesHandlers
    {

        public static async Task<IEnumerable<Filme>> GetFilmes(Context context)  //assync task diz que é uma operação assíncrona 
        {
             return await context.Filmes.Include(filme => filme.Diretores).OrderBy(filme => filme.Ano).ToListAsync();  //await espera a operação assíncrona ser concluída antes de continuar
        }

        public static async Task<IEnumerable<Filme>> GetFilmesById (int id, Context context) 
        {

        return await context.Filmes.Where(filme => filme.Id == id). Include(filme => filme.Diretores).ToListAsync();
    }

        public static async Task<IEnumerable<Filme>> GetFilmesByNameEFFunctionAsync (string titulo, Context context) 
        {

            return await context.Filmes
                .Where(filme =>
                    EF.Functions.Like(filme.Titulo, $"%{titulo}%") // Usando EF.Functions.Like para buscar se o título contém a string dada
                )
                .Include(filme => filme.Diretores)
                .ToListAsync();
         }
        

        public static async Task<IEnumerable<Filme>> GetFilmesByNameLINQAsync (string titulo, Context context) 
        {

            return await context.Filmes
                .Where(filme =>
                    EF.Functions.Like(filme.Titulo, $"%{titulo}%") 
                )
                .Include(filme => filme.Diretores)
                .ToListAsync();
         }

        public static async Task DeleteFilmeAsync (Context context, int filmeId) 
        {
           await context.Filmes
            .Where(filme => filme.Id == filmeId)
            .ExecuteDeleteAsync<Filme>();

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
