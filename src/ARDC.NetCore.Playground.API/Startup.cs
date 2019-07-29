using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using ARDC.NetCore.Playground.API.Filters;
using ARDC.NetCore.Playground.API.Settings;
using ARDC.NetCore.Playground.API.ViewModels.Registration;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;

namespace ARDC.NetCore.Playground.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));            
        }

        public GitHubSettings GitHubSettings { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            // TODO: Permitir que a implementação seja escolhida por Settings
            services.AddGenerators();
            services.AddMemoryPersistence();
            services.AddAutoMapper(ProfileRegistration.GetProfiles());

            services.AddMvc();

            GitHubSettings = _configuration.GetSection(nameof(GitHubSettings)).Get<GitHubSettings>();   // TODO: Criar uma extension para facilitar a vida

            if (GitHubSettings == null)
                throw new ArgumentNullException(nameof(GitHubSettings), "GitHub OAuth settings were not found. Did you forget to edit your appSettings.json?");
            
            services
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = "GitHub";
            })
            .AddCookie()
            .AddGitHub("GitHub", opt =>
            {
                opt.ClientId = GitHubSettings.ClientId;                             
                opt.ClientSecret = GitHubSettings.ClientSecret;     
                opt.CallbackPath = new PathString(GitHubSettings.CallbackPath);
                opt.SaveTokens = true;

                opt.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                opt.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                opt.ClaimActions.MapJsonKey("urn:github:login", "login");
                opt.ClaimActions.MapJsonKey("urn:github:url", "html_url");
                opt.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

                opt.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                        var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                        response.EnsureSuccessStatusCode();

                        var user = JObject.Parse(await response.Content.ReadAsStringAsync());

                        context.RunClaimActions(user);
                    }
                };
            });

            // Armazenando as configurações do GitHub para uso Futuro
            services.Configure<GitHubSettings>(_configuration.GetSection(nameof(GitHubSettings)));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "NetCore Playground API",
                    Description = "API para testes com .NET Core 2.2",
                    Contact = new Contact { Name = "Rodolpho Alves", Url = @"https://github.com/rodolphocastro", Email = "not.my.email@gmail.com" }   // TODO: Algum dia colocar um e-mail válido :shrug:
                });

                c.AddSecurityDefinition("GitHub", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "accessCode",
                    AuthorizationUrl = "https://github.com/login/oauth/authorize",
                    TokenUrl = "https://github.com/login/oauth/access_token"
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            // TODO: Entender por qual motivo o Authorize do Swagger não funciona.
            app.UseSwaggerUI(opt =>
            {
                opt.DisplayRequestDuration();
                opt.RoutePrefix = string.Empty;
                opt.SwaggerEndpoint("swagger/v1/swagger.json", "Playground API V1");
                opt.OAuth2RedirectUrl("https://localhost:5001/auth/callback");
                opt.OAuthClientId(GitHubSettings.ClientId);
                opt.OAuthClientSecret(GitHubSettings.ClientSecret);
                opt.OAuthAppName("dotNet Core Playground");
            });
            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
