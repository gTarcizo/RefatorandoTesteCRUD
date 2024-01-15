using Regra.Models;
using Regra.Regra;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;


namespace TesteCRUD.Controllers
{
   public class HomeController : Controller
   {
      private RegraPessoa _regraPessoa;
      public HomeController(RegraPessoa regraPessoa)
      {
         _regraPessoa = regraPessoa;
      }

      [HttpGet]
      public async Task<IActionResult> Index()
      {
         var listaPessoas = await _regraPessoa.CarregarListaDePessoas();
         return View(listaPessoas);
      }
   }
}