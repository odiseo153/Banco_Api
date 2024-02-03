using AutoMapper;
using Banco.Application.DTO;
using Banco.Application.Tools;
using Banco.Infraestructure.Modelos;

namespace Banco.Application.Queries
{
    public class TransaccioneController
    {
        private Banco_DBContext context;
        private IMapper mapper;

        public TransaccioneController(IMapper map, Banco_DBContext context)
        {
            this.context = context;
            mapper = map;
            Validaciones.Bloquear_Al_Que_Debe(context);
        }

        public ErrorMessage Hacer_Transaccion(CrearTransaccionDTO entity)
        {
            var transaccion = mapper.Map<Transaccione>(entity);

            var cuentaOrigen = context.CuentasBancarias.SingleOrDefault(x => x.Id == entity.CuentaId);
            var cuentaDestino = context.CuentasBancarias.SingleOrDefault(x => x.Id == entity.CuentaDestinoId);

            if (cuentaOrigen == null)
            {
                return new ErrorMessage { mensaje = "No existe cuenta de origen con ese Id", valido = false };
            }

            if (!cuentaOrigen.Habilitada)
            {
                var prestamosVencidos = context.Prestamos
                    .Where(x => x.ClienteId == cuentaOrigen.ClienteId && x.PlazoPrestamo < DateTime.Now)
                    .Select(x => new
                    {
                        MontoPendiente = x.MontoPrestamo,
                        Tasa_Interes_Aplicada = x.TasaInteres,
                        Monto_Con_Interes =x.MontoPrestamo + (x.MontoPrestamo * x.TasaInteres),
                    })
                    .ToList();

                return new ErrorMessage
                {
                    mensaje = new { error = "La cuenta de origen está bloqueada porque tiene un préstamo pendiente", prestamos = prestamosVencidos },
                    valido = false
                };
            }

            if (cuentaDestino == null)
            {
                return new ErrorMessage { mensaje = "No existe cuenta Destinada con ese Id", valido = false };
            }

            if (cuentaOrigen.SaldoActual < entity.Monto)
            {
                return new ErrorMessage { mensaje = "No tiene saldo suficiente en esa cuenta", valido = false };
            }

            cuentaDestino.SaldoActual += entity.Monto;
            cuentaOrigen.SaldoActual -= entity.Monto;

            context.Transacciones.Add(transaccion);
            context.SaveChanges();

            return new ErrorMessage { mensaje = "Transacción hecha con éxito", valido = true };
        }


        public IQueryable Transaccion()
        {
            var transacciones = context.Transacciones.Select(x => new
            {
                tipo = x.TipoTransaccion,
                Fecha = x.FechaHoraTransaccion,
                Monto_Que_Se_transfirio = x.Monto,
                Cuenta = x.Cuenta
            });

            return transacciones; 
        }

    }
}
