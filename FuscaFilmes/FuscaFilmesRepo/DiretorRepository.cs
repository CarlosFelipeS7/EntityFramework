using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contratos;
using FuscaFilmesRepo.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuscaFilmes.Repo
{
    public class DiretorRepository(Context _context) : IDiretorRepository

     {
        private readonly Context Context = _context;


        public Diretor GetDiretorById(int DiretorId)
        {
            return Context.Diretores.Where(diretor => diretor.Id == DiretorId)
              .Include(diretor => diretor.Filmes)
             .OrderBy(diretor => diretor.Name)
             .FirstOrDefault();
        }

        public List<Diretor> GetDiretores()
        {
          return  Context.Diretores.Include(diretor => diretor.Filmes).ToList();
        }


        public void Add (Diretor diretor)
        {
            _context.Diretores.Add(diretor);
            _context.SaveChanges();
            
        }

        public void Delete( int diretorId)
        {
            var diretor = _context.Diretores.Find(diretorId);
            if (diretor != null)
            {
                _context.Diretores.Remove(diretor);
                _context.SaveChanges();
            }
        }

        public void Update(Diretor diretorNovo)
        {
            var diretor = Context.Diretores.Find(diretorNovo.Id);

            if (diretor != null)
            {
                diretor.Name = "Diretor Atualizado";
                Context.Update(diretor);
                Context.SaveChanges();
            }

        }
        

        public bool SaveChanges()
        {
            return true;
        }

        
    }
}
