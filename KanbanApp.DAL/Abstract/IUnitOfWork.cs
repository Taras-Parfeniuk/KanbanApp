using System;
using System.Threading.Tasks;

using KanbanApp.Common.Entities;

namespace KanbanApp.DAL.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<long, Card> CardRepository { get; }
        IRepository<long, CardList> CardListRepository { get; }

        Task SaveAsync();
    }
}
