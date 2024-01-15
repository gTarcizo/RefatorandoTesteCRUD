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
      private readonly string _connection;
      private readonly RegraEndereco _regraEndereco;
      private readonly IPessoaRepositorio _pessoaRepositorio;
      public RegraPessoa(IConfiguration configuration, IPessoaRepositorio pessoaRepositorio, RegraEndereco regraEndereco)
      {
         _connection = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
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
         using (SqlConnection con = new SqlConnection(_connection))
         {
            string query = "INSERT INTO Pessoa (Nome, CPF, Telefone) VALUES( @Nome, @CPF, @Telefone); SELECT CAST(scope_identity() AS INT);";
            return await con.QueryFirstAsync<int>(query, pessoaModel);
         }
      }

      public async Task<PessoaModel> CarregarEditar(int idPessoa)
      {
         try
         {
            PessoaModel pessoas = new PessoaModel();
            if (idPessoa > 0)
            {
               using (SqlConnection con = new SqlConnection(_connection))
               {

                  string queryPessoa = "SELECT * FROM Pessoa WHERE IdPessoa = @idPessoa";
                  pessoas = await con.QueryFirstOrDefaultAsync<PessoaModel>(queryPessoa, new { idPessoa }) ?? new PessoaModel();

                  IList<EnderecoModel> listaEnderecos;
                  string queryEndereco = "SELECT * FROM Endereco WHERE IdPessoa = @idPessoa";
                  var enderecos = await con.QueryAsync<EnderecoModel>(queryEndereco, new { idPessoa });
                  listaEnderecos = enderecos.ToList();
                  if (listaEnderecos.Count > 0) pessoas.ListaEndereco = listaEnderecos.ToList();
               }
            }
            return pessoas;
         }
         catch (Exception) { throw; }
      }

      public async Task<int> EditarPessoa(PessoaModel pessoaModel)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            string atualizarQuery = "UPDATE Pessoa SET Nome = @Nome, CPF = @CPF, Telefone = @Telefone WHERE IdPessoa = @IdPessoa";
            return await con.ExecuteAsync(atualizarQuery, pessoaModel);
         }
      }

      public async Task<int> ApagarPessoa(int idPessoa)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM Endereco WHERE IdPessoa = @idPessoa ");
            sb.Append("DELETE FROM Pessoa WHERE IdPessoa = @idPessoa ");
            return await con.ExecuteAsync(sb.ToString(), new { idPessoa });
         }
      }

      public async Task<PessoaModel> BuscarPessoaPorEndereco(int idEndereco)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT e.IdEndereco, p.* FROM Pessoa p ");
            sb.Append("INNER JOIN Endereco e ON p.IdPessoa = e.IdPessoa "); 
            sb.Append("WHERE e.IdEndereco = @idEndereco "); 
            var pessoaModel = await con.QueryFirstOrDefaultAsync<PessoaModel>(sb.ToString(), new { idEndereco });
            if (pessoaModel == null) return new PessoaModel();
            return pessoaModel;
         }
      }

   }
}
