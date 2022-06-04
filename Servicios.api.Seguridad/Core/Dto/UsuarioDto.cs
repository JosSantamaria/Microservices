using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Dto
{
    public class UsuarioDto   //DTO: Data Transformation Object
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }



    }
}
