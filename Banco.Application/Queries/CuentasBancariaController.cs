using AutoMapper;
using Banco.Application.DTO;
using Banco.Application.Tools;
using Banco.Infraestructure.Modelos;

namespace Banco.Application.Queries
{
    public class CuentasBancariaController
    {
        private Banco_DBContext context;
        private IMapper mapper;

        public CuentasBancariaController(IMapper map, Banco_DBContext context)
        {
            this.context = context;
            mapper = map;
            Validaciones.Bloquear_Al_Que_Debe(context);
        }

        public ErrorMessage CrearCuentaBancaria(CrearCuentaBancariaDTO Cuenta)
        {
            var cliente = context.Clientes.FirstOrDefault(x => x.Id.Equals(Cuenta.ClienteId)) ;

            if (cliente == null)
            {
                return new ErrorMessage()
                {
                    mensaje = "No existe Cliente con ese Id",
                    valido = false,
                };
            }

            var cuenta = mapper.Map<CuentasBancaria>(Cuenta);

            context.CuentasBancarias.Add(cuenta);
            context.SaveChanges();

            return new ErrorMessage()
            {
                mensaje = new
                {
                    mensaje = $"Cuenta bancaria de {cliente.Nombre} creada con exito",
                    saldo = cuenta.SaldoActual,
                    tipo = cuenta.TipoCuenta
                },
                valido = true,
            };

        }

        public ErrorMessage BorrarCuentaBancaria(Guid id)
        {
            var cuenta = context.CuentasBancarias.FirstOrDefault(x => x.Id.Equals(id));

            if (cuenta == null)
            {
                return new ErrorMessage()
                {
                    mensaje = "No existe cuenta con ese Id",
                    valido = false,
                };
            }

            return new ErrorMessage()
            {
                mensaje = $"Cuenta bancaria eliminada con exito",
                valido = true,
            };

        }

      
    }
}
