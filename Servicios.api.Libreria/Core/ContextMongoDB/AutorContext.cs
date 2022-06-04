using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.ContextMongoDB
{     //En el contexto del autor, implementamos la interfaz del mismo elemento haciendo referencia a que devolvera ese dato (IAutorContext)
    public class AutorContext : IAutorContext
    {
        private readonly IMongoDatabase _db;
        
        public AutorContext(IOptions<MongoSettings> options) 
        {
            //creamos un nuevo cliente de mongodb //Obtenemos el valor desde options : ConnectionString
            var client = new MongoClient(options.Value.ConnectionString);
            //Inicializacion de acceso a base de datos
            _db = client.GetDatabase(options.Value.Database);
        }


        public IMongoCollection<Autor> Autores => _db.GetCollection<Autor>("Autor");
    }
}
