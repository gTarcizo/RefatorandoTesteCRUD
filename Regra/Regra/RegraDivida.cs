using Dapper;
using Microsoft.Extensions.Configuration;
using Regra.Entidades;
using Regra.Interfaces;
using Regra.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regra.Regra
{
   public class RegraDivida
   {

      private readonly IDividaRepositorio _dividaRepositorio;
      public RegraDivida(IDividaRepositorio dividaRepositorio)
      {
         _dividaRepositorio = dividaRepositorio;
      }

      public async Task<List<DividaModel>> BuscarDividaPorId(int idPessoa)
      {
         List<DividaModel> listaDividaModel = new List<DividaModel>();
         var listaDivida = await _dividaRepositorio.BuscarDividaPorId(idPessoa);
         if (listaDivida.Count > 0)
         {
            foreach (var divida in listaDivida)
            {
               DividaModel dividaModel = new DividaModel();
               dividaModel.EntidadeParaModel(divida);
               listaDividaModel.Add(dividaModel);
            }
         }
         return listaDividaModel;
      }

   }
}
