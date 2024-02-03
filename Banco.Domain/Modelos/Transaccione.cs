using System;
using System.Collections.Generic;

namespace Banco.Infraestructure.Modelos
{
    public partial class Transaccione
    {
        public Guid Id { get; set; }
        public Guid? CuentaId { get; set; }
        public string? TipoTransaccion { get; set; } = "De pago";
        public decimal Monto { get; set; }
        public DateTime? FechaHoraTransaccion { get; set; } = DateTime.Now;

        public virtual CuentasBancaria? Cuenta { get; set; }
    }
}
