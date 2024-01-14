using Dapper;
using Microsoft.Extensions.Configuration;
using Regra.Interfaces;
using Regra.Entidades;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Repositorios
{
   public class EnderecoRepositorio : IEnderecoRepositorio
   {
      private readonly string _connection;
      public EnderecoRepositorio(IConfiguration configuration)
      {
         _connection = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
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
   }
}
