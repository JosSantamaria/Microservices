using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.Entities
{
    public class Document : IDocument //Entidad Documento con la Interfaz de Documento Implementada
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set ; } //STR

        public DateTime CreateDate => DateTime.Now;
    }
}
