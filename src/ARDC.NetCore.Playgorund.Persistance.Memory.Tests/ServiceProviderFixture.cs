using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Persistance.Mock.Generators;
using Microsoft.Extensions.DependencyInjection;

namespace ARDC.NetCore.Playground.Persistance.Memory.Tests
{
    public class ServiceProviderFixture
    {
        private readonly ServiceProvider _provider;

        public ServiceProviderFixture()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddGenerators();
            services.AddMemoryPersistence();
            _provider = services.BuildServiceProvider();
            SeedData(_provider);
        }

        /// <summary>
        /// Dependency Provider.
        /// </summary>
        public ServiceProvider Provider => _provider;

        private void SeedData(ServiceProvider provider)
        {
            var gameGenerator = provider.GetService<IModelGenerator<Game>>();
            var bunchOfGames = gameGenerator.Get(2, 50);

            var reviewGenerator = provider.GetService<IModelGenerator<Review>>();
            var bunchOfReviews = reviewGenerator.Get(2, 50);

            var context = provider.GetService<PlaygroundContext>();
            context.Games.AddRange(bunchOfGames);
            context.Reviews.AddRange(bunchOfReviews);

            context.SaveChanges();

        }
    }
}
