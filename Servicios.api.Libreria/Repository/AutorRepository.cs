using MongoDB.Driver;
using Servicios.api.Libreria.Core.ContextMongoDB; //Contexto de la conexion a MongoDB
using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Repository
{   //Implementacion de Interfaz IAutorRepository
    public class AutorRepository : IAutorRepository
    {
        
        private readonly IAutorContext _autorContext;
        //Constructor Inyeccion del objeto Autor Context (contiene Cadena de conexion)
        
        
        public AutorRepository(IAutorContext autorContext) 
        {
            //variable de lectura == parametro de AutorRepository
            _autorContext = autorContext;
        }

        //tarea asincrona buscar autores y retornar lista
        //Metodo asincrino, evita que se detengan otras operaciones hechas por el sistema al mismo tiempo de ejecucion
        public async Task<IEnumerable<Autor>> GetAutores()
        {
            return await _autorContext.Autores.Find(p => true).ToListAsync();
        }
    }
}
