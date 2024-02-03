using Banco.Application.DTO;
using Banco.Application.Queries;
using Banco.Application.Tools;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Banco.Presentation.Controllers
{
    public class ClienteController : Controller
    {
        private ClientesController cliente;

        public ClienteController(ClientesController clientes) 
        { 
            this.cliente = clientes;    
        }

        [HttpPost("Cliente")]
        public ErrorMessage Crear(CrearClienteDTO Nuevocliente)
        {
            return cliente.CrearCliente(Nuevocliente);
        }
        [HttpPut("Cliente")]
        public Task<ErrorMessage> Actualizar(CrearClienteDTO cliente1)
        {
            return cliente.ActualizarCliente(cliente1);
        }


        [HttpGet("Cliente/{cedula}")]
        public IActionResult ClienteCedula(string cedula)
        {
            return Ok( cliente.Datos_Cliente(cedula));
        }

        [HttpDelete("Cliente")]
        public ErrorMessage Borrar(string cedula)
        {
            return cliente.EliminarCliente(cedula);
        }


    }
}
