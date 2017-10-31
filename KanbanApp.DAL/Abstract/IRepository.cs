using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KanbanApp.Common.Abstract;

namespace KanbanApp.DAL.Abstract
{
    public interface IRepository<TKey, TEntity> 
        where TKey: IComparable 
        where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteByIdAsync(TKey id);
        IQueryable<TEntity> Query { get; }
    }
}
