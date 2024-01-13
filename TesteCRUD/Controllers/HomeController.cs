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
      private RegraEndereco _regraEndereco;
      private readonly string _connection;
      public HomeController(IConfiguration configuration, RegraEndereco regraEndereco, RegraPessoa regraPessoa)
        {
            _connection = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            _regraPessoa = regraPessoa;
            _regraEndereco = regraEndereco;
        }

      [HttpGet]
      public async Task<IActionResult> Index()
      {
            var listaPessoas = await _regraPessoa.CarregarListaDePessoas();
            return View(listaPessoas);
      }
   }
}