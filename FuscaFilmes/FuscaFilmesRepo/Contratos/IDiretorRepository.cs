using FuscaFilmes.Domain.Entities;
using FuscaFilmesRepo.Contexts;
using System;

namespace FuscaFilmes.Repo.Contratos;

public interface IDiretorRepository
{

   Diretor GetDiretorById(int DiretorId);

     List<Diretor> GetDiretores();

    void Add(Diretor diretor);

    void Update(Diretor diretor);

    void Delete(int diretorId);

    bool SaveChanges(); 

}
