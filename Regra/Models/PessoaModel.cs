using Regra.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Regra.Models
{
   public class PessoaModel
   {
      public PessoaModel()
      {
         ListaEndereco = new List<EnderecoModel>();
         ListaPessoa = new List<PessoaModel>();
         ListaDivida = new List<DividaModel>();
      }
      public int IdPessoa { get; set; }

      [Required(ErrorMessage = "Preencha o campo Nome")]
      public string Nome { get; set; }
      public string CPF { get; set; }
      public string Telefone { get; set; }
      public string Email { get; set; }
      public List<PessoaModel> ListaPessoa { get; set; }
      public EnderecoModel? Endereco { get; set; }
      public List<EnderecoModel> ListaEndereco { get; set; }
      public int QuantidadeEndereco
      {
         get
         {
            return ListaEndereco.Count;
         }
      }
      public DividaModel Divida { get; set; }

      public List<DividaModel> ListaDivida { get; set; }

      public int QuantidadeDivida
      {
         get
         {
            return ListaDivida.Count;
         }
      }
      public void EntidadeParaModel(Pessoa pessoa)
      {
         IdPessoa = pessoa.IdPessoa;
         Nome = pessoa.Nome;
         CPF = pessoa.CPF;
         Telefone = pessoa.Telefone;
         Email = pessoa.Email;
      }
      public Pessoa ModeloParaEntidade(PessoaModel pessoa)
      {
         return new Pessoa()
         {
            IdPessoa = pessoa.IdPessoa,
            Nome = pessoa.Nome,
            CPF = pessoa.CPF,
            Telefone = pessoa.Telefone,
            Email = pessoa.Email
         };
      }
   }
}