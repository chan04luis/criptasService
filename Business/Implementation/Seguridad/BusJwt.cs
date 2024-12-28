using Microsoft.IdentityModel.Tokens;
using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementation.Seguridad
{
    public class BusJwt
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly int _tokenValidityInMinutes;
        private readonly int _refreshTokenValidityInDays;
        public DateTime GetRefreshTokenValidity { get { return DateTime.Now.AddDays(_refreshTokenValidityInDays); } }
        public BusJwt(string secretKey, string issuer)
        {
            _secretKey = secretKey;
            _issuer = issuer;
            _tokenValidityInMinutes = 5;
            _refreshTokenValidityInDays = 7;
        }
        public AutenticacionModelo GenerateJwtToken(List<Claim> authClaims, string audience)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var dtValid = DateTime.Now;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = dtValid.AddMinutes(_tokenValidityInMinutes),
                NotBefore = dtValid,
                Audience = audience,
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = GenerateRefreshToken();

            return new AutenticacionModelo
            {
                sToken = tokenHandler.WriteToken(token),
                sRefreshToken = refreshToken,
                dtExpires = token.ValidTo
            };
        }
        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey))
                }, out var validatedToken);

                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            if (String.IsNullOrEmpty(token)) return null;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
                ValidateLifetime = false,
                ValidIssuer = _issuer
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
        public bool IsTokenValid(string token)
        {
            var principal = GetPrincipalFromToken(token);
            return principal != null;
        }
        public AutenticacionModelo RefreshToken(string token)
        {
            var principal = GetPrincipalFromExpiredToken(token);

            if (principal == null)
            {
                return null;
            }

            var audience = principal.FindFirst("aud")?.Value ?? "";
            var claims = principal.Claims.Where(e => e.Type != "aud").ToList();

            return GenerateJwtToken(claims, audience);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
