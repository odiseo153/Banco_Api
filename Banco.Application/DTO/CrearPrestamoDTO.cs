using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Application.DTO
{
    public class CrearPrestamoDTO
    {
        public Guid? ClienteId { get; set; }
        public decimal MontoPrestamo { get; set; }
        public string? EstadoPrestamo { get; set; }
    }
}
