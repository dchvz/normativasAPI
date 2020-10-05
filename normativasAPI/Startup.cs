using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using normativasAPI.APIData;
using normativasAPI.APIData.Configuration;

namespace normativasAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // se carga la seccion del base settings
            services.Configure<NormativasDataBaseSettings>(
                Configuration.GetSection(nameof(NormativasDataBaseSettings)));

            // para el sistema de inyeccion de dependencias, cuando referenciemos la interfaz, se hace la instancia de NormativesDatabaseSettings
            services.AddSingleton<INormativasDataBaseSettings>(sp =>
                sp.GetRequiredService<IOptions<NormativasDataBaseSettings>>().Value);

            // para que la clase de conexion de BD este disponible para el controller
            services.AddSingleton<NormativasDBService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
