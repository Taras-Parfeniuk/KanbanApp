using KanbanApp.Common.Abstract;

namespace KanbanApp.Common.Entities
{
    public class Card : BaseEntity<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public CardList CardList { get; set; }
        public int Position { get; set; }
    }
}
