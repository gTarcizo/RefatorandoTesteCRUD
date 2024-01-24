using Regra.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Regra.Regra;
using Regra.Entidades;

namespace TesteCRUD.Controllers
{
   public class PessoasController : Controller
   {
      private RegraPessoa _regraPessoa;
      private RegraEndereco _regraEndereco;

      public PessoasController(RegraPessoa regraPessoa, RegraEndereco regraEndereco)
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

      [HttpPost]
      public async Task<IActionResult> CreateEndereco(EnderecoModel endereco)
      {
         if (!ModelState.IsValid) return RedirectToAction("Editar", "Pessoas", new { endereco.IdPessoa });
         try
         {
            await _regraEndereco.CreateEndereco(endereco);
         }
         catch (Exception) { throw; }
         return RedirectToAction("Editar", "Pessoas", new { endereco.IdPessoa });
      }

      [HttpGet]
      public async Task<IActionResult> Editar(int idPessoa)
      {
         try
         {
            var pessoaModel = await _regraPessoa.CarregarEditar(idPessoa);
            ViewData["EnderecoModel"] = pessoaModel.ListaEndereco;
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

      [HttpGet]
      public async Task<IActionResult> EditarEndereco(int IdEndereco)
      {
         try
         {
            var endereco = await _regraEndereco.CarregarEditarEndereco(IdEndereco);
            return View(endereco);
         }
         catch (Exception) { throw; }
      }

      [HttpPost]
      public async Task<IActionResult> EditarEndereco(EnderecoModel enderecoModel)
      {
         if (enderecoModel.IdEndereco == 0)
         {
            return NotFound();
         }
         if (ModelState.IsValid)
         {
            await _regraEndereco.EditarEndereco(enderecoModel);
            var pessoaModel = await _regraPessoa.BuscarPessoaPorEndereco(enderecoModel.IdEndereco);
            return RedirectToAction("Editar", "Pessoas", new { pessoaModel.IdPessoa });
         }
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

      [HttpPost]
      public async Task<IActionResult> ApagarEndereco(int idEndereco)
      {

         if (ModelState.IsValid)
         {
            try
            {
               PessoaModel pessoaModel = await _regraPessoa.BuscarPessoaPorEndereco(idEndereco);
               await _regraEndereco.ApagarEndereco(idEndereco);
               return RedirectToAction("Editar", "Pessoas", new { pessoaModel.IdPessoa });
            }
            catch (Exception) { throw; }

         }
         return Redirect("/");
      }
   }
}
