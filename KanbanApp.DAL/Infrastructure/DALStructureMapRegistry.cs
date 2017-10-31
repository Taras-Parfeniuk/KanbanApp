using Microsoft.EntityFrameworkCore;

using StructureMap;
using StructureMap.AutoFactory;

using KanbanApp.DAL.Abstract;
using KanbanApp.Common.Entities;
using KanbanApp.DAL.MSSQL.Repositories;
using KanbanApp.DAL.MSSQL;

namespace KanbanApp.DAL.Infrastructure
{
    public class DALStructureMapRegistry : Registry
    {
        public DALStructureMapRegistry()
        {
            For<IRepository<long, Card>>().Use<CardRepository>();
            For<IRepository<long, CardList>>().Use<CardListRepository>();
            For<IUnitOfWork>().Use<UnitOfWork>();
            For<IUnitOfWorkFactory>().CreateFactory();
            For<IRepositoryFactory>().CreateFactory();
            For<DbContext>().Use<KanbanDbContext>();
            For<KanbanDbInitializer>().Use<KanbanDbInitializer>();
        }
    }
}
