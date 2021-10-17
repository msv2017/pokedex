using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokedex.Application;
using Pokedex.Proxies;

namespace Pokedex
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
            services.AddControllers();

            services.AddTransient<IPokeApiProxy, PokeApiProxy>();
            services.AddTransient<IFunTranslationsApiProxy, FunTranslationsApiProxy>();

            services.AddTransient<IPokemonService, PokemonService>();

            services.AddSingleton<ICacheService, RedisCacheService>();

            services.AddSingleton<IPokemonTranslator, DefaultTranslator>();
            services.AddSingleton<IPokemonTranslator, YodaTranslator>();
            services.AddSingleton<IPokemonTranslator, ShakespearTranslator>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("redis");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
