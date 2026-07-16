using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ELearning.Models.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace ELearningPortal.Helpers
{
    // Simple helper to generate JWT tokens for logged in users.
    // Reads Jwt settings from appsettings.json (Jwt:Key, Jwt:Issuer, Jwt:Audience, Jwt:ExpiryMinutes).
    public class JwtHelper
    {
        private readonly IConfiguration _config;

        public JwtHelper(IConfiguration config)
        {
            _config = config;
        }

        // Build a JWT for the given user. Claims include UserId, Email, FullName, Role, BranchId
        public string GenerateToken(User user)
        {
            var jwtSection = _config.GetSection("Jwt");
            var key = jwtSection["Key"] ?? throw new Exception("Jwt:Key missing in appsettings");
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var expiryMinutes = int.Parse(jwtSection["ExpiryMinutes"] ?? "120");

            // These are the details we can read back from the token in every request
            var claims = new List<Claim>
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? string.Empty),
                new Claim("BranchId", user.BranchId.ToString()),
                new Claim("BranchName", user.Branch?.BranchName ?? string.Empty),
                new Claim("ProfileImage", user.ProfileImage ?? string.Empty)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
