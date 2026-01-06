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

       public async Task<Diretor> GetDiretorByIdAsync(int diretorId)
        {
            return await _context.Diretores
                .Where(d => d.Id == diretorId)
                .Include(d => d.Filmes)
                .OrderBy(d => d.Name)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Diretor>> GetDiretoresAsync()
        {
            return await _context.Diretores
                .Include(d => d.Filmes)
                .ToListAsync();
        }

        public async Task AddAsync(Diretor diretor)
        {
           await _context.Diretores.AddAsync(diretor);
           
        }

        public async Task DeleteAsync(int diretorId)
        {
            var diretor = await _context.Diretores.FindAsync(diretorId);

            if (diretor != null)
            {
                _context.Diretores.Remove(diretor);
                _context.SaveChanges();
            }
        }

        public async Task UpdateAsync(Diretor diretorNovo)
        {
            var diretor = await _context.Diretores.FindAsync(diretorNovo.Id);

            if (diretor != null)
            {
                diretor.Name = diretorNovo.Name;
                _context.Update(diretor);
                _context.SaveChanges();
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    
    
    }
}
