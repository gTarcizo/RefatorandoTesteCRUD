using Microsoft.AspNetCore.Mvc;
using Regra.Entidades;
using Regra.Models;
using Regra.Regra;

namespace TesteCRUD.Controllers
{
   public class DividaController : Controller
   {
      private RegraDivida _regraDivida;
     public DividaController(RegraDivida regraDivida)
      {
         _regraDivida = regraDivida;
      }

      [HttpPost]
      public async Task<IActionResult> CreateDivida(DividaModel divida)
      {
         await _regraDivida.CriarDivida(divida);
         return RedirectToAction("Editar", "Pessoa", new { divida.IdPessoa });
      }

      [HttpGet]
      public async Task<IActionResult> EditarDivida(int idDivida)
      {
         try
         {
            var divida = await _regraDivida.CarregarDividaPorId(idDivida);
            return View(divida);
         }
         catch (Exception) { throw; }
      }
   }
}
