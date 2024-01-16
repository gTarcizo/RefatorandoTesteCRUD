using Dapper;
using Microsoft.Extensions.Configuration;
using Regra.Entidades;
using Regra.Interfaces;
using Regra.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regra.Regra
{
   public class RegraPessoa
   {
      private readonly RegraEndereco _regraEndereco;
      private readonly IPessoaRepositorio _pessoaRepositorio;
      public RegraPessoa(IPessoaRepositorio pessoaRepositorio, RegraEndereco regraEndereco)
      {
         _pessoaRepositorio = pessoaRepositorio;
         _regraEndereco = regraEndereco;
      }

      public async Task<List<PessoaModel>> CarregarListaDePessoas()
      {
         var listaPessoa = await _pessoaRepositorio.ListarPessoas();
         var listaPessoaModel = new List<PessoaModel>();

         foreach (var pessoa in listaPessoa)
         {
            var modelo = new PessoaModel();
            modelo.EntidadeParaModel(pessoa);
            listaPessoaModel.Add(modelo);
         }

         foreach (var pessoaModel in listaPessoaModel)
         {
            pessoaModel.ListaEndereco = await _regraEndereco.BuscarEnderecoPessoaPorId(pessoaModel.IdPessoa);
         }
         return listaPessoaModel.ToList();
      }

      public async Task<int> CriarPessoa(PessoaModel pessoaModel)
      {
         return await _pessoaRepositorio.CriarPessoa(pessoaModel.ModeloParaEntidade(pessoaModel));
      }

      public async Task<PessoaModel> CarregarEditar(int idPessoa)
      {
         try
         {
            PessoaModel pessoaModel = new PessoaModel();
            if (idPessoa > 0)
            {
               var pessoa = await _pessoaRepositorio.BuscarPessoaPorId(idPessoa);
               pessoaModel.EntidadeParaModel(pessoa);
               pessoaModel.ListaEndereco = await _regraEndereco.BuscarEnderecoPessoaPorId(idPessoa);
            }
            return pessoaModel;
         }
         catch (Exception) { throw; }
      }

      public async Task<int> EditarPessoa(PessoaModel pessoaModel)
      {
         return  await _pessoaRepositorio.AtualizarPessoa(pessoaModel.ModeloParaEntidade(pessoaModel));
      }

      public async Task<int> ApagarPessoa(int idPessoa)
      {
         await _regraEndereco.ApagarTodosEnderecos(idPessoa);
          return await _pessoaRepositorio.ApagarPessoa(idPessoa);
      }

      public async Task<PessoaModel> BuscarPessoaPorEndereco(int idEndereco)
      {
         var pessoa = await _pessoaRepositorio.BuscarPessoaPorIdEndereco(idEndereco);
         PessoaModel pessoaModel = new PessoaModel();
         pessoaModel.EntidadeParaModel(pessoa);
         return pessoaModel;
      }

   }
}
