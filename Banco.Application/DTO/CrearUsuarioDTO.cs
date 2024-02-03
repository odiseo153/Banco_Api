using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Application.DTO
{
    public class CrearUsuarioDTO
    {
        public string NombreUsuario { get; set; } = null!;
        public string? Contraseña { get; set; }
        public string? Rol { get; set; }
    }
}
