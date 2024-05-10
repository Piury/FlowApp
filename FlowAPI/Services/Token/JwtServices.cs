using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FlowDb.Services.Token
{
    public class JwtServices : IJwtServices
    {
        public IConfiguration configuration;

        public JwtServices(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public string GetToken(string name, string email, bool superAdmin)
        {
            var claims = new List<Claim>
            {
                new Claim("name", name),
                new Claim("email", email),
                new Claim("SuperAdmin", superAdmin.ToString())
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Get<string>() ?? string.Empty));
            var credentials = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}