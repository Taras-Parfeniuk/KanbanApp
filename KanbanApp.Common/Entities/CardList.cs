using System.Collections.Generic;

using KanbanApp.Common.Abstract;

namespace KanbanApp.Common.Entities
{
    public class CardList : BaseEntity<long>
    {
        public string Title { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}
