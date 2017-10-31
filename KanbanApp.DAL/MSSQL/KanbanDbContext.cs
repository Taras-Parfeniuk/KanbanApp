using Microsoft.EntityFrameworkCore;

using KanbanApp.Common.Entities;

namespace KanbanApp.DAL.MSSQL
{
    public class KanbanDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardList> CardLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=kanbandb;Trusted_Connection=True;");
        }
    }
}
