using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.Entities
{//Creamos la clase autor ahora con la herencia de los helpers genericos (Interfaz => IDocument y Clase => Document).
    
    //Hacemos el match por medio de la notacion especificada con la clase.
    
    [BsonCollection("Autor")]  //Hacemos que coincida la clase con el nombre de la coleccion.
    public class AutorEntity : Document //Implementa las propiedades de ID y Creation Time
    {
        /*
         * Aqui implementariamos el ID, pero como sacamos el ID a una clase generica  (Document), este hereda las propiedades de la misma.
         * 
         * Aqui ya heredamos las propiedades de  ID y CreationDate.
         */

        [BsonElement("nombre")] //Elemento Bson(dentro de la DB)
        public string Nombre { get; set; }

        [BsonElement("apellido")] //notacion de elemento
        public string Apellido { get; set; }

        [BsonElement("gradoAcademico")]
        public string GradoAcademico { get; set; }

        [BsonElement("nombreCompleto")]
        public string NombreCompleto { get; set; }

    }
}
