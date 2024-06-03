using Afiliacion.Models.Sesion;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Afiliacion.Services.AppServices;
public class TokenizerService {
    public UserModel DecodeToken(string token) {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var claims = jwtToken.Claims;

        return new UserModel {
            Id = claims.FirstOrDefault(c => c.Type == "unique_name")?.Value,
            Lvl = claims.FirstOrDefault(c => c.Type == "Lvl")?.Value,
            Name = claims.FirstOrDefault(c => c.Type == "Name")?.Value,
            Apps = claims.FirstOrDefault(c => c.Type == "Apps")?.Value
        };
    }
    public async Task<bool> ValidateToken(string token) {
        try {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Aqui_Va_La_SUPER_HYPER_MEGA_clave_secreta_**");
            var validationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false, // Puedes establecer estos valores según los requisitos de tu aplicación
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true
            };

            // Validar el token
            handler.ValidateToken(token, validationParameters, out _);

            return true;
        } catch (Exception ex) {
            Console.WriteLine($"Error al validar el token: {ex.Message}");
            return false;
        }
    }
}
