using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.Entities
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)] //Establecemos la herencia de la clase en false
    public class BsonCollectionAttribute : Attribute //Herencia de Atributo
    {
        public string CollectionName { get; } //Propiedad

        //Creamos el constructor de la clase
        public BsonCollectionAttribute(string collectionName) 
        {
            CollectionName = collectionName; //Propiedad == a parametro.
        }
    }
}
