using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TesteContasPagar.Dal;

namespace TesteContasPagar.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ContasPagarContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ContasPagarContext>>()))
            {
                // verifica se as regras ja existem
                if (context.RegraAtraso.Any())
                {
                    return;   // Regras existem
                }

                context.RegraAtraso.AddRange(

                    new RegraAtraso
                    {
                        Descricao = "Até 3 dias",
                        DiasAtraso = 3,
                        Multa = 2,
                        JurosDia = 0.1M
                    },

                     new RegraAtraso
                     {
                         Descricao = "Superior a 3 dias",
                         DiasAtraso = 5,
                         Multa = 2,
                         JurosDia = 0.1M
                     },
                      new RegraAtraso
                      {
                          Descricao = "Superior a 5 dias",
                          DiasAtraso = 6,
                          Multa = 2,
                          JurosDia = 0.1M
                      }
                );
                context.SaveChanges();
            }
        }
    }
}
