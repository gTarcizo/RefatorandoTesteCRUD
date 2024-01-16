using Regra.Entidades;
using Regra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regra.Interfaces
{
   public interface IEnderecoRepositorio
   {
      Task<List<Endereco>> BuscarEnderecoPessoaPorId(int idPessoa);
      Task<int> ApagarTodosEnderecos(int idPessoa);
      Task<int> CriarEndereco(Endereco endereco);
      Task<Endereco> BuscarEnderecoPorId(int idEndereco);
      Task<int> AtualizarEndereco(Endereco endereco);
      Task<int> ApagarEnderecoPorId(int idEndereco);

   }
}
