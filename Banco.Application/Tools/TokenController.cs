using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Banco.Application.Tools
{
    public class TokenController
    {
        public TokenController() { }

        // Método para generar un token JWT
        public static string GenerarToken(string nombre, string rol)
        {
            // Definición de las reclamaciones (claims) del token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, nombre),
                new Claim(ClaimTypes.Role, rol),
            };

            // Configuración de la firma y la información de audiencia del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("odiseo es el mejor y lo sabes odiseo es el mejor y lo sabes odiseo es el mejor y lo sabes odiseo es el mejor y lo sabes odiseo es el mejor y lo sabes odiseo es el mejor y lo sabes")),
                    SecurityAlgorithms.HmacSha256)
            };

            // Creación del manejador de tokens JWT y generación del token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Conversión del token en una cadena de texto
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
