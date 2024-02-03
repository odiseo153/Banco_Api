using Banco.Application.DTO;
using Banco.Application.Queries;
using Banco.Application.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Banco.Presentation.Controllers
{
    public class CuentaBancariaController : Controller
    {
        private CuentasBancariaController cuenta;

        public CuentaBancariaController(CuentasBancariaController cuenta)
        {
            this.cuenta = cuenta;
        }

        [HttpPost("Cuenta")]
        public ErrorMessage CrearCuenta(CrearCuentaBancariaDTO Cuenta)
        {
            return cuenta.CrearCuentaBancaria(Cuenta);
        }


        [HttpDelete("Cuenta/{id}")]
        public ErrorMessage BorrarCuenta(Guid id)
        {
            return cuenta.BorrarCuentaBancaria(id);
        }
    }
}
