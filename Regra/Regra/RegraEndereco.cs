using Dapper;
using Microsoft.Extensions.Configuration;
using Regra.Interfaces;
using Regra.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regra.Regra
{
   public class RegraEndereco
   {
      private readonly IEnderecoRepositorio _enderecoRepositorio;

      public RegraEndereco(IEnderecoRepositorio enderecoRepositorio)
      {
         _enderecoRepositorio = enderecoRepositorio;
      }

      public async Task CreateEndereco(EnderecoModel enderecoModel)
      {
         try
         {
            await _enderecoRepositorio.CriarEndereco(enderecoModel.ModeloParaEntidade(enderecoModel));
         }
         catch (Exception) { throw; }
      }

      public async Task<EnderecoModel?> CarregarEditarEndereco(int idEndereco)
      {
         EnderecoModel enderecoModel = new EnderecoModel();
         if (idEndereco > 0)
         {
            var endereco = await _enderecoRepositorio.BuscarEnderecoPorId(idEndereco);
            enderecoModel.EntidadeParaModel(endereco);
         }
         return enderecoModel;
      }

      public async Task<int> EditarEndereco(EnderecoModel enderecoModel)
      {
         return await _enderecoRepositorio.AtualizarEndereco(enderecoModel.ModeloParaEntidade(enderecoModel));
      }

      public async Task<int> ApagarEndereco(int idEndereco)
      {
         return await _enderecoRepositorio.ApagarEnderecoPorId(idEndereco);
      }

      public async Task<List<EnderecoModel>> BuscarEnderecoPessoaPorId(int idPessoa)
      {
         var listaEndereco = await _enderecoRepositorio.BuscarEnderecoPessoaPorId(idPessoa);
         var listaEnderecoModel = new List<EnderecoModel>();

         foreach (var endereco in listaEndereco)
         {
            var modelo = new EnderecoModel();
            modelo.EntidadeParaModel(endereco);
            listaEnderecoModel.Add(modelo);
         }
         return listaEnderecoModel;
      }

      public async Task<int> ApagarTodosEnderecos(int idPessoa)
      {
         return await _enderecoRepositorio.ApagarTodosEnderecos(idPessoa);
      }
   }
}
