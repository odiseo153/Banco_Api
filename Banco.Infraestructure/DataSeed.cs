using Banco.Infraestructure.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Infraestructure
{
    public class DataSeed
    {
        public static List<Usuario> SeedUsuarios()
        {
            return new List<Usuario>()
            {
                new Usuario(){  Contraseña= "12345",NombreUsuario="odiseo",Rol="admin"},
                new Usuario(){  Contraseña= "12345",NombreUsuario="javier",Rol="ejecutivo"},
                new Usuario(){ Contraseña= "12345",NombreUsuario="miguel",Rol="gerente"},
                new Usuario(){ Contraseña= "12345",NombreUsuario="jose",Rol="ejecutivo"},
                new Usuario(){ Contraseña= "12345",NombreUsuario="gabriel",Rol="gerente"},
                new Usuario(){ Contraseña= "12345",NombreUsuario="gabriel",Rol="admin"},
            };
        }

        public static List<Cliente> SeedCliente()
        {
            return new List<Cliente>()
            {
                new Cliente(){Id=Guid.Parse("c2d0b5c8-81b4-4e4d-9f45-076b0a8c5311"), Nombre="Odiseo",Apellido="Rincon",Email="odiseoRincon@gmail.com",NumeroIdentificacion="1234567",Telefono="8297890766"},
                new Cliente(){Id=Guid.Parse("a8e92717-2a69-4c07-9d81-50c06ef1d71e"),Nombre="javier",Apellido="mejia",Email="javier@gmail.com",NumeroIdentificacion="987654",Telefono="8097852525"},
                new Cliente(){Id=Guid.Parse("7b3f4fe4-9a89-4f8a-8f1d-2e6832a546a7"),Nombre="gabriel",Apellido="medina",Email="gabriel@gmail.com",NumeroIdentificacion="654321",Telefono="8097891236"},
                new Cliente(){Id = Guid.Parse("f2e7d6d1-3e3b-4d7e-8fc3-98837a14b366"), Nombre="santos",Apellido="martinez",Email="santos@gmail.com",NumeroIdentificacion="123789",Telefono="120125896"},
                new Cliente(){Id=Guid.Parse("e4290d02-7bb3-470c-ae94-d3e51a6e2ed5"), Nombre="jose",Apellido="Rincon",Email="jose@gmail.com",NumeroIdentificacion="456123",Telefono="8097829745"},
                new Cliente(){Id = Guid.Parse("4af06289-22cb-4f68-8e5b-ef7be5d29e2c"), Nombre="antonio",Apellido="mendez",Email="antonio@gmail.com",NumeroIdentificacion="741852",Telefono="849652384"},
            };
        }

        public static List<CuentasBancaria> SeedCuentasBancarias()
        {
            return new List<CuentasBancaria>()
    {
        new CuentasBancaria()
        {
            ClienteId = Guid.Parse("c2d0b5c8-81b4-4e4d-9f45-076b0a8c5311"), EstadoCuenta = "activa", FechaApertura = DateTime.Now,  Habilitada = true,SaldoActual = 1000, TipoCuenta = "ahorros" },
        new CuentasBancaria() 
        {
            ClienteId = Guid.Parse("c2d0b5c8-81b4-4e4d-9f45-076b0a8c5311"), EstadoCuenta = "activa",FechaApertura = DateTime.Now, Habilitada = true, SaldoActual = 500, TipoCuenta = "corriente"
        },
        new CuentasBancaria()
        {
            ClienteId = Guid.Parse("a8e92717-2a69-4c07-9d81-50c06ef1d71e"),EstadoCuenta = "activa", FechaApertura = DateTime.Now, Habilitada = true, SaldoActual = 1500,TipoCuenta = "ahorros"
        },
        new CuentasBancaria()
        {
            ClienteId = Guid.Parse("a8e92717-2a69-4c07-9d81-50c06ef1d71e"),EstadoCuenta = "activa", FechaApertura = DateTime.Now,Habilitada = true,SaldoActual = 200,TipoCuenta = "corriente"
        },
            new CuentasBancaria()
        {
            ClienteId = Guid.Parse("7b3f4fe4-9a89-4f8a-8f1d-2e6832a546a7"),EstadoCuenta = "En uso",FechaApertura = DateTime.Now,Habilitada = true, SaldoActual = 1000,TipoCuenta = "ahorros"
        },
        new CuentasBancaria()
        {
            ClienteId = Guid.Parse("7b3f4fe4-9a89-4f8a-8f1d-2e6832a546a7"),EstadoCuenta = "activa", FechaApertura = DateTime.Now, Habilitada = true,  SaldoActual = 500,TipoCuenta = "empresarial"
        },
        new CuentasBancaria()
        {
            ClienteId = Guid.Parse("f2e7d6d1-3e3b-4d7e-8fc3-98837a14b366"),EstadoCuenta = "activa", FechaApertura = DateTime.Now,Habilitada = true,   SaldoActual = 1500,TipoCuenta = "ahorros"
        },
        new CuentasBancaria()
        {
            ClienteId = Guid.Parse("f2e7d6d1-3e3b-4d7e-8fc3-98837a14b366"), EstadoCuenta = "activa", FechaApertura = DateTime.Now, Habilitada = true,SaldoActual = 200, TipoCuenta = "corriente"
        },
          new CuentasBancaria()
        {
            ClienteId = Guid.Parse("7b3f4fe4-9a89-4f8a-8f1d-2e6832a546a7"), EstadoCuenta = "En uso", FechaApertura = DateTime.Now,Habilitada = true,SaldoActual = 1000, TipoCuenta = "ahorros"
        },
        new CuentasBancaria()
        {
            ClienteId = Guid.Parse("7b3f4fe4-9a89-4f8a-8f1d-2e6832a546a7"), EstadoCuenta = "activa",   FechaApertura = DateTime.Now,   Habilitada = true, SaldoActual = 500, TipoCuenta = "empresarial"
        },
        new CuentasBancaria()
        {
            ClienteId = Guid.Parse("f2e7d6d1-3e3b-4d7e-8fc3-98837a14b366"), EstadoCuenta = "activa", FechaApertura = DateTime.Now, Habilitada = true, SaldoActual = 1500, TipoCuenta = "ahorros"
        },
        new CuentasBancaria()
        {
            ClienteId = Guid.Parse("f2e7d6d1-3e3b-4d7e-8fc3-98837a14b366"),  EstadoCuenta = "activa", FechaApertura = DateTime.Now, Habilitada = true,  SaldoActual = 200,TipoCuenta = "corriente"
        },
    };
        }

        public static List<Transaccione> SeedTransferencia()
        {
            return new List<Transaccione>()
            {
                new Transaccione(){},
                new Transaccione(){},
                new Transaccione(){},
                new Transaccione(){},
                new Transaccione(){},
                new Transaccione(){},
            };
        }

        public static List<Prestamo> SeedPrestamo()
        {
            return new List<Prestamo>()
            {
                new Prestamo(){},
                new Prestamo(){},
                new Prestamo(){},
                new Prestamo(){},
                new Prestamo(){},
                new Prestamo(){},
            };
        }

    }
}
