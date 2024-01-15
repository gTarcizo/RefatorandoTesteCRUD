using Regra.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Regra.Models
{
   public class EnderecoModel
   {

      public int IdEndereco { get; set; }
      public string NomeEndereco { get; set; }
      public string CEP { get; set; }
      public string Estado { get; set; }
      public string Cidade { get; set; }
      public string NumeroCasa { get; set; }
      public int IdPessoa { get; set; }

      public void EntidadeParaModel(Endereco enderecos)
      {
         IdEndereco = enderecos.IdEndereco;
         NomeEndereco = enderecos.NomeEndereco;
         CEP = enderecos.CEP;
         Estado= enderecos.Estado;
         Cidade = enderecos.Cidade;
         NumeroCasa = enderecos.NumeroCasa;
         IdPessoa = enderecos.IdPessoa;
      }
   }
}