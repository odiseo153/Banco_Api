using Banco.Application.Queries;
using Banco.Infraestructure.Modelos;
using System.IdentityModel.Tokens.Jwt;

namespace Banco.Application.Tools
{
    public class Validaciones
    {
        // Método para bloquear cuentas asociadas a préstamos vencidos
        public static void Bloquear_Al_Que_Debe(Banco_DBContext context)
        {
            // Obtener préstamos vencidos
            var prestamosVencidos = context.Prestamos.Where(p => p.PlazoPrestamo < DateTime.Now);

            foreach (var prestamo in prestamosVencidos)
            {
                // Obtener cuentas bancarias del cliente asociado al préstamo vencido
                var cuentasCliente = context.CuentasBancarias.Where(c => c.ClienteId.Equals(prestamo.ClienteId));

                foreach (var cuenta in cuentasCliente)
                {
                    cuenta.Habilitada = false; // Deshabilitar la cuenta
                }
            }

            // Reactivar cuentas inactivas de clientes sin préstamos
            foreach (var cliente in context.Clientes.ToList())
            {
                var prestamos = context.Prestamos.Where(c => c.ClienteId.Equals(cliente.Id));

                if (!prestamos.Any())
                {
                    var cuentasInactivas = context.CuentasBancarias.Where(c => c.Habilitada == false && c.ClienteId.Equals(cliente.Id));

                    foreach (var cuenta in cuentasInactivas)
                    {
                        cuenta.Habilitada = true; // Habilitar la cuenta
                    }
                }
            }

            context.SaveChanges(); // Guardar los cambios en la base de datos
        }

        // Método para validar la creación de un usuario
        public static ErrorMessage ValidarUsuario(Usuario user, string rol)
        {
            string[] roles = { "admin", "ejecutivo", "director", "gerente" };

            // Verificar si el rol del usuario que intenta crear es válido
            if (!roles.Any(r => r.ToLower() == user.Rol.ToLower()))
            {
                return new ErrorMessage()
                {
                    mensaje = "Los roles disponibles son: admin, ejecutivo, director, gerente",
                    valido = false
                };
            }

            // Validaciones específicas según el rol del usuario que realiza la acción
            if (rol.ToLower() == "gerente" && (user.Rol.ToLower() != "director" || user.Rol.ToLower() != "ejecutivo"))
            {
                return new ErrorMessage()
                {
                    mensaje = "El gerente solo puede crear a un director y ejecutivo",
                    valido = false
                };
            }

            if (rol.ToLower() == "director" && user.Rol.ToLower() != "ejecutivo")
            {
                return new ErrorMessage()
                {
                    mensaje = "El director solo puede crear a ejecutivo",
                    valido = false
                };
            }

            return new ErrorMessage()
            {
                mensaje = "Usuario creado exitosamente",
                valido = true
            };
        }

        // Método para validar el rol desde un token JWT
        public static JwtSecurityToken ValidarRolFromToken(string authorizationHeader)
        {
            // Extrae el token excluyendo el esquema "Bearer "
            string token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var tokenHandler = new JwtSecurityTokenHandler();

            // Lee y valida el token (sin verificar la firma)
            var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            return jsonToken;
        }
    }
}
