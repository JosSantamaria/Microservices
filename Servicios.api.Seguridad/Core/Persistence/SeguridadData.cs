using Microsoft.AspNetCore.Identity;
using Servicios.api.Seguridad.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Persistence
{
    public class SeguridadData
    {
        public static  async Task InsertarUsuario(SeguridadContexto context, UserManager<Usuario> usuarioManager) 
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario { 
                    Nombre = "Joset",
                    Apellido = "Santamaria",
                    Direccion = "C-29B, 141",
                    UserName = "jsantamaria", //Data Obligatoria
                    Email = "josetsantamaria@gmail.com"
                };

                await usuarioManager.CreateAsync(usuario, "Pass123$");
            }
        }
    }
}
