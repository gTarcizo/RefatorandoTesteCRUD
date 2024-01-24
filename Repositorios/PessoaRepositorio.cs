using Regra.Entidades;
using Regra.Interfaces;
using Dapper;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using Regra.Models;
namespace Repositorios
{
   public class PessoaRepositorio : IPessoaRepositorio
   {
      private readonly string _connection;
      public PessoaRepositorio(IConfiguration configuration)
      {
         _connection = configuration.GetConnectionString("DefaultConnection");
      }
      public async Task<IEnumerable<Pessoa>> ListarPessoas()
      {
         using (SqlConnection co = new SqlConnection(_connection))
         {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Pessoa");
            return await co.QueryAsync<Pessoa>(sb.ToString());
         }
      }

      public async Task<int> CriarPessoa(Pessoa pessoa)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            string query = "INSERT INTO Pessoa (Nome, CPF, Telefone, Email) VALUES( @Nome, @CPF, @Telefone, @Email); SELECT CAST(scope_identity() AS INT);";
            return await con.QueryFirstAsync<int>(query, pessoa);
         }
      }

      public async Task<Pessoa> BuscarPessoaPorId(int idPessoa)
      {
         using (SqlConnection co = new SqlConnection(_connection))
         {
            string queryPessoa = "SELECT * FROM Pessoa WHERE IdPessoa = @idPessoa";
            var pessoa = await co.QueryFirstOrDefaultAsync<Pessoa>(queryPessoa, new { idPessoa });
            if (pessoa != null)
            {
               return pessoa;
            }
            return new Pessoa();
         }
      }

      public async Task<int> AtualizarPessoa(Pessoa pessoa)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            string atualizarQuery = "UPDATE Pessoa SET Nome = @Nome, CPF = @CPF, Telefone = @Telefone, Email = @Email WHERE IdPessoa = @IdPessoa";
            return await con.ExecuteAsync(atualizarQuery, pessoa);
         }
      }

      public async Task<int> ApagarPessoa(int idPessoa)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM Pessoa WHERE IdPessoa = @idPessoa ");
            return await con.ExecuteAsync(sb.ToString(), new { idPessoa });
         }
      }
      
      public async Task<Pessoa> BuscarPessoaPorIdEndereco(int idEndereco)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT e.IdEndereco, p.* FROM Pessoa p ");
            sb.Append("INNER JOIN Endereco e ON p.IdPessoa = e.IdPessoa ");
            sb.Append("WHERE e.IdEndereco = @idEndereco ");
            var pessoa = await con.QueryFirstOrDefaultAsync<Pessoa>(sb.ToString(), new { idEndereco });
            if (pessoa == null) return new Pessoa();
            return pessoa;
         }
      }
   }
}
