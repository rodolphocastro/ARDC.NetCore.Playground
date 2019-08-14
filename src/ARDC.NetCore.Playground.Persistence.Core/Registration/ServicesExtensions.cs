﻿using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Persistence.Core;
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}