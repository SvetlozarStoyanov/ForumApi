using Contracts.Services.JWT;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.JWT
{
    public class JwtService : IJwtService
    {
        private readonly IOptions<JWTConfiguration> options;

        public JwtService(IOptions<JWTConfiguration> options)
        {
            this.options = options;
        }

        public string GenerateJwtToken(string userId, string userName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define the claims to include in the token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),       // User ID
                new Claim(JwtRegisteredClaimNames.UniqueName, userName), // Username
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique token ID
            };

            // Create the token with claims
            var token = new JwtSecurityToken(
                issuer: options.Value.Issuer,
                audience: options.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
