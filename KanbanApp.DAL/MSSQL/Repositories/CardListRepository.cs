using Microsoft.EntityFrameworkCore;

using KanbanApp.Common.Entities;

namespace KanbanApp.DAL.MSSQL.Repositories
{
    public class CardListRepository : BaseRepository<long, CardList>
    {
       public CardListRepository(DbContext context) 
            : base(context) { }
    }
}
