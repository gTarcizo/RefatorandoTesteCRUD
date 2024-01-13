using Regra.Models;
using System.ComponentModel.DataAnnotations;

namespace Regra.Entidades
{
   public class Pessoa
   {
      public int PessoaId { get; set; }
      public string nome { get; set; }
      public string cpf { get; set; }
      public string telefone { get; set; }
   }
}