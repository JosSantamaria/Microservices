using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.Entities
{
    public class PaginationEntity<TDocument> //Entidad para paginacion
    {
        public int PageSize { get; set; } //Tamaño de paginas

        public int Page { get; set; } //Pagina

        public string Sort { get; set; } //Orden

        public string SortDirection { get; set; } //Direccion del orden asc / desc

        public string Filter { get; set; } //Filtros

        public FilterValueClass FilterValue { get;set;} //Filtro dinamico propiedad : valor

        public int PageQuantity { get; set; }

        public IEnumerable<TDocument> Data { get; set; } //Persistencia de datos - Guardado - Preservacion de la informacion( formato/atributos ) SE ESTABLECEN LOS VALORES de la entidad.

        public int TotalRows { get; set; }



    }
}
