using Microsoft.Extensions.DependencyInjection;
using Regra.Interfaces;
using Regra.Regra;
using Repositorios;

namespace ScopedInjection
{
   public class Scopedinjection
   {
      public static void ConfigurarScoped(IServiceCollection services)
      {
         #region Repositorios

         services.AddScoped<IPessoaRepositorio, PessoaRepositorio>();
         services.AddScoped<IEnderecoRepositorio, EnderecoRepositorio>();
         services.AddScoped<IDividaRepositorio, DividaRepositorio>();
         #endregion
         #region Regras
         services.AddScoped<RegraPessoa>();
         services.AddScoped<RegraEndereco>();
         services.AddScoped<RegraDivida>();
         #endregion
      }
   }
}
