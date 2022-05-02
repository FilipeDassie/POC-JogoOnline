using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace JogoOnline.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Jogo Online API",
                        Version = "v1.0",
                        Description = "Jogo Online API.",
                        Contact = new OpenApiContact
                        {
                            Name = "JogoOnline",
                            Url = new Uri("http://JogoOnline.com")
                        }
                    });
            });
        }
    }
}