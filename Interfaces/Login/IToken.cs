namespace kanban_websocket_back.Interfaces
{
    public interface IToken
    {
        string JwtKey();
        string JwtIssuer();
        string JwtAudience();
        string JwtClaim(string level);
        int JwtExpiry(string level);
    }
}