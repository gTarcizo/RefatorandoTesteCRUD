using Microsoft.Extensions.DependencyInjection;
using Regra.Interfaces;
using Repositorios;

namespace ScopedInjection
{
   public class Scopedinjection
   {
      public static void ConfigurarScoped(IServiceCollection services)
      {
            services.AddScoped<IPessoaRepositorio, PessoaRepositorio>();
            services.AddScoped<IEnderecoRepositorio, EnderecoRepositorio>();
      }
   }
}
