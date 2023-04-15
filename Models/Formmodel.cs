namespace kanban_websocket_back.Models
{
    public partial class Formmodel
    {
        public long Id { get; set; }
        public string? Properties { get; set; }
        public long? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
