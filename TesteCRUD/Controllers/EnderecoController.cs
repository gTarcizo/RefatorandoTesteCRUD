using Microsoft.AspNetCore.Mvc;
using Regra.Models;
using Regra.Regra;

namespace TesteCRUD.Controllers
{
   public class EnderecoController : Controller
   {
      private RegraEndereco _regraEndereco;
      private RegraPessoa _regraPessoa;
      public EnderecoController(RegraEndereco regraEndereco, RegraPessoa regraPessoa)
      {
         _regraEndereco = regraEndereco;
         _regraPessoa = regraPessoa;
      }

      [HttpPost]
      public async Task<IActionResult> CreateEndereco(EnderecoModel endereco)
      {
         if (!ModelState.IsValid) return RedirectToAction("Editar", "Pessoa", new { endereco.IdPessoa });
         try
         {
            await _regraEndereco.CreateEndereco(endereco);
         }
         catch (Exception) { throw; }
         return RedirectToAction("Editar", "Pessoa", new { endereco.IdPessoa });
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
            return RedirectToAction("Editar", "Pessoa", new { pessoaModel.IdPessoa });
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
               return RedirectToAction("Editar", "Pessoa", new { pessoaModel.IdPessoa });
            }
            catch (Exception) { throw; }

         }
         return Redirect("/");
      }
   }
}
