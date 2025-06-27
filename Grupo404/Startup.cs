using Microsoft.Owin;
using Owin;
using System.Text;
using System;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;

[assembly: OwinStartupAttribute(typeof(Grupo404.Startup))]
namespace Grupo404
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
			var issuer = "tu_issuer"; // El emisor de tu token (puede ser el dominio de tu aplicación)
			var audience = "tu_audiencia"; // La audiencia (quién está autorizado a consumir el token)
			var secretKey = "qSgD8V1pA+k7V5u6Sop9uwRtpNmseqlOeS0iNY6i01E="; // La clave secreta para firmar los JWT

			// El middleware de JWT
			app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
			{
				AuthenticationMode = AuthenticationMode.Active,
				TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = issuer,
					ValidAudience = audience,
					IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
					ValidateIssuerSigningKey = true,
					ValidateAudience = true,
					ValidateIssuer = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero // Evitar problemas con la expiración del token
				}
			});
		}
    }
}
