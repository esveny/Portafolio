using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Grupo404.Controllers
{
    public class AuthController : Controller
    { // GET: Auth
        public JsonResult Login(string usuario, string contrasena)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qSgD8V1pA+k7V5u6Sop9uwRtpNmseqlOeS0iNY6i01E="));

            var claims = new[]
           {
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim(ClaimTypes.Role, "Cliente"),
                new Claim(ClaimTypes.Role, "Contador")
            };

            // Crear el token JWT
            var token = new JwtSecurityToken(
                issuer: "tu_issuer",
                audience: "tu_audiencia",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // El token expira en 1 hora
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            // Generar el token como cadena
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Json(new { token = tokenString });
        }


    }
}
