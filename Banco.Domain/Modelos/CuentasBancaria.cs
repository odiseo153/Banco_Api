using System;
using System.Collections.Generic;

namespace Banco.Infraestructure.Modelos
{
    public partial class CuentasBancaria
    {
        public CuentasBancaria()
        {
            Transacciones = new HashSet<Transaccione>();
        }

        public Guid Id { get; set; }
        public Guid? ClienteId { get; set; }
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoActual { get; set; }
        public DateTime? FechaApertura { get; set; } = DateTime.Now;
        public string EstadoCuenta { get; set; } = null!;
        public bool Habilitada { get; set; } = true;

        public virtual Cliente? Cliente { get; set; }
        public virtual ICollection<Transaccione> Transacciones { get; set; }
    }
}
