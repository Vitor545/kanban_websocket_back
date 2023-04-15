namespace kanban_websocket_back.Models
{
    public partial class Lead
    {
        public long Id { get; set; }
        public string? PropsLead { get; set; }
        public long? UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Comments { get; set; }
        public long? IndexNumber { get; set; }
        public string? Color { get; set; }
        public long? BoardId { get; set; }

        public virtual Board? Board { get; set; }
        public virtual User? User { get; set; }
    }
}
