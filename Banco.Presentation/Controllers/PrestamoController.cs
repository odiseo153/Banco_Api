using Banco.Application.DTO;
using Banco.Application.Queries;
using Banco.Application.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Banco.Presentation.Controllers
{
    public class PrestamoController : Controller
    {
        private PrestamosController prestamo;

        public PrestamoController(PrestamosController prestamo)
        {
            this.prestamo = prestamo;
        }


        [HttpPost("Prestamo")]
        public ErrorMessage CrearPrestamo(CrearPrestamoDTO entity)
        {
            return prestamo.CrearPrestamo(entity);
        }

        [HttpPost("Pagar_Prestamo")]
        public ErrorMessage PagarPrestamo(Guid idPrestamo,int monto,Guid idCuenta)
        {
            return prestamo.Pagar_Prestamos(idPrestamo,monto,idCuenta);
        }

        [HttpGet("Cliente_Prestamo")]
        public ErrorMessage ClientePrestamos(Guid id)
        {
            return prestamo.Cliente_Prestamos(id);
        }
    }
}
