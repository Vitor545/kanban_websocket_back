namespace kanban_websocket_back.Models
{
    public partial class Board
    {
        public Board()
        {
            Leads = new HashSet<Lead>();
        }

        public long Id { get; set; }
        public string? BoardName { get; set; }
        public string? Color { get; set; }
        public long? KanbanId { get; set; }
        public string? PropsBoard { get; set; }
        public long? UserId { get; set; }

        public virtual Kanban? Kanban { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Lead> Leads { get; set; }
    }
}
