

namespace Banco.Application.DTO
{
    public class CrearTransaccionDTO
    {
        public Guid? CuentaId { get; set; }
        public Guid? CuentaDestinoId { get; set; }
        public decimal Monto { get; set; }
    }
}
