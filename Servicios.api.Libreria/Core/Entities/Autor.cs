
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.Entities
{
    public class Autor
    {   [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }
        

        [BsonElement("nombre")] //Propiedad de Document(BSON) en Base de datos MongoDb
        public string Nombre { get; set; }
        

        [BsonElement("apellido")] //Propiedad de Document(BSON) en Base de datos MongoDb
        public string Apellido { get; set; }

        [BsonElement("gradoAcademico")] //Propiedad de Document(BSON) en Base de datos MongoDb
        public string GradoAcademico { get; set; }


    }
}
