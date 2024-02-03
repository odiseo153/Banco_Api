using AutoMapper;
using Banco.Application.DTO;
using Banco.Application.Tools;
using Banco.Infraestructure.Modelos;
using Microsoft.AspNetCore.Http;
using System.Data.Entity;


namespace Banco.Application.Queries
{

    public class UsuariosController
    {
        private Banco_DBContext context;
        private IMapper mapper;
        public IHttpContextAccessor http;

        public UsuariosController(IMapper map,Banco_DBContext context,IHttpContextAccessor con)
        {
            this.http = con;
            this.context = context;
            mapper = map;
            Validaciones.Bloquear_Al_Que_Debe(context);
        }

        
        public ErrorMessage Login(string nombre,string clave)
        {
            var usuario = context.Usuarios.FirstOrDefault(x => x.NombreUsuario.Equals(nombre) && x.Contraseña.Equals(clave));

            if (usuario == null)
            {
                return new ErrorMessage()
                {
                    mensaje = "no existe usuario con ese nombre y clave",
                    valido = false,
                };
            }


            return new ErrorMessage() {
                mensaje = $"Bienvenido seas {usuario.NombreUsuario}",
                token = TokenController.GenerarToken(usuario.NombreUsuario, usuario.Rol),
                valido = true 
            }; 
        }

      
        public string CrearUsuario(CrearUsuarioDTO user,string rol)
        {
            
           var usuario = mapper.Map<Usuario>(user);

           var validacion = Validaciones.ValidarUsuario(usuario, rol);

            if(!validacion.valido)
            {
                return validacion.mensaje.ToString();
            }

            context.Usuarios.Add(usuario);
            context.SaveChanges();

            return "Usuario Creado Correctamente";
        }
      
        public Usuario UsuarioById(string Id)
        {
            return context.Usuarios.FirstOrDefault(x => x.UsuarioId.Equals(Guid.Parse(Id)));
        }

        public List<Usuario> Usuarios()
        {
            return context.Usuarios.AsNoTracking().ToList();
        }
      
        public string EliminarUsuario(string Id)
        {
            var usuario = context.Usuarios.FirstOrDefault(x => x.UsuarioId.Equals(Guid.Parse(Id)));

            if (usuario == null) return "ese Usuario no existe";

            context.Usuarios.Remove(usuario);
            context.SaveChanges();

            return "Usuario Eliminado Correctamente";
        }

      

    }
}
