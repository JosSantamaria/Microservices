using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Servicios.api.Seguridad.Core.Application;
using Servicios.api.Seguridad.Core.Entities;
using Servicios.api.Seguridad.Core.JwtLogic;
using Servicios.api.Seguridad.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad
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
            //Validacion con Fluent Validation
            services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Register>());

            services.AddDbContext<SeguridadContexto>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("ConexionDb"));
            });

            //Constructor
            var builder = services.AddIdentityCore<Usuario>(); //Metodo para creacion de Objetos
            //Consrictor
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            //Constructor de la cadena de conexion de la DB
            identityBuilder.AddEntityFrameworkStores<SeguridadContexto>();
            //Constructor Agregado de la clase para el inicio de sesion y creacion del primer usuario
            identityBuilder.AddSignInManager<SignInManager<Usuario>>(); //Inicio de Sesion

            services.TryAddSingleton<ISystemClock, SystemClock>();
            //devuelde el tipo de objeto ensamblado
            services.AddMediatR(typeof(Register.UsuarioRegisterCommand).Assembly);

            services.AddAutoMapper(typeof(Register.UsuarioRegisterHandler));
            //Constructor de implementacion del JWT
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            //Obtenemos la sesion actual.
            services.AddScoped<IUsuarioSesion, UsuarioSesion>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jsantamaria2022$asdfghjklñ"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false  //Verifica el dominio
                };
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseAuthentication(); //Se implementa para poder obtener la sesion por medio del token y no de error de userName.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
