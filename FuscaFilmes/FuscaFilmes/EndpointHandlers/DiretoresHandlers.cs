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
  
            return  diretorRepository.GetDiretores();   
        }

        public static Diretor GetDiretorById(int DiretorId, IDiretorRepository diretorRepository)
        {
            return diretorRepository.GetDiretorById(DiretorId);
        }

        public static void CreateDiretor(IDiretorRepository diretorRepository, Diretor diretor)
        {
            diretorRepository.Add(diretor);
            diretorRepository.SaveChanges();
            
        }


        public static void UpdateDiretor (IDiretorRepository diretorRepository, Diretor diretorNovo)
        {

            diretorRepository.Update(diretorNovo);

            diretorRepository.SaveChanges();
                

        }

        public static void DeleteDiretor (IDiretorRepository diretorRepository, int diretorId)
        {
                diretorRepository.Delete(diretorId);
            diretorRepository.SaveChanges();
            }
        }



    }
















