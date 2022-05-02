using JogoOnline.API.Helpers;
using JogoOnline.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Naylah.Data.Access;
using System;

namespace JogoOnline.API.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddDbContext<JogoOnlineDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString(Constant.Key_ConnectionStrings_JogoOnline), JogoOnlineDbContext.ConfigureDBContext));

            services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<JogoOnlineDbContext>());
        }
    }
}