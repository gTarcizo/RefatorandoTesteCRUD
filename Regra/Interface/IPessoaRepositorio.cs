﻿using Regra.Entidades;
using Regra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regra.Interfaces
{
   public interface IPessoaRepositorio
   {
      Task<IEnumerable<Pessoa>> ListarPessoas();
      Task<int> CriarPessoa(Pessoa pessoa);
   }
}
