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
   public class DividaRepositorio : IDividaRepositorio
   {
      private readonly string _connection;
      public DividaRepositorio(IConfiguration configuration)
      {
         _connection = configuration.GetConnectionString("DefaultConnection");
      }
      
      public async Task<List<Divida>> BuscarDividaPorId(int idPessoa)
      {
         using (var co = new SqlConnection(_connection))
         {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Divida WHERE IdPessoa = @idPessoa ");
            var listaDivida = await co.QueryAsync<Divida>(sb.ToString(), new { idPessoa });
            return listaDivida.ToList();
         }
      }
   }
}
