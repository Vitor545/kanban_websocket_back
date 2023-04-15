namespace kanban_websocket_back.Models
{
    public partial class Kanban
    {
        public Kanban()
        {
            Boards = new HashSet<Board>();
        }

        public long Id { get; set; }
        public string? KanbanName { get; set; }
        public long? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Board> Boards { get; set; }
    }
}
