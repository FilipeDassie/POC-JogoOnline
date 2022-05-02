using JogoOnline.API.Services;
using JogoOnline.API.Services.Cache;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JogoOnline.API.Configurations
{
    public static class ServiceConfiguration
    {
        public static void AddServiceConfiguration(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<PlayerService>();
            services.AddScoped<GameService>();
            services.AddScoped<GameResultService>();

            #region Helpers

            services.AddSingleton<CacheService>();
            services.AddSingleton<MemoryCacheService>();

            #endregion
        }
    }
}