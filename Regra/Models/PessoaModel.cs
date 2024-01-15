using Regra.Entidades;
using Regra.Models;
using System.ComponentModel.DataAnnotations;

namespace Regra.Models
{
   public class PessoaModel
   {
      public PessoaModel()
      {
         ListaEndereco = new List<EnderecoModel>();
         ListaPessoa = new List<PessoaModel>();
      }
      public int IdPessoa { get; set; }

      [Required(ErrorMessage ="Preencha o campo Nome")]
      public string Nome { get; set; }

      [Required(ErrorMessage = "Preencha o campo CPF")]
      public string CPF { get; set; }

      [Required(ErrorMessage = "Preencha o campo Telefone")]
      public string Telefone { get; set; }

      public List<EnderecoModel> ListaEndereco { get; set; }

      public List<PessoaModel> ListaPessoa { get; set; }

      public int QuantidadeEndereco
      {
         get
         {
            return ListaEndereco.Count;
         }
      }
      public void EntidadeParaModel(Pessoa pessoa)
      {
         IdPessoa = pessoa.IdPessoa;
         Nome = pessoa.Nome;
         CPF = pessoa.CPF;
         Telefone = pessoa.Telefone;
      }
   }
}