using MongoDB.Driver;
using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.ContextMongoDB
{
    public interface IAutorContext
    {
        //Creamos la interfaz del autor
        IMongoCollection<Autor> Autores {get;} //get: en este caso obtenemos los autores usando esta interfaz.
    }
}
