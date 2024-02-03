using AutoMapper;
using Banco.Application.DTO;
using Banco.Application.Tools;
using Banco.Infraestructure.Modelos;
using Microsoft.AspNetCore.Http;
using System.Data.Entity;

namespace Banco.Application.Queries
{
    public class ClientesController
    {
        private Banco_DBContext context;
        private IMapper mapper;

        public ClientesController(IMapper map, Banco_DBContext context)
        {
            // Constructor que inicializa el controlador con el contexto de la base de datos y el mapeador
            this.context = context;
            mapper = map;
            Validaciones.Bloquear_Al_Que_Debe(context); // Llamada a método de validación al construir el controlador
        }

        // Método para crear un nuevo cliente
        public ErrorMessage CrearCliente(CrearClienteDTO cliente)
        {
            // Verificar si ya existe un cliente con el mismo número de identificación
            var clienteExistente = context.Clientes.FirstOrDefault(x => x.NumeroIdentificacion.Equals(cliente.NumeroIdentificacion));
            var clienteNuevo = mapper.Map<Cliente>(cliente);

            if (clienteExistente != null)
            {
                return new ErrorMessage()
                {
                    mensaje = "Ya existe un Cliente con ese numero de identificacion",
                    valido = true,
                };
            }

            // Agregar el nuevo cliente a la base de datos
            context.Clientes.Add(clienteNuevo);
            context.SaveChanges();

            return new ErrorMessage()
            {
                mensaje = "Cliente agregado correctamente",
                valido = true,
            };
        }

        // Método para obtener los datos de un cliente
        public ErrorMessage Datos_Cliente(string cedula)
        {
            // Consultar información del cliente y sus cuentas bancarias y préstamos asociados
            var clienteExistente = context.Clientes.Select(x => new
            {
                Nombre = x.Nombre + " " + x.Apellido,
                Telefono = x.Telefono,
                Correo = x.Email,
                Id = x.Id,
                Identificacion = x.NumeroIdentificacion,
                cuentasBancarias = x.CuentasBancaria.Where(c => c.ClienteId.Equals(x.Id)).Select(a => new
                {
                    tipo = a.TipoCuenta,
                    Estado = a.EstadoCuenta,
                    Saldo_Actual = a.SaldoActual,
                    Id = a.Id,
                    Habilitada = a.Habilitada,
                }),
                Prestamos = x.Prestamos.Where(p => p.ClienteId.Equals(x.Id)).Select(r => new
                {
                    Interes = r.TasaInteres,
                    Estado = r.EstadoPrestamo,
                    Id = r.Id
                })
            }).FirstOrDefault(c => c.Identificacion.Equals(cedula));

            if (clienteExistente == null)
            {
                return new ErrorMessage()
                {
                    mensaje = "No existe un cliente con ese numero de identificacion",
                };
            }

            return new ErrorMessage()
            {
                mensaje = clienteExistente,
            };
        }

        // Método para actualizar la información de un cliente
        public async Task<ErrorMessage> ActualizarCliente(CrearClienteDTO cliente)
        {
            try
            {
                // Buscar el cliente a actualizar en la base de datos
                var clienteA = await context.Clientes.FirstOrDefaultAsync(x => x.NumeroIdentificacion.Equals(cliente.NumeroIdentificacion));

                // Actualizar los datos del cliente con los valores proporcionados
                clienteA.Telefono = cliente.Telefono ?? clienteA.Telefono;
                clienteA.Apellido = cliente.Apellido ?? clienteA.Apellido;
                clienteA.Email = cliente.Email ?? clienteA.Email;
                clienteA.Nombre = cliente.Nombre ?? clienteA.Nombre;

                await context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return new ErrorMessage()
                {
                    mensaje = "Datos de cliente actualizado correctamente",
                    valido = true,
                };
            }
            catch (Exception ex)
            {
                return new ErrorMessage()
                {
                    mensaje = ex.Message,
                    valido = false,
                };
            }
        }

        // Método para eliminar un cliente
        public ErrorMessage EliminarCliente(string cedula)
        {
            // Buscar el cliente a eliminar en la base de datos
            var cliente = context.Clientes.FirstOrDefault(x => x.NumeroIdentificacion.Equals(cedula));

            if (cliente == null)
            {
                return new ErrorMessage()
                {
                    mensaje = "No existe cliente con esa cedula",
                    valido = true,
                };
            }

            // Eliminar el cliente de la base de datos
            context.Clientes.Remove(cliente);
            context.SaveChanges();

            return new ErrorMessage()
            {
                mensaje = "Cliente borrado correctamente",
                valido = true,
            };
        }
    }
}
