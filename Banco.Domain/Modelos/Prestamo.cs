using System;
using System.Collections.Generic;

namespace Banco.Infraestructure.Modelos
{
    public partial class Prestamo
    {
        public Guid Id { get; set; }
        public Guid? ClienteId { get; set; }
        public decimal MontoPrestamo { get; set; }
        public decimal? TasaInteres { get; set; } = (decimal)0.15;
        public DateTime? PlazoPrestamo { get; set; }
        public string? EstadoPrestamo { get; set; }

        public virtual Cliente? Cliente { get; set; }
    }
}
