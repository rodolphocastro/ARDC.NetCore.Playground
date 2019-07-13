using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ARDC.NetCore.Playground.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "NetCore Playground API",
                    Description = "API para testes com .NET Core 2.2",
                    Contact = new Contact { Name = "Rodolpho Alves", Url = @"https://github.com/rodolphocastro", Email = "not.my.email@gmail.com" }   // TODO: Algum dia colocar um e-mail válido :shrug:
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwaggerUI(opt =>
            {
                opt.RoutePrefix = string.Empty;
                opt.SwaggerEndpoint("swagger/v1/swagger.json", "Playground API V1");
            });

            app.UseMvc();
        }
    }
}
