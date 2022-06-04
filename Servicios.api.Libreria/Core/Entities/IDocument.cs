using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.Entities
{//Interfaz Generica para el patron Repository (Sustituye las Interfaces individuales por una sola y se implementa en la clase Document)
    //Establecemos la interfaz como public para no tener problemas de Incoherencia / restricciones de acceso en otras implementaciones.
    public interface IDocument 
    {
        [BsonId]

        [BsonRepresentation(BsonType.ObjectId)]
        
        string Id { get; set; }
        
        DateTime CreateDate { get; }
 

    }
}
