using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.api.Libreria.Core.Entities;
using Servicios.api.Libreria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriaServicioController : ControllerBase
    {
        
        private readonly IAutorRepository _autorRepository; //declaracion de variable de lectura

        private readonly IMongoRepository<AutorEntity> _autorGenericoRepository; // Implementacion la VL en base a las clases Genericas del Repository

        private readonly IMongoRepository<EmpleadoEntity> _empleadoGenericoRepository; //Instanciamos la entidad de empleado (nueva)

        public LibreriaServicioController(IAutorRepository autorRepository, IMongoRepository<AutorEntity> autorGenericoRepository , IMongoRepository<EmpleadoEntity> empleadoGenericoRepository) //declaracion del metodo para consumo del cliente
        {
            _autorRepository = autorRepository; //VarLect = parametro
            _autorGenericoRepository = autorGenericoRepository;
            _empleadoGenericoRepository = empleadoGenericoRepository;
        }

        //Nueva peticion de entidad generica
        [HttpGet("empleadoGenerico")]                                 //Funcion padre - definida en el controller
        public async Task<ActionResult<IEnumerable<EmpleadoEntity> > > GetempleadoGenerico()
        { //aplicar la funcion GetAll() creada en la interface IMongoRepository : herencia de IDocument a el parametro correspondiente a la nueva entidad empleado.
            var empleado = await _empleadoGenericoRepository.GetAll();
            return Ok(empleado);
        }

        //Construimos una tarea HTTP nueva con el enfoque al documento generico
        [HttpGet("autorGenerico")]
        public async Task<ActionResult<IEnumerable<AutorEntity> > > GetAutorGenerico() 
        {
            var autores = await _autorGenericoRepository.GetAll();
            return Ok(autores);
        }

        
        [HttpGet("autores")]
        public async Task<ActionResult<IEnumerable<Autor> > > GetAutores()
        {
            var autores = await _autorRepository.GetAutores();
            return Ok(autores);
        }


    }
}
