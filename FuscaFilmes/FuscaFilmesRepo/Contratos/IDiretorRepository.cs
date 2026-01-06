using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;
using System;

namespace FuscaFilmes.Repo.Contratos;

public interface IDiretorRepository
{

   Diretor GetDiretorByIdAsync(int DiretorId);

     List<Diretor> GetDiretoresAsync();

    void AddAsync(Diretor diretor);

    void UpdateAsync(Diretor diretor);

    void DeleteAsync(int diretorId);

    bool SaveChangesAsync(); 

}
