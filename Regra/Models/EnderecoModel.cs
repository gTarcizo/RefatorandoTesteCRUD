using Regra.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Regra.Models
{
   public class EnderecoModel
   {

      public int IdEndereco { get; set; }

      [Required(ErrorMessage = "Endereço precisa ser preenchido!")]
      public string NomeEndereco { get; set; }

      [Required(ErrorMessage = "CEP precisa ser preenchido!")]
      public string CEP { get; set; }

      [Required(ErrorMessage = "Estado precisa ser preenchido!")]
      public string Estado { get; set; }

      [Required(ErrorMessage = "Cidade precisa ser preenchido!")]
      public string Cidade { get; set; }

      [Required(ErrorMessage = "Número do endereço precisa ser preenchido!")]
      public string NumeroCasa { get; set; }
      public int IdPessoa { get; set; }

      public void EntidadeParaModel(Endereco endereco)
      {
         IdEndereco = endereco.IdEndereco;
         NomeEndereco = endereco.NomeEndereco;
         CEP = endereco.CEP;
         Estado = endereco.Estado;
         Cidade = endereco.Cidade;
         NumeroCasa = endereco.NumeroCasa;
         IdPessoa = endereco.IdPessoa;
      }
      public Endereco ModeloParaEntidade(EnderecoModel endereco)
      {
         return new Endereco()
         {
            IdEndereco = endereco.IdEndereco,
            NomeEndereco = endereco.NomeEndereco,
            CEP = endereco.CEP,
            Estado = endereco.Estado,
            Cidade = endereco.Cidade,
            NumeroCasa = endereco.NumeroCasa,
            IdPessoa = endereco.IdPessoa,
         };
      }
   }
}