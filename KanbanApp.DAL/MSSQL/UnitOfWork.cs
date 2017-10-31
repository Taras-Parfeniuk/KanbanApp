using System;
using System.Threading.Tasks;

using KanbanApp.DAL.Abstract;
using KanbanApp.Common.Entities;

namespace KanbanApp.DAL.MSSQL
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<long, Card> CardRepository {
            get
            {
                return _cardRepository ?? (_cardRepository = _factory.CreateRepository<long, Card>(_context));
            }
        }

        public IRepository<long, CardList> CardListRepository
        {
            get
            {
                return _cardListRepository ?? (_cardListRepository = _factory.CreateRepository<long, CardList>(_context));
            }
        }

        public UnitOfWork(KanbanDbContext context, IRepositoryFactory factory)
        {
            _context = context;
            _factory = factory;
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;

        private IRepositoryFactory _factory;
        private KanbanDbContext _context;

        private IRepository<long, Card> _cardRepository;
        private IRepository<long, CardList> _cardListRepository;
    }
}
