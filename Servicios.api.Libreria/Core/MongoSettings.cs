using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core
{
    public class MongoSettings
    {
       public string ConnectionString { get; set; } //Propiedades usadas en el appsettings.json

        public string Database { get; set; } //Propiedades usadas en el appsettings.json

    }
}
