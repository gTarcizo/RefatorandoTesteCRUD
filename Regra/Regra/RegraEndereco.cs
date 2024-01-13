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
               const string query = "INSERT INTO enderecos (endereco, cep, estado, cidade, numeroCasa, pessoaId) VALUES( @endereco, @cep, @estado, @cidade, @numeroCasa, @pessoaId)";
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
            string queryEndereco = "SELECT * FROM enderecos WHERE enderecoId = @enderecoId";
            using (SqlConnection con = new SqlConnection(_connection))
            {
               var endereco = await con.QueryFirstOrDefaultAsync<EnderecoModel>(queryEndereco, new { enderecoId = idEndereco });
               if (endereco != null) enderecos = endereco;
            }
         }
            return enderecos;
      }

      public async Task<int> EditarEndereco(EnderecoModel endereco)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            string atualizarQuery = "UPDATE enderecos SET endereco=@endereco, cep=@cep, estado=@estado, cidade=@cidade, numeroCasa=@numeroCasa WHERE enderecoId=@enderecoId";
            return await con.ExecuteAsync(atualizarQuery, endereco);
         }
      }

      public async Task<int> ApagarEndereco(int enderecoId)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            string query = "DELETE FROM enderecos WHERE enderecoId = @enderecoId ";
            return await con.ExecuteAsync(query, new { enderecoId });
         }
      }

      public async Task<List<EnderecoModel>> BuscarEnderecoPessoaPorId(int pessoaId)
      {
         var listaEndereco = await _enderecoRepositorio.BuscarEnderecoPessoaPorId(pessoaId);
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
