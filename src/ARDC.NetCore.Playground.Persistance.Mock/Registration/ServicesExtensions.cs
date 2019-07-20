using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistance.Mock.Generators;
using ARDC.NetCore.Playground.Persistance.Mock.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ARDC.NetCore.Playground.Persistance.Mock.Registration
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
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddSingleton<IGameRepository, GameRepository>();
            services.AddSingleton<IReviewRepository, ReviewRepository>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
        }
    }
}
