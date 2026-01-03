using FuscaFilmes.Domain.Entities;
using System;

namespace FuscaFilmes.Repo.Contratos;

public interface IDiretorRepository
{
    void Add(Diretor diretor);

    void Update(Diretor diretor);

    void Delete(int diretorId);

    bool SaveChanges(); 

}
