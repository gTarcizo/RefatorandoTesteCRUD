using Dapper;
using Microsoft.Extensions.Configuration;
using Regra.Interfaces;
using Regra.Entidades;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Regra.Models;

namespace Repositorios
{
   public class EnderecoRepositorio : IEnderecoRepositorio
   {
      private readonly string _connection;
      public EnderecoRepositorio(IConfiguration configuration)
      {
         _connection = configuration.GetConnectionString("DefaultConnection");
      }
      public async Task<List<Endereco>> BuscarEnderecoPessoaPorId(int idPessoa)
      {
         using (var co = new SqlConnection(_connection))
         {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Endereco WHERE IdPessoa = @idPessoa ");
            var listaEndereco = await co.QueryAsync<Endereco>(sb.ToString(), new { idPessoa });
            return listaEndereco.ToList();
         }
      }
      public async Task<int> ApagarTodosEnderecos(int idPessoa)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM Endereco WHERE IdPessoa = @idPessoa ");
            return await con.ExecuteAsync(sb.ToString(), new { idPessoa });
         }
      }

      public async Task<int> CriarEndereco(Endereco endereco)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            const string query = "INSERT INTO Endereco (NomeEndereco, CEP, Estado, Cidade, NumeroCasa, IdPessoa) VALUES ( @NomeEndereco, @CEP, @Estado, @Cidade, @NumeroCasa, @IdPessoa)";
            return await con.ExecuteAsync(query, endereco);
         }
      }

      public async Task<Endereco> BuscarEnderecoPorId(int idEndereco)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            string queryEndereco = "SELECT * FROM Endereco WHERE IdEndereco = @idEndereco";
            var endereco = await con.QueryFirstOrDefaultAsync<Endereco>(queryEndereco, new { idEndereco });
            if(endereco != null) return endereco;
            return new Endereco();
         }
      }

      public async Task<int> AtualizarEndereco(Endereco endereco)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            string atualizarQuery = "UPDATE Endereco SET NomeEndereco = @NomeEndereco, CEP = @CEP, Estado = @Estado, Cidade = @Cidade, NumeroCasa = @NumeroCasa WHERE IdEndereco = @IdEndereco";
            return await con.ExecuteAsync(atualizarQuery, endereco);
         }
      }

      public async Task<int> ApagarEnderecoPorId(int idEndereco)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            string query = "DELETE FROM Endereco WHERE IdEndereco = @idEndereco ";
            return await con.ExecuteAsync(query, new { idEndereco });
         }
      }
   }
}
