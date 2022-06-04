using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Repository
{//Procesar el documento generico, mientras sea de tipo IDocument ( Interfaz de documento generico que creamos)
    //Establecemos la Interfaz como publica para su uso.
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        //Podemos obtener todo de cualquier tipo de documento (Autor,Nombre,Grado,etc)

        //Esta definicion es en caso del uso del metodo no asincrono
        //IQueryable<TDocument> GetAll();

        //Uso en metodo asyncrono definimos como Tarea
        Task<IEnumerable<TDocument>> GetAll(); //Obtener todos

        Task<TDocument> GetById(string Id); //Obtener por Id

        Task InsertDocument(TDocument document); //Insertar

        Task UpdateDocument(TDocument document); //Actualizar

        Task DeleteById(string Id); //Eliminar por ID

        //Filtro rigido
        Task<PaginationEntity<TDocument>> PaginationBy(
            Expression<Func<TDocument, bool>> filterExpression, //Expresion generica - expresiofiltro
            PaginationEntity<TDocument> pagination //paginacion es de tipo PaginationEntity valida que el TypeDocument es herencia de IDocument
            );

        Task<PaginationEntity<TDocument>> PaginationByFilter( //paginacion por medio de filtro dinamico / flexible
            PaginationEntity<TDocument> pagination 
            );
    }
}