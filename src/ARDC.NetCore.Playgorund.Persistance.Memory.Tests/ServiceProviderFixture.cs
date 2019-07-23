using Microsoft.Extensions.DependencyInjection;

namespace ARDC.NetCore.Playground.Persistance.Memory.Tests
{
    public class ServiceProviderFixture
    {
        private readonly ServiceProvider _provider;

        public ServiceProviderFixture()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddMemoryPersistence();
            _provider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Dependency Provider.
        /// </summary>
        public ServiceProvider Provider => _provider;
    }
}
