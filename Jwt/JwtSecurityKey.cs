using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace kanban_websocket_back.Jwt
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}