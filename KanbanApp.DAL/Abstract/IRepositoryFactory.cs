using System;
using Microsoft.EntityFrameworkCore;

using KanbanApp.Common.Abstract;

namespace KanbanApp.DAL.Abstract
{
    public interface IRepositoryFactory
    {
        IRepository<TKey, TEntity> CreateRepository<TKey, TEntity>(DbContext context)
            where TKey : IComparable
            where TEntity : BaseEntity<TKey>;
    }
}
