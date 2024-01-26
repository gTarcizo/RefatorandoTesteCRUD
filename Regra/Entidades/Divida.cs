using Regra.Enum;
using Regra.Models;
using System.ComponentModel.DataAnnotations;

namespace Regra.Entidades
{
   public class Divida
   {

      public int IdDivida { get; set; }
      public int IdPessoa { get; set; }
      public string DescricaoDivida { get; set; }
      public float ValorDivida { get; set; }
      public float? Juros { get; set; }
      public DateTime DataCriacao { get; set; }
      public TipoCalculoEnum TipoCalculo { get; set; }

   }
}