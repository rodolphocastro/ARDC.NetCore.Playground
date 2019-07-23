using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistance.Memory;
using ARDC.NetCore.Playground.Persistance.Memory.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtensions
    {
        public static void AddMemoryPersistence(this IServiceCollection services)
        {
            services.AddEntityFrameworkInMemoryDatabase();
            services.AddDbContext<PlaygroundContext>(opt =>
            {
                opt.UseInMemoryDatabase("PlaygroundDb");
            });
            services.AddScoped<IGameRepository, GameRepository>();
            // TODO: Registrar repository de Reviews
            // TODO: Registrar UoW
        }
    }
}
