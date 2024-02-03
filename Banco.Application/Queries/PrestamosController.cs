using AutoMapper;
using Banco.Application.DTO;
using Banco.Application.Tools;
using Banco.Infraestructure.Modelos;

namespace Banco.Application.Queries
{
    public class PrestamosController
    {
        private Banco_DBContext context;
        private IMapper mapper;

        public PrestamosController(IMapper map, Banco_DBContext context)
        {
            this.context = context;
            mapper = map;
            Validaciones.Bloquear_Al_Que_Debe(context);
        }

        public ErrorMessage CrearPrestamo(CrearPrestamoDTO entity)
        {
            var cliente = context.Clientes.FirstOrDefault(x => x.Id.Equals(entity.ClienteId));

            if (cliente == null)
            {
                return new ErrorMessage()
                {
                    mensaje = "No existe cliente con esa Id",
                    valido = false
                };
            }

            var prestamo = mapper.Map<Prestamo>(entity);

            prestamo.PlazoPrestamo = DateTime.Now.AddSeconds(20);
            prestamo.TasaInteres = (decimal)0.15;

            context.Prestamos.Add(prestamo);
            context.SaveChanges();

            return new ErrorMessage() 
            {
                mensaje = new
                {
                    respuesta = "Prestamo creado con exito",
                    Fecha_Para_pagar = prestamo.PlazoPrestamo.ToString(),
                    Tasa_Interes = prestamo.TasaInteres,
                    Monto = prestamo.MontoPrestamo
                },
                valido = true
            };
        }

        public ErrorMessage Cliente_Prestamos(Guid idCliente)
        {
            var cliente = context.Clientes.FirstOrDefault(x => x.Id.Equals(idCliente));

            if (cliente == null)
            {
                return new ErrorMessage()
                {
                    mensaje = "No existe cliente con ese Id",
                    valido = false
                };
            }

            var prestamos = context.Prestamos.Where(x => x.ClienteId.Equals(cliente.Id)).Select(x => new
            {
                Monto = x.MontoPrestamo,
                Tasa_Interes = x.TasaInteres,
                Fecha_Limite = x.PlazoPrestamo,
                Estado = x.EstadoPrestamo,
                Id = x.Id
            });

            return new ErrorMessage() 
            {
            mensaje =prestamos,
            valido=true
            }; 
        }

        public ErrorMessage Pagar_Prestamos(Guid idPrestamo, int monto_a_pagar,Guid idCuenta)
        {
            var prestamo = context.Prestamos.SingleOrDefault(x => x.Id.Equals(idPrestamo));
            bool recargo=false;

            if (prestamo == null)
            {
                return new ErrorMessage()
                {
                    mensaje = "No existe préstamo con ese Id",
                    valido = false
                };
            }

            var cuentasBancaria = context.CuentasBancarias.SingleOrDefault(x => x.Id.Equals(idCuenta));

            
            if (cuentasBancaria == null)
            {
                return new ErrorMessage()
                {
                    mensaje = "No tiene cuenta bancaria",
                    valido = false
                };
            }


            if (cuentasBancaria.SaldoActual < monto_a_pagar)
            {
                return new ErrorMessage()
                {
                    mensaje = "No tiene suficiente saldo para pagar",
                    valido = false
                };
            }

            cuentasBancaria.SaldoActual -= monto_a_pagar;

            if (prestamo.PlazoPrestamo == DateTime.Now.Date)
            {
                prestamo.MontoPrestamo *= (decimal) prestamo.TasaInteres;
                cuentasBancaria.Habilitada = false;
                recargo = true;
            }

            prestamo.MontoPrestamo -= monto_a_pagar;

            if (prestamo.MontoPrestamo <=0)
            {
                context.Prestamos.Remove(prestamo);
                cuentasBancaria.Habilitada = true;
            }


            context.SaveChanges();

            return new ErrorMessage()
            {
                mensaje = new
                {
                    mensaje = "Préstamo pagado con éxito",
                    aviso =recargo ? $"Al monto del préstamo se le agregó un interés de {prestamo.TasaInteres * 100}% por tardanza en el pago" : "No hay recargo",
                    nuevoMonto = prestamo.MontoPrestamo <= 0 ? "No hay monto" : prestamo.MontoPrestamo.ToString()
                },
                valido = true
            };
        }



    }
}
