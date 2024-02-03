using System;
using System.Collections.Generic;

namespace Banco.Infraestructure.Modelos
{
    public partial class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string? Contraseña { get; set; }
        public string? Rol { get; set; }
    }
}
