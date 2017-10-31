using Microsoft.EntityFrameworkCore;

using KanbanApp.Common.Entities;

namespace KanbanApp.DAL.MSSQL.Repositories
{
    public class CardRepository : BaseRepository<long, Card>
    {
        public CardRepository(DbContext context) 
            : base(context) { }
    }
}
