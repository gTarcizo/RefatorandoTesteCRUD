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
            pessoaModel.ListaEndereco = await _regraEndereco.BuscarEnderecoPessoaPorId(pessoaModel.PessoaId);
         }
         return listaPessoaModel.ToList();
      }

      public async Task<int> CriarPessoa(PessoaModel pessoa)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            string query = "INSERT INTO pessoas (nome, cpf, telefone) VALUES( @nome, @cpf, @telefone); SELECT CAST(scope_identity() AS INT);";
            return await con.QueryFirstAsync<int>(query, pessoa);
         }
      }

      public async Task<PessoaModel> CarregarEditar(int pessoaId)
      {
         try
         {
            PessoaModel pessoas = new PessoaModel();
            if (pessoaId > 0)
            {
               using (SqlConnection con = new SqlConnection(_connection))
               {

                  string queryPessoa = "SELECT * FROM pessoas WHERE pessoaId = @pessoaId";
                  pessoas = await con.QueryFirstOrDefaultAsync<PessoaModel>(queryPessoa, new { pessoaId = pessoaId }) ?? new PessoaModel();

                  IList<EnderecoModel> listaEnderecos;
                  string queryEndereco = "SELECT * FROM enderecos WHERE pessoaId = @pessoaId";
                  var enderecos = await con.QueryAsync<EnderecoModel>(queryEndereco, new { pessoaId = pessoaId });
                  listaEnderecos = enderecos.ToList();
                  if (listaEnderecos.Count > 0) pessoas.ListaEndereco = listaEnderecos.ToList();
               }
            }
            return pessoas;
         }
         catch (Exception) { throw; }
      }

      public async Task<int> EditarPessoa(PessoaModel pessoas)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            string atualizarQuery = "UPDATE pessoas SET nome=@nome, cpf=@cpf, telefone=@telefone WHERE pessoaId=@pessoaId";
            return await con.ExecuteAsync(atualizarQuery, pessoas);
         }
      }

      public async Task<int> ApagarPessoa(int idPessoa)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM enderecos WHERE pessoaId = @idPessoa ");
            sb.Append("DELETE FROM pessoas WHERE pessoaId = @idPessoa ");
            return await con.ExecuteAsync(sb.ToString(), new { idPessoa });
         }
      }

      public async Task<PessoaModel> BuscarPessoaPorEndereco(int idEndereco)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT e.enderecoId, p.* FROM pessoas p ");
            sb.Append("INNER JOIN enderecos e ON p.pessoaId = e.pessoaId "); 
            sb.Append("WHERE e.enderecoId = @enderecoId "); 
            var pessoaModel = await con.QueryFirstOrDefaultAsync<PessoaModel>(sb.ToString(), new { enderecoId =idEndereco });
            if (pessoaModel == null) return new PessoaModel();
            return pessoaModel;
         }
      }

   }
}
