using Regra.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Regra.Regra;

namespace TesteCRUD.Controllers
{
   public class PessoasController : Controller
   {
      private readonly string _connection;
      private RegraPessoa _regraPessoa;
      private RegraEndereco _regraEndereco;

      public PessoasController(IConfiguration configuration, RegraPessoa regraPessoa, RegraEndereco regraEndereco)
      {
         _connection = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
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
         if (!ModelState.IsValid) return View();
         return Redirect($"Editar?pessoaId={await _regraPessoa.CriarPessoa(pessoa)}");
      }

      [HttpPost]
      public async Task<IActionResult> CreateEndereco(EnderecoModel enderecoModel)
      {
         if (!ModelState.IsValid) return RedirectToAction("Editar", "Pessoas", new { enderecoModel.pessoaId });
         try
         {
            await _regraEndereco.CreateEndereco(enderecoModel);
         }
         catch (Exception) { throw; }
         return RedirectToAction("Editar", "Pessoas", new { enderecoModel.pessoaId });
      }

      [HttpGet]
      public async Task<IActionResult> Editar(int pessoaId)
      {
         try
         {
            var pessoaModel = await _regraPessoa.CarregarEditar(pessoaId);
            ViewData["EnderecoModel"] = pessoaModel.ListaEndereco;
            return View(pessoaModel);
         }
         catch (Exception) { throw; }
      }

      [HttpPost]
      public async Task<IActionResult> Editar(int pessoaId, PessoaModel pessoas)
      {
         if (pessoaId != pessoas.PessoaId) return NotFound();
         if (!ModelState.IsValid) return View();
         await _regraPessoa.EditarPessoa(pessoas);
         return Redirect("/");
      }

      [HttpGet]
      public async Task<IActionResult> EditarEndereco(int enderecoId)
      {
         try
         {
            var endereco = await _regraEndereco.CarregarEditarEndereco(enderecoId);
            return View(endereco);
         }
         catch (Exception) { throw; }
      }

      [HttpPost]
      public async Task<IActionResult> EditarEndereco(int enderecoId, EnderecoModel enderecos)
      {
         if (enderecoId != enderecos.enderecoId)
         {
            return NotFound();
         }
         if (ModelState.IsValid)
         {
            await _regraEndereco.EditarEndereco(enderecos);
            return Redirect("/");
         }
         return View();
      }

      [HttpPost]
      public async Task<IActionResult> Apagar(int pessoaId)
      {

         if (ModelState.IsValid)
         {
            await _regraPessoa.ApagarPessoa(pessoaId);
         }
         return Redirect("/");
      }

      [HttpPost]
      public async Task<IActionResult> ApagarEndereco(int enderecoId)
      {

         if (ModelState.IsValid)
         {
            try
            {
               PessoaModel pessoaModel = await _regraPessoa.BuscarPessoaPorEndereco(enderecoId);
               await _regraEndereco.ApagarEndereco(enderecoId);
               return RedirectToAction("Editar", "Pessoas", new { pessoaModel.PessoaId });
            }
            catch (Exception) { throw; }

         }
         return Redirect("/");
      }
   }
}
