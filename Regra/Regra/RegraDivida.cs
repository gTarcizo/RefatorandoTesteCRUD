using Dapper;
using Microsoft.Extensions.Configuration;
using Regra.Entidades;
using Regra.Interfaces;
using Regra.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Formats.Asn1;
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

      public async Task<IEnumerable<Divida>> CriarDivida(DividaModel dividaModel)
      {
         if (dividaModel.ValorDivida is 0) return Enumerable.Empty<Divida>();
         Divida divida = dividaModel.ModeloParaEntidade(dividaModel);
         return await _dividaRepositorio.CriarDivida(divida);
      }

      public async Task<List<DividaModel>> ListarDividaPorId(int idPessoa)
      {
         List<DividaModel> listaDividaModel = new List<DividaModel>();
         var listaDivida = await _dividaRepositorio.ListarDividaPorId(idPessoa);
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

      public async Task<DividaModel> CarregarDividaPorId(int idDivida)
      {
         var dividaModel = new DividaModel();
         var divida = await _dividaRepositorio.BuscarDividaPorId(idDivida);
         if (divida.IdPessoa > 0)
         {
            dividaModel.EntidadeParaModel(divida);
         }
         return dividaModel;
      }
   }
}
