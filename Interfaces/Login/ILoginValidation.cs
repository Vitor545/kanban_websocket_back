namespace kanban_websocket_back.Interfaces.Login
{
    public interface ILoginValidation
    {
        string NewEncrypt(string text, int key);
    }
}