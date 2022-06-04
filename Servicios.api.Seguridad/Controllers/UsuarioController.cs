﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Servicios.api.Seguridad.Core.Application;
using Servicios.api.Seguridad.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioDto>> Registrar(Register.UsuarioRegisterCommand parametros) 
        {
            return await _mediator.Send(parametros);
        }
    }
}
