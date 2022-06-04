using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Servicios.api.Libreria.Core;
using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Repository
{//Clase generica
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        //Instanciamos la clase de Collection
        private readonly IMongoCollection<TDocument> _collection;

        //Inyectamos la dependencia de la cadena de conexion y usamos la variable de options
        public MongoRepository(IOptions<MongoSettings>options) 
        {
            var client = new MongoClient(options.Value.ConnectionString); //Nuevo cliente y conexion
            var db = client.GetDatabase(options.Value.Database); // Implementamos la cadena de conexion y usamos una funcion para obtener la base de datos que pasamos por parametro

            //Obtenemos la instancia de la conexion GetCollection(nombre de entidad,nombre de coleccion)
            _collection = db.GetCollection<TDocument>( GetCollectionName( typeof(TDocument) ) );
        }

        private protected string GetCollectionName(Type documentType) 
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()).CollectionName;
        }

        //Metodos de Interfaz //metodo no asincrono.
        //public IQueryable<TDocument> GetAll()
        //{
        //    return _collection.AsQueryable();
        //}

        //CONVERTIMOS el metodo en uno asincrono
        public async Task<IEnumerable<TDocument>> GetAll() //Metodo definido en la interfaz, debemos modificar tambien.
        {
            return await _collection.Find(p =>true ).ToListAsync();
        }

         public async Task<TDocument> GetById(string Id)
        {   
            //Filtro de condicion logica
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, Id);//Busqueda de documento / compara con el parametro.
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task InsertDocument(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task UpdateDocument(TDocument document)
        {
            //Realizamos un filtro que busque el documento con el tipo de TDocument y luego lo compare
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            
            //Despues lo buscara en la coleccion y lo reemplazara
            await _collection.FindOneAndReplaceAsync(filter, document);  
        }

        public async Task DeleteById(string Id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, Id);
            await _collection.FindOneAndDeleteAsync(filter);
        }

        //Metodo de paginacion
        public async Task<PaginationEntity<TDocument>> PaginationBy(Expression<Func<TDocument, bool>> filterExpression, PaginationEntity<TDocument> pagination)
        {               
            /*Objeto de paginacion*/
            //>Builders> Generado a travez de la colecion TDocument - Orden ascendente por defecto
            var sort = Builders<TDocument>.Sort.Ascending(pagination.Sort);
            
            if (pagination.SortDirection == "desc") 
            {
               sort = Builders<TDocument>.Sort.Descending(pagination.Sort);
            }

            //Filtro de paginacion nulo o vacio?
            if (string.IsNullOrEmpty(pagination.Filter))
            {
                pagination.Data = await _collection.Find(p => true)
                            .Sort(sort)
                            .Skip((pagination.Page - 1) * pagination.PageSize)
                            .Limit(pagination.PageSize)
                            .ToListAsync();
            }
            else 
            {
                pagination.Data = await _collection.Find(filterExpression)
                            .Sort(sort)
                            .Skip((pagination.Page - 1) * pagination.PageSize)
                            .Limit(pagination.PageSize)
                            .ToListAsync();
            }

            long totalDocuments = await _collection.CountDocumentsAsync(FilterDefinition<TDocument>.Empty); // Conteo de documentos
            var totalPages = Convert.ToInt32( Math.Ceiling( Convert.ToDecimal( totalDocuments / pagination.PageSize) ) ); //Division de total / n.paginas /Conversion a Decimal / Conversion a Entero

            pagination.PageQuantity = totalPages;

            return pagination;


        }

        public async Task<PaginationEntity<TDocument>> PaginationByFilter(PaginationEntity<TDocument> pagination)
        {
            /*Objeto de paginacion*/
            //>Builders> Generado a travez de la colecion TDocument - Orden ascendente por defecto
            var sort = Builders<TDocument>.Sort.Ascending(pagination.Sort);

            if (pagination.SortDirection == "desc")
            {
                sort = Builders<TDocument>.Sort.Descending(pagination.Sort);
            }


            var totalDocuments = 0;

            //Filtro de paginacion nulo o vacio?
            if (pagination.FilterValue == null) //En este caso se usa una clase y se pregunta diferente:
            {
                pagination.Data = await _collection.Find(p => true)
                            .Sort(sort)
                            .Skip((pagination.Page - 1) * pagination.PageSize)
                            .Limit(pagination.PageSize)
                            .ToListAsync();
               totalDocuments =  (await _collection.Find(p => true).ToListAsync()).Count();
            }
            else
            {
                var valueFilter = ".*" + pagination.FilterValue.Valor + ".*"; //Representacion de Expresiones Regulares.
                var filter = Builders<TDocument>.Filter.Regex(pagination.FilterValue.Propiedad, new BsonRegularExpression( valueFilter,"i"));

                pagination.Data = await _collection.Find(filter)
                            .Sort(sort)
                            .Skip((pagination.Page - 1) * pagination.PageSize)
                            .Limit(pagination.PageSize)
                            .ToListAsync();
                totalDocuments = (await _collection.Find(filter).ToListAsync()).Count();

            }

            //Devuelve el n.de total docuemntos dentro de la colecion en la DB
            //long totalDocuments = await _collection.CountDocumentsAsync(FilterDefinition<TDocument>.Empty); // Conteo de documentos

            //devuelve el numero de documentos segun el filtro
            var rounded = Math.Ceiling(totalDocuments / Convert.ToDecimal(pagination.PageSize) );

            var totalPages = Convert.ToInt32(rounded); 

            pagination.PageQuantity = totalPages; //cantidad de paginas

            pagination.TotalRows = Convert.ToInt32(totalDocuments); //cantidad de registros por documentos

            return pagination;
            
        }
    }
}
