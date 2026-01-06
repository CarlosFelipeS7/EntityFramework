using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;
using System;

namespace FuscaFilmes.Repo.Contratos;


public interface IDiretorRepository
{
    Task<Diretor?> GetDiretorByIdAsync(int id);
    Task<List<Diretor>> GetDiretoresAsync();

    Task AddAsync(Diretor diretor);
    Task UpdateAsync(Diretor diretor);
    Task DeleteAsync(int id);

    Task<bool> SaveChangesAsync();
}
