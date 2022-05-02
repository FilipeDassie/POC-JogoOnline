using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json.Serialization;

namespace JogoOnline.API.Configurations
{
    public static class JsonOptionsConfiguration
    {
        public static void AddJsonOptionsConfiguration(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.IgnoreNullValues = true;
            });
        }
    }
}