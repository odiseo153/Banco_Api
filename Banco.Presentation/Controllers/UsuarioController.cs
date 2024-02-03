using Banco.Application.DTO;
using Banco.Application.Queries;
using Banco.Application.Tools;
using Banco.Infraestructure.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Banco.Presentation.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuariosController usuario;

        
        public UsuarioController(UsuariosController user)
        {
            this.usuario = user;
        }

        [HttpPost("Login")]
        public ErrorMessage Login(string nombre,string clave)
        {
            //HttpContext.Session.SetString("token", usuario.Login(nombre, clave).token ?? "");

            return usuario.Login(nombre,clave);
        }

 
        //[Authorize]
        [HttpPost("Usuario")]
        public string Crear([FromBody]CrearUsuarioDTO user)
        {
         //var tokenPayload = ObtenerRoleDesdeToken();

         return usuario.CrearUsuario(user, ObtenerRoleDesdeToken());

        }


        private string ObtenerRoleDesdeToken()
        {
            string token = HttpContext.Request.Headers["Authorization"][0].Substring("Bearer ".Length)?.Trim() ?? "";
            var jsonToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            return jsonToken?.Claims.FirstOrDefault(x => x.Type.Equals("role"))?.Value;
        }


        [Authorize]
        [HttpDelete("Usuario")]
        public string Borrar(string id)
        {
            return usuario.EliminarUsuario(id);
        }


        [Authorize]
        [HttpGet("Usuario/{id}")]
        public Usuario UserById(string id)
        {
            return usuario.UsuarioById(id);
        }


        //[Authorize(Roles ="admin")]
        [HttpGet("Usuario")]
        public List<Usuario> Users()
        {
            return usuario.Usuarios();
        }



    }
}
