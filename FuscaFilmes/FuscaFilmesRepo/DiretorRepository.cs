using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contratos;
using FuscaFilmes.Repo.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FuscaFilmes.Repo
{
    public class DiretorRepository : IDiretorRepository
    {
        private readonly Context _context;

        public DiretorRepository(Context context)
        {
            _context = context;
        }

        public Diretor GetDiretorById(int diretorId)
        {
            return _context.Diretores
                .Where(d => d.Id == diretorId)
                .Include(d => d.Filmes)
                .OrderBy(d => d.Name)
                .FirstOrDefault();
        }

        public List<Diretor> GetDiretores()
        {
            return _context.Diretores
                .Include(d => d.Filmes)
                .ToList();
        }

        public void Add(Diretor diretor)
        {
            _context.Diretores.Add(diretor);
            _context.SaveChanges();
        }

        public void Delete(int diretorId)
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
            var diretor = _context.Diretores.Find(diretorNovo.Id);

            if (diretor != null)
            {
                diretor.Name = diretorNovo.Name;
                _context.Update(diretor);
                _context.SaveChanges();
            }
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
