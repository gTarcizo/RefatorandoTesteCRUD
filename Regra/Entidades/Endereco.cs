using System.ComponentModel.DataAnnotations;

namespace Regra.Entidades
{
   public class Endereco
   {
      public int IdEndereco { get; set; }
      public string NomeEndereco { get; set; }
      public string CEP { get; set; }
      public string Cidade { get; set; }
      public string Estado { get; set; }
      public string NumeroCasa { get; set; }
      public int IdPessoa { get; set; }
   }
}