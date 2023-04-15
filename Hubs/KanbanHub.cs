using kanban_websocket_back.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;

namespace kanban_websocket_back.Hubs
{
    public class KanbanHub : Hub<IKanbanClient>
    { }
}
