using System;
using System.Collections.Generic;

namespace Banco.Infraestructure.Modelos
{
    public partial class Cliente
    {
        public Cliente()
        {
            CuentasBancaria = new HashSet<CuentasBancaria>();
            Prestamos = new HashSet<Prestamo>();
        }

        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string NumeroIdentificacion { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telefono { get; set; }

        public virtual ICollection<CuentasBancaria> CuentasBancaria { get; set; }
        public virtual ICollection<Prestamo> Prestamos { get; set; }
    }
}
