//using System.IdentityModel.Tokens.Jwt; //(ask Nizar) // which one to choose 
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using realationshipss.Entities;

namespace realationshipss.security
{
    public class JwtProvider
    {
        private readonly IConfiguration _configuration;
        public JwtProvider(IConfiguration configuration)
        { 
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            string SecretKey = _configuration["jwt:SecretKey"]!;
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);


            var TokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(
                    [
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role),
                        //new Claim(JwtRegisteredClaimNames.Iss, "Bookapp"),
                        //new Claim(JwtRegisteredClaimNames.Aud, "Book_Audeince")
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["jwt:Expiration"]!)),
                SigningCredentials = Credentials,
                Issuer = _configuration["jwt:Issuer"],
                Audience = _configuration["jwt:Audience"]
            };

            var handler = new JsonWebTokenHandler(); // we used it instead of "jwtsecurityhandeler"
                                                     // because 30% faster and etc (ask Nizar)
            return handler.CreateToken(TokenDescriptor);

        }
    }
}
