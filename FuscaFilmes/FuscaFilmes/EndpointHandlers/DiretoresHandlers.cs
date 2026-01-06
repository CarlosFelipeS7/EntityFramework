using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contratos;
using FuscaFilmes.Repo.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace FuscaFilmes.EndpointHandlers
{
    public static class DiretoresHandlers
    {
        public static async Task<IEnumerable<Diretor>> GetDiretoresAsync(IDiretorRepository diretorRepository)  //injetando o contexto via parametro, ou seja ele ja cria o contexto para mim,
        {
  
            return await  diretorRepository.GetDiretoresAsync();   
        }

        public static async Task<Diretor> GetDiretorByIdAsync(int DiretorId, IDiretorRepository diretorRepository)
        {
            return await diretorRepository.GetDiretorByIdAsync(DiretorId);
        }

        public static async Task CreateDiretorAsync(IDiretorRepository diretorRepository, Diretor diretor)
        {
            await diretorRepository.AddAsync(diretor);
            await diretorRepository.SaveChangesAsync();
            
        }


        public static async Task UpdateDiretorAsync (IDiretorRepository diretorRepository, Diretor diretorNovo)
        {

            await diretorRepository.UpdateAsync(diretorNovo);

            await diretorRepository.SaveChangesAsync();
                

        }

        public static async Task DeleteDiretorAsync (IDiretorRepository diretorRepository, int diretorId)
        {
            await diretorRepository.DeleteAsync(diretorId);
            await diretorRepository.SaveChangesAsync();
            }
        }



    }
















