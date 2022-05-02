using AutoMapper;
using JogoOnline.API.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JogoOnline.API.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddAutoMapper(typeof(PlayerProfile));
            services.AddAutoMapper(typeof(GameProfile));
            services.AddAutoMapper(typeof(GameResultProfile));
        }
    }
}