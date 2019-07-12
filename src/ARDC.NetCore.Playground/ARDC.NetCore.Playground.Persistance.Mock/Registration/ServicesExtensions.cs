using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Persistance.Mock.Generators;
using Microsoft.Extensions.DependencyInjection;

namespace ARDC.NetCore.Playground.Persistance.Mock.Registration
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Add the Model Generators to the Service Collection.
        /// </summary>
        /// <param name="services">Coleção de serviços a a</param>
        public static void AddGenerators(this IServiceCollection services)
        {
            services.AddSingleton<IModelGenerator<Game>, GameGenerator>();
            services.AddSingleton<IModelGenerator<Review>, ReviewGenerator>();
        }
    }
}
