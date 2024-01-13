using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Repositorio.Interface;
using Repositorio.Entidades;
using System.Data.SqlClient;
using Dapper;

namespace RepositorioPessoa.Repositorio
{
   internal class PessoaRepositorio: IPessoaRepositorio
   {
      private readonly string _connection;
      public PessoaRepositorio(IConfiguration configuration)
      {
         _connection = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
      }

      public async Task<List<Pessoa>> ListarPessoaTelaInicial()
      {
         using (SqlConnection co = new SqlConnection(_connection))
         {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Pessoas");
            var listaPessoas = await co.QueryAsync<Pessoa>(sb.ToString());
            return listaPessoas.ToList();
         }
      }
   }
}
