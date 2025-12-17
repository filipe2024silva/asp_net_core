using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class TokenService : ITokenService
    {
        public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config)
        {
            var key = _config.GetSection("JWT").GetValue<string>("SecretKey") ?? throw new InvalidOperationException("Invalid secret key");

            var privateKey = System.Text.Encoding.UTF8.GetBytes(key);

            var sgningCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_config.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")),
                Audience = _config.GetSection("JWT").GetValue<string>("ValidAudience"),
                Issuer = _config.GetSection("JWT").GetValue<string>("ValidIssuer"),
                SigningCredentials = sgningCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return securityToken;
        }

        public string GenerateRefreshToken()
        {
            var secureRandomNumber = new byte[128];

            using var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(secureRandomNumber);

            return Convert.ToBase64String(secureRandomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token, IConfiguration _config)
        {
            var secretKey = _config["JWT:SecretKey"] ?? throw new InvalidOperationException("Invalid key");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(secretKey)
                ),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(
                token,
                tokenValidationParameters,
                out SecurityToken securityToken
            );

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;

        }
    }
}
