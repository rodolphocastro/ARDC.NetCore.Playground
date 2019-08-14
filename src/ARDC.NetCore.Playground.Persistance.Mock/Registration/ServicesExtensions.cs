using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistence.Mock;
using ARDC.NetCore.Playground.Persistence.Mock.Generators;
using ARDC.NetCore.Playground.Persistence.Mock.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Add the Model Generators to the Service Collection.
        /// </summary>
        /// <param name="services"></param>
        public static void AddGenerators(this IServiceCollection services)
        {
            services.AddSingleton<IModelGenerator<Game>, GameGenerator>();
            services.AddSingleton<IModelGenerator<Review>, ReviewGenerator>();
        }

        /// <summary>
        /// Add the Unit of Work and its Repositories to the Service Collection.
        /// </summary>
        /// <param name="services"></param>
        public static void AddMockPersistence(this IServiceCollection services)
        {
            // TODO: Garantir que os generators estejam cadastrados
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
