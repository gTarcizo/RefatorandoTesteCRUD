using Regra.Models;
using System.ComponentModel.DataAnnotations;

namespace Regra.Entidades
{
   public class Divida
   {

      public int IdPessoa { get; set; }
      public int IdDivida { get; set; }
      public string DescricaoDivida { get; set; }
      public float ValorDivida { get; set; }
      public float? Juros { get; set; }
      public DateTime DataCriacao { get; set; }

   }
}