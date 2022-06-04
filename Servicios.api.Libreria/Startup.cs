using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Servicios.api.Libreria.Core;
using Servicios.api.Libreria.Core.ContextMongoDB;
using Servicios.api.Libreria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria
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
            services.Configure<MongoSettings>(
                options => {
                    //Valores de configuracion

                    //obtenemos el valor del ConnectionString (Clase MongoSettings)
                    options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value; //desde appsettings.json
                    //Obtener el valor / nombre de database
                    options.Database = Configuration.GetSection("MongoDb:Database").Value;

                }
                );
            //Evalua si existe MongoSettings sino crea el objeto.
            services.AddSingleton<MongoSettings>();

            //Crea una nueva instancia cuando el cliente intente ejecutar la api (por transaccion)
            services.AddTransient<IAutorContext, AutorContext>();
            
            //Nueva instancia de Autor Repository
            services.AddTransient<IAutorRepository, AutorRepository>();

            //Instaciamos el tipo de docuemnto para documentos genericos
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddControllersWithViews();

            //Añadimos nuestro CORS - politica de acceso
            services.AddCors(opt => {

                opt.AddPolicy("CorsRule", rule =>
                {
                    rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*"); //Acceso permitido desde cualquier cliente / puerto
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("CorsRule"); //Añadimos el uso del CORS que creamos

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
