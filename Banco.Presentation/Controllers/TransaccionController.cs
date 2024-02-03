using Banco.Application.DTO;
using Banco.Application.Queries;
using Banco.Application.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Banco.Presentation.Controllers
{
    public class TransaccionController : Controller
    {
        private TransaccioneController transaccion;

        public TransaccionController(TransaccioneController transaccion)
        {
            this.transaccion = transaccion;
        }

        [HttpPost("Transaccion")]
        public ErrorMessage Hacer_Una_Transaccion(CrearTransaccionDTO entity)
        {
            return transaccion.Hacer_Transaccion(entity);
        }

        [HttpGet("Transaccion")]
        public IQueryable Transacciones()
        {
            return transaccion.Transaccion();
        }
    }
}
