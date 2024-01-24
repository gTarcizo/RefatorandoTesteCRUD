using Regra.Models;
using System.ComponentModel.DataAnnotations;

namespace Regra.Entidades
{
   public class Pessoa
   {
      public int IdPessoa { get; set; }
      public string Nome { get; set; }
      public string CPF { get; set; }
      public string Telefone { get; set; }
      public string Email { get; set; }
   }
}