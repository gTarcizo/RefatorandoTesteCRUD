using Regra.Entidades;
using Regra.Enum;
using System.ComponentModel.DataAnnotations;

namespace Regra.Models
{
   public class DividaModel
   {
      public DividaModel()
      {

      }

      public int IdPessoa { get; set; }
      public int IdDivida { get; set; }
      public string DescricaoDivida { get; set; }
      [Required(ErrorMessage ="Preencha pelo menos o valor da divida. Se quer fazer divida... faça direito!")]
      public float ValorDivida { get; set; }
      public float? Juros { get; set; }
      public float? ValorComJuros { get; set; }
      public DateTime DataCriacao { get; set; }
      [Required(ErrorMessage = "Preencha o tipo de calculo")]
      public TipoCalculoEnum TipoCalculo { get; set; }

      public void EntidadeParaModel(Divida endereco)
      {
         IdPessoa = endereco.IdPessoa;
         IdDivida = endereco.IdDivida;
         DescricaoDivida = endereco.DescricaoDivida;
         ValorDivida = endereco.ValorDivida;
         Juros = endereco.Juros;
         TipoCalculo = endereco.TipoCalculo;
         DataCriacao = endereco.DataCriacao;
      }
      public Divida ModeloParaEntidade(DividaModel endereco)
      {
         return new Divida()
         {
            IdPessoa = endereco.IdPessoa,
            IdDivida = endereco.IdDivida,
            DescricaoDivida = endereco.DescricaoDivida,
            ValorDivida = endereco.ValorDivida,
            Juros = endereco.Juros,
            TipoCalculo = endereco.TipoCalculo,
            DataCriacao = DateTime.Now
         };
      }
   }
}