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
      private readonly string _connection;
      private readonly IEnderecoRepositorio _enderecoRepositorio;

      public RegraEndereco(IConfiguration configuration, IEnderecoRepositorio enderecoRepositorio)
      {
         _connection = configuration.GetConnectionString("DefaultConnection")??string.Empty;
         _enderecoRepositorio = enderecoRepositorio;
      }

      public async Task CreateEndereco(EnderecoModel enderecoModel)
      {
         try
         {
            using (SqlConnection con = new SqlConnection(_connection))
            {
               const string query = "INSERT INTO Endereco (NomeEndereco, CEP, Estado, Cidade, NumeroCasa, IdPessoa) VALUES ( @NomeEndereco, @CEP, @Estado, @Cidade, @NumeroCasa, @IdPessoa)";
               await con.ExecuteAsync(query, enderecoModel);
            }
         }
         catch (Exception) { throw; }
      }

      public async Task<EnderecoModel?> CarregarEditarEndereco (int idEndereco)
      {
         EnderecoModel enderecos = new EnderecoModel();
         if (idEndereco != 0 && idEndereco > 0)
         {
            string queryEndereco = "SELECT * FROM Endereco WHERE IdEndereco = @idEndereco";
            using (SqlConnection con = new SqlConnection(_connection))
            {
               var endereco = await con.QueryFirstOrDefaultAsync<EnderecoModel>(queryEndereco, new { idEndereco });
               if (endereco != null) enderecos = endereco;
            }
         }
            return enderecos;
      }

      public async Task<int> EditarEndereco(EnderecoModel enderecoModel)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            string atualizarQuery = "UPDATE Endereco SET NomeEndereco = @NomeEndereco, CEP = @CEP, Estado = @Estado, Cidade = @Cidade, NumeroCasa = @NumeroCasa WHERE IdEndereco = @IdEndereco";
            return await con.ExecuteAsync(atualizarQuery, enderecoModel);
         }
      }

      public async Task<int> ApagarEndereco(int idEndereco)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            string query = "DELETE FROM Endereco WHERE IdEndereco = @idEndereco ";
            return await con.ExecuteAsync(query, new { idEndereco });
         }
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
   }
}
