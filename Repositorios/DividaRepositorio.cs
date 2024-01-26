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
      
      public async Task<IEnumerable<Divida>> CriarDivida(Divida divida)
      {
         using (var co = new SqlConnection(_connection))
         {
            return await co.QueryAsync<Divida>("INSERT INTO Divida (IdPessoa, DescricaoDivida,  ValorDivida, Juros, DataCriacao, TipoCalculo ) VALUES (@IdPessoa, @DescricaoDivida, @ValorDivida, @Juros, @DataCriacao, @TipoCalculo);", divida);
         }
      }

      public async Task<List<Divida>> ListarDividaPorId(int idPessoa)
      {
         using (var co = new SqlConnection(_connection))
         {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Divida WHERE IdPessoa = @idPessoa ");
            var listaDivida = await co.QueryAsync<Divida>(sb.ToString(), new { idPessoa });
            return listaDivida.ToList();
         }
      }

      public async Task<int> AtualizarDivida(Divida divida)
      {
         using (SqlConnection con = new SqlConnection(_connection))
         {
            var sb = new StringBuilder();
            sb.Append("UPDATE Divida SET DescricaoDivida = @DescricaoDivida,  ValorDivida = @ValorDivida, Juros = @Juros, DataCriacao = @DataCriacao, TipoCalculo = @TipoCalculo");
            return await con.ExecuteAsync(sb.ToString(), divida);
         }
      }

      public async Task<Divida> BuscarDividaPorId(int idDivida)
      {
         using (var co = new SqlConnection(_connection))
         {
            var sb = new StringBuilder();
            sb.Append("SELECT * FROM Divida WHERE IdDivida = @idDivida");
            var divida = await co.QueryFirstOrDefaultAsync<Divida>(sb.ToString(), new { idDivida });
            if(divida != null) return divida;
            return new Divida();
         }
      }
   }
}
