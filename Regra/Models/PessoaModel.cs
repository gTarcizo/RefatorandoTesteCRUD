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
      public int PessoaId { get; set; }
      [Required(ErrorMessage ="Preencha o campo Nome")]
      public string nome { get; set; }
      [Required(ErrorMessage = "Preencha o campo CPF")]
      public string cpf { get; set; }
      [Required(ErrorMessage = "Preencha o campo Telefone")]
      public string telefone { get; set; }
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
         PessoaId = pessoa.PessoaId;
         nome = pessoa.nome;
         cpf = pessoa.cpf;
         telefone = pessoa.telefone;
      }
   }
}