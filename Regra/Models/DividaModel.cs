using Regra.Entidades;
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
      public float ValorDivida { get; set; }
      public float? Juros { get; set; }
      public float? ValorComJuros { get; set; }
      public DateTime DataCriacao { get; set; }

      public void EntidadeParaModel(Divida endereco)
      {
         IdPessoa = endereco.IdPessoa;
         IdDivida = endereco.IdDivida;
         DescricaoDivida = endereco.DescricaoDivida;
         ValorDivida = endereco.ValorDivida;
         Juros = endereco.Juros;
         DataCriacao = endereco.DataCriacao;
      }
      public Divida ModeloParaEntidade(Divida endereco)
      {
         return new Divida()
         {
            IdPessoa = endereco.IdPessoa,
            IdDivida = endereco.IdDivida,
            DescricaoDivida = endereco.DescricaoDivida,
            ValorDivida = endereco.ValorDivida,
            Juros = endereco.Juros,
            DataCriacao=endereco.DataCriacao
         };
      }
   }
}