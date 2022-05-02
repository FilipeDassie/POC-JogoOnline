using Hangfire;
using JogoOnline.API.Configurations;
using JogoOnline.API.Helpers;
using JogoOnline.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JogoOnline.API
{
    public class JogoOnlineService
    {
        public IConfiguration Configuration { get; }

        public JogoOnlineService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração do Hangfire.
            services.AddHangfireConfiguration(Configuration);

            // Configuração do Contexto (banco de dados).
            services.AddDatabaseConfiguration(Configuration);

            // Configuração dos Serviços.
            services.AddServiceConfiguration();

            // Configuração dos Mapeamentos.
            services.AddAutoMapperConfiguration();

            // Configuração do Cache.
            services.AddCacheConfiguration(Configuration);

            // Configuração do Json.
            services.AddJsonOptionsConfiguration();

            // Configuração da Compressão dos dados de retorno.
            services.AddCompressionConfiguration();

            // Configuração do Swagger.
            services.AddSwaggerConfiguration();

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc();
            services.AddControllers(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Ativa o Swagger.
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jogo Online API v1.0");
            });

            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangFireAuthFilter() }
            });

            // Ativa a Compressão.
            app.UseResponseCompression();

            app.UseMvc();

            RegisterBackgroundServices();
        }

        private void RegisterBackgroundServices()
        {
            // Agenda o processamento dos resultados dos jogos.
            RecurringJob.AddOrUpdate<GameResultService>(nameof(GameResultService.GetGameResultRedisCache), x => x.GetGameResultRedisCache(null, true),
                Configuration[Constant.Key_AppSettings_Cron_Expression_Process_Game_Result]);
        }
    }
}