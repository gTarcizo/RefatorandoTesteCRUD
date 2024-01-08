using Regra.Models;

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
      public string nome { get; set; }
      public string cpf { get; set; }
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
   }
}