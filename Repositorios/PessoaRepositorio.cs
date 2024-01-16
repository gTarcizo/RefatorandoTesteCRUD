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
            string query = "INSERT INTO Pessoa (Nome, CPF, Telefone) VALUES( @Nome, @CPF, @Telefone); SELECT CAST(scope_identity() AS INT);";
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
   }
}
