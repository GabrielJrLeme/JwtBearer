using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TokenJwtBearer.Models;
using TokenJwtBearer.Models.Security;
using TokenJwtBearer.Services;

namespace TokenJwtBearer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();
            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(Security.Securitykey);

            services.AddAuthentication(x =>
            {

                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });


            var cache = new CacheService();
            Database(cache.Cache);

            services.AddSingleton(cache);
            services.AddScoped<AuthService>();

        }




        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private void Database(ConcurrentDictionary<string, Registros> cache)
        {

            List<Usuario> usuarios = new List<Usuario>()
            {
                new Usuario()
                {
                    Name = "gabriel",
                    Password = "1234",
                    Email = "gabriel@email.com",
                    Role = "admin",
                    Token = null
                },
                new Usuario()
                {
                    Name = "carlos",
                    Password = "1234",
                    Email = "carlos@email.com",
                    Role = "user",
                    Token = null
                }
            };

            Registros listaRegistros = new Registros(usuarios) { };

            cache.TryAdd(listaRegistros.Chave, listaRegistros);
        }
    }
}
