using Hangfire;
using Hangfire.Console;
using JogoOnline.API.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JogoOnline.API.Configurations
{
    public static class HangfireConfiguration
    {
        public static void AddHangfireConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddHangfire(x =>
            {
                x.UseSqlServerStorage(configuration.GetConnectionString(Constant.Key_ConnectionStrings_JogoOnline));
                x.UseConsole();
            });
        }
    }
}