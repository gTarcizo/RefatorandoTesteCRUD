using Regra.Entidades;
using Regra.Models;
using Regra.Interfaces;
using Dapper;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
namespace Repositorios
{
   public class PessoaRepositorio : IPessoaRepositorio
   {
      private readonly string _connection;
      public PessoaRepositorio(IConfiguration configuration)
      {
         _connection = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
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
   }
}
