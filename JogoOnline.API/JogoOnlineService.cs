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
            // Configura��o do Hangfire.
            services.AddHangfireConfiguration(Configuration);

            // Configura��o do Contexto (banco de dados).
            services.AddDatabaseConfiguration(Configuration);

            // Configura��o dos Servi�os.
            services.AddServiceConfiguration();

            // Configura��o dos Mapeamentos.
            services.AddAutoMapperConfiguration();

            // Configura��o do Cache.
            services.AddCacheConfiguration(Configuration);

            // Configura��o do Json.
            services.AddJsonOptionsConfiguration();

            // Configura��o da Compress�o dos dados de retorno.
            services.AddCompressionConfiguration();

            // Configura��o do Swagger.
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

            // Ativa a Compress�o.
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