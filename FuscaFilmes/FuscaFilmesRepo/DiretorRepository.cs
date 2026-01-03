using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contratos;
using FuscaFilmesRepo.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuscaFilmes.Repo
{
    public class DiretorRepository(Context context) : IDiretorRepository

     {
        private readonly Context context = context;

        public void Add (Diretor diretor)
        {
            context.Diretores.Add(diretor);
            context.SaveChanges();
            
        }

        public void Delete(int diretorId)
        {
            throw new NotImplementedException();
        }

      
        public void Update(Diretor diretor)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return true;
        }

    }
}
