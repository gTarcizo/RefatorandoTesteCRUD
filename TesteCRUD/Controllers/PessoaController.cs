using Regra.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Regra.Regra;
using Regra.Entidades;

namespace TesteCRUD.Controllers
{
   public class PessoaController : Controller
   {
      private RegraPessoa _regraPessoa;
      private RegraEndereco _regraEndereco;

      public PessoaController(RegraPessoa regraPessoa, RegraEndereco regraEndereco)
      {
         _regraPessoa = regraPessoa;
         _regraEndereco = regraEndereco;
      }

      [HttpGet]
      public IActionResult Create()
      {
         return View();
      }

      [HttpPost]
      public async Task<IActionResult> Create(PessoaModel pessoa)
      {
         if (string.IsNullOrEmpty(pessoa.Nome)) return View();
         return Redirect($"Editar?idPessoa={await _regraPessoa.CriarPessoa(pessoa)}");
      }

      [HttpGet]
      public async Task<IActionResult> Editar(int idPessoa)
      {
         try
         {
            var pessoaModel = await _regraPessoa.CarregarEditar(idPessoa);
            return View(pessoaModel);
         }
         catch (Exception) { throw; }
      }

      [HttpPost]
      public async Task<IActionResult> Editar(PessoaModel pessoaModel)
      {
         if (pessoaModel.IdPessoa == 0) return NotFound();
         await _regraPessoa.EditarPessoa(pessoaModel);
         return Redirect("/");
      }

      [HttpPost]
      public async Task<IActionResult> Apagar(int idPessoa)
      {
         if (ModelState.IsValid)
         {
            await _regraPessoa.ApagarPessoa(idPessoa);
         }
         return Redirect("/");
      }
   }
}
