using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using KanbanApp.Common.Entities;

namespace KanbanApp.DAL.MSSQL
{
    public class KanbanDbInitializer
    {
        public KanbanDbInitializer(DbContext context)
        {
            _context = context as KanbanDbContext;
        }

        public void Seed()
        {
            if (!_context.CardLists.Any())
            {
                _context.CardLists.Add(new CardList
                {
                    Cards = new List<Card>(),
                    Title = "To Do"
                });

                _context.CardLists.Add(new CardList
                {
                    Cards = new List<Card>(),
                    Title = "In Progress"
                });

                _context.CardLists.Add(new CardList
                {
                    Cards = new List<Card>(),
                    Title = "Done"
                });

                _context.SaveChanges();
            }
        }

        private KanbanDbContext _context;
    }
}
