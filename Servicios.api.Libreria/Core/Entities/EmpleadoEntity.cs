using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.Entities
{   
    //Match con el BSON collection
    [BsonCollection("Empleado")]
    public class EmpleadoEntity : Document //Instancia de Document (otra Entity)
    {
        [BsonElement("nombre")]    //Hacemos el match con el Bson Element
        public string Nombre { get; set; }
   
    }
}
