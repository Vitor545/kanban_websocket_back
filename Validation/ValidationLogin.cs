using kanban_websocket_back.Interfaces.Login;
using kanban_websocket_back.Jwt;
using kanban_websocket_back.Tokens;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace kanban_websocket_back.Validation
{
    public class LoginValidationL : ILoginValidation
    {
        private readonly LoginValidation _loginValidation = new LoginValidation();
        private readonly Token _token = new Token();
        public string NewEncrypt(string text, int key)
        {
            string result = "";
            string salt = "pFpsOyaekMRDTfreOQCAvlFLxo46yY_3vePGUemOGfv7-QIlFg_W4ZrQc75wYROi-94zvRN2f_DfHH1FwI_Jug";
            for (int i = 0; i < text.Length; i++)
            {
                result += (char)((int)text[i] + key);
            }
            var valueBytes = KeyDerivation.Pbkdf2(
                                password: result,
                                salt: Encoding.UTF8.GetBytes(salt),
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public JwtToken? ValidateToken(string tokenBearer)
        {
            if (tokenBearer == null)
                return null;
            var token = tokenBearer.Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Authentication")["SecurityKey"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
                var login = jwtToken.Claims.First(x => x.Type == "email").Value;
                var newToken = new JwtTokenBuilder()
                                            .AddSecurityKey(JwtSecurityKey.Create(_token.JwtKey()))
                                            .AddSubject(login)
                                            .AddClaim("id", userId)
                                            .AddClaim("email", login)
                                            .AddExpiry(_token.JwtExpiry("User"))
                                            .AddIssuer(_token.JwtIssuer())
                                            .AddAudience(_token.JwtAudience())
                                            .Build();
                return newToken;
            }
            catch
            {
                return null;
            }
        }


        public bool Validate(string value, int key, string hash)
            => NewEncrypt(value, key) == hash;
    }
}
