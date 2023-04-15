namespace kanban_websocket_back.Hubs.Clients
{
    public interface IKanbanClient
    {
        Task ReceiveMessage(dynamic message);
    }
}
