namespace KanbanApp.Common.DTO
{
    public class CardDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public long ListId { get; set; }
    }
}
