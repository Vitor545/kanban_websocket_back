using kanban_websocket_back.Interfaces;

namespace kanban_websocket_back.Tokens
{
    public class Token : IToken
    {
        public string JwtKey()
        {
            var key = "i#4#Mcs}237=RGKPen&*ZXf$BozkU55ANY]x8y^}-hq;WE)-_zU$;Y+L]IPp+7=qP";
            return key;
        }

        public string JwtIssuer()
        {
            var issuer = "Kanban WebSocket";
            return issuer;
        }

        public string JwtAudience()
        {
            var audience = "Kanban WebSocket bearer";
            return audience;
        }

        public string JwtClaim(string level)
        {
            if (level == "Admin")
            {
                var claim = "MemberAdminId";
                return claim;
            }
            else if (level == "Login")
            {
                var claim = "MembershipId";
                return claim;
            }
            else if (level == "Email")
            {
                var claim = "EmailId";
                return claim;
            }
            else
            {
                var claim = "MembershipId";
                return claim;
            }
        }

        public int JwtExpiry(string level)
        {
            if (level == "Admin")
            {
                var expiry = 86400;
                return expiry;
            }
            else if (level == "Mobile")
            {
                var expiry = 480;
                return expiry;
            }
            else
            {
                var expiry = 480;
                return expiry;
            }
        }
    }
}