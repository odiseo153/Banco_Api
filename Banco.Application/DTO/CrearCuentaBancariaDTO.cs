namespace Banco.Application.DTO
{
    public class CrearCuentaBancariaDTO
    {
        public Guid? ClienteId { get; set; }
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoActual { get; set; }
        public string EstadoCuenta { get; set; } = null!;

    }
}
