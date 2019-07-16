﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;

namespace ARDC.NetCore.Playground.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddMvc();

            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = "GitHub";
                })
                .AddCookie("Bearer")
                .AddGitHub("GitHub", opt =>
                {
                    opt.ClientId = @"my client id";                             // TODO: Adicionar aos Secrets
                    opt.ClientSecret = @"my client secret";     // TODO: Adicionar aos Secrets
                    opt.CallbackPath = new PathString("/signin-github");

                });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = "https://github.com/login/oauth/authorize",
                    TokenUrl = "https://github.com/login/oauth/access_token"
                });
                
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

            app.UseHsts();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwaggerUI(opt =>
            {
                opt.DisplayRequestDuration();
                opt.OAuthAppName("dotNet Core Playground");
                opt.OAuthClientId("my client id");          // TODO: Obter dos Secrets
                opt.OAuth2RedirectUrl("https://localhost:5001/signin-github");
                opt.RoutePrefix = string.Empty;
                opt.SwaggerEndpoint("swagger/v1/swagger.json", "Playground API V1");
            });           
        }
    }
}
