using Regra.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Regra.Models
{
   public class EnderecoModel
   {

      public int enderecoId { get; set; }
      public string endereco { get; set; }
      public string cep { get; set; }
      public string estado { get; set; }
      public string cidade { get; set; }
      public string numeroCasa { get; set; }
      public int pessoaId { get; set; }

      public void EntidadeParaModel(Endereco enderecos)
      {
         enderecoId = enderecos.enderecoId;
         endereco = enderecos.endereco;
         cep = enderecos.cep;
         estado = enderecos.estado;
         cidade = enderecos.cidade;
         numeroCasa = enderecos.numeroCasa;
         pessoaId = enderecos.pessoaId;
      }
   }
}