using Microsoft.EntityFrameworkCore;


namespace Banco.Infraestructure.Modelos
{
    public partial class Banco_DBContext : DbContext
    {
        public Banco_DBContext()
        {
        }

        public Banco_DBContext(DbContextOptions<Banco_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<CuentasBancaria> CuentasBancarias { get; set; } = null!;
        public virtual DbSet<Prestamo> Prestamos { get; set; } = null!;
        public virtual DbSet<Transaccione> Transacciones { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=odiseo\\ODISEO;Database=Banco_DB;User Id=odiseo;Password=padomo153;TrustServerCertificate=True;");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Apellido).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.NumeroIdentificacion).HasMaxLength(20);

                entity.Property(e => e.Telefono).HasMaxLength(20);

            });

            modelBuilder.Entity<CuentasBancaria>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.EstadoCuenta).HasMaxLength(20);

                entity.Property(e => e.FechaApertura)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                
                entity.Property(e => e.Habilitada).HasDefaultValueSql("((0))");

                entity.Property(e => e.SaldoActual).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TipoCuenta).HasMaxLength(20);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.CuentasBancaria)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__CuentasBa__Clien__286302EC");
            });

            modelBuilder.Entity<Prestamo>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.EstadoPrestamo).HasMaxLength(20);

                entity.Property(e => e.MontoPrestamo).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PlazoPrestamo).HasColumnType("datetime");

                entity.Property(e => e.TasaInteres).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Prestamos)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__Prestamos__Clien__32E0915F");
            });

            modelBuilder.Entity<Transaccione>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CuentaId).HasColumnName("CuentaID");

                entity.Property(e => e.FechaHoraTransaccion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TipoTransaccion).HasMaxLength(20);

                entity.HasOne(d => d.Cuenta)
                    .WithMany(p => p.Transacciones)
                    .HasForeignKey(d => d.CuentaId)
                    .HasConstraintName("FK__Transacci__Cuent__2E1BDC42");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.UsuarioId)
                    .HasColumnName("UsuarioID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Contraseña).HasColumnType("nvarchar(max)");

                entity.Property(e => e.NombreUsuario).HasMaxLength(50);

                entity.Property(e => e.Rol)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
