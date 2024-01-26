using Regra.Entidades;
using Regra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regra.Interfaces
{
   public interface IDividaRepositorio
   {
      Task<IEnumerable<Divida>> CriarDivida(Divida divida);
      Task<List<Divida>> ListarDividaPorId(int idPessoa);
      Task<int> AtualizarDivida(Divida divida);
      Task<Divida> BuscarDividaPorId(int idDivida);
   }
}
