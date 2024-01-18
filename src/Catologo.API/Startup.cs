using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Catalogo.API;

namespace Catalogo.API.Startup
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Configuração de serviços
        public void ConfigureServices(IServiceCollection services)
        {
            // Obtenha o caminho do arquivo JSON da configuração
            var jsonFilePath = Configuration["JsonFilePath"];

            // Adicione o serviço CatalogoService com a string para o caminho do arquivo JSON
            services.AddScoped(provider => new CatalogoService(jsonFilePath));

            services.AddControllers();
        }

        // Configuração da pipeline de solicitação
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Catalogo}/{action=GetAllBooks}/{id?}");
            });
        }
    }
}