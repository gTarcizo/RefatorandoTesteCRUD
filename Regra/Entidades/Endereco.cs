using System.ComponentModel.DataAnnotations;

namespace Regra.Entidades
{
   public class Endereco
   {
      public int enderecoId { get; set; }
      public string endereco { get; set; }
      public string cep { get; set; }
      public string estado { get; set; }
      public string cidade { get; set; }
      public string numeroCasa { get; set; }
      public int pessoaId { get; set; }
   }
}