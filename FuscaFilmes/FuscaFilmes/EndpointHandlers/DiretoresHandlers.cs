using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contratos;
using FuscaFilmesRepo.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace FuscaFilmes.EndpointHandlers
{
    public static class DiretoresHandlers
    {
        public static List<Diretor> GetDiretores(Context context)  //injetando o contexto via parametro, ou seja ele ja cria o contexto para mim,
        {
  
            return context.Diretores.Include(diretor => diretor.Filmes).ToList(); //lambda para incluir os filmes relacionados ao diretor
        }

        public static Diretor GetDiretorById(int DiretorId, Context context)
        {
            return context.Diretores.Where(diretor => diretor.Id == DiretorId)
             .Include(diretor => diretor.Filmes)
            .OrderBy(diretor => diretor.Name)
            .FirstOrDefault();
        }

        public static void CreateDiretor(IDiretorRepository diretorRepository, Diretor diretor)
        {
            diretorRepository.Add(diretor);
            diretorRepository.SaveChanges();
            
        }


        public static void UpdateDiretor (Context context, int diretorId)
        {
            var diretor = context.Diretores.Find(diretorId);

            if (diretor != null)
            {
                diretor.Name = "Diretor Atualizado";
                context.Update(diretor);
                context.SaveChanges();
                }

        }

        public static void DeleteDiretor (Context context, int diretorId)
        {
            var diretor = context.Diretores.Find(diretorId);
            if (diretor != null)
            {
                context.Diretores.Remove(diretor);
                context.SaveChanges();
            }
        }



    }


}













