using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace palugada_api.Helpers {
    public class JwtAuthenticationManager : IJwtAuthenticationManager {
        private readonly string key;
        public JwtAuthenticationManager(string key) {
            this.key = key;
        }

        public string GenerateToken(string username) {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] tokenKey = Encoding.ASCII.GetBytes(key);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.UserData, username)
                }),
                Expires = System.DateTime.Now.AddDays(365),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);

            string? tokenString = tokenHandler.WriteToken(token);
            return tokenString;


        }

    }
}
