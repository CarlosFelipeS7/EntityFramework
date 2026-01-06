using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contratos;
using FuscaFilmes.Repo.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace FuscaFilmes.EndpointHandlers
{
    public static class DiretoresHandlers
    {
        public static List<Diretor> GetDiretores(IDiretorRepository diretorRepository)  //injetando o contexto via parametro, ou seja ele ja cria o contexto para mim,
        {
  
            return  diretorRepository.GetDiretoresAsync();   
        }

        public static Diretor GetDiretorById(int DiretorId, IDiretorRepository diretorRepository)
        {
            return diretorRepository.GetDiretorByIdAsync(DiretorId);
        }

        public static void CreateDiretor(IDiretorRepository diretorRepository, Diretor diretor)
        {
            diretorRepository.AddAsync(diretor);
            diretorRepository.SaveChangesAsync();
            
        }


        public static void UpdateDiretor (IDiretorRepository diretorRepository, Diretor diretorNovo)
        {

            diretorRepository.UpdateAsync(diretorNovo);

            diretorRepository.SaveChangesAsync();
                

        }

        public static void DeleteDiretor (IDiretorRepository diretorRepository, int diretorId)
        {
                diretorRepository.DeleteAsync(diretorId);
            diretorRepository.SaveChangesAsync();
            }
        }



    }
















