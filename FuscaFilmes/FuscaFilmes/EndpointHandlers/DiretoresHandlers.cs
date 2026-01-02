using FuscaFilmes.DbContexts;
using FuscaFilmes.Entities;
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

        public static Diretor CreateDiretor(Diretor diretor, Context context)
        {
            context.Diretores.Add(diretor);
            context.SaveChanges();
            return diretor;
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




