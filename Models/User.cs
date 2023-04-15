namespace kanban_websocket_back.Models
{
    public partial class User
    {
        public User()
        {
            Boards = new HashSet<Board>();
            Formmodels = new HashSet<Formmodel>();
            Kanbans = new HashSet<Kanban>();
            Leads = new HashSet<Lead>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Board> Boards { get; set; }
        public virtual ICollection<Formmodel> Formmodels { get; set; }
        public virtual ICollection<Kanban> Kanbans { get; set; }
        public virtual ICollection<Lead> Leads { get; set; }
    }
}
