using JogoOnline.API.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JogoOnline.API.Configurations
{
    public static class CacheConfiguration
    {
        public static void AddCacheConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // Configuração do redis.
            services.AddDistributedRedisCache(opts =>
            {
                opts.Configuration = configuration[Constant.Key_AppSettings_RedisCache_Connection];
            });

            // Habilita o cache em memória.
            services.AddMemoryCache();
        }
    }
}