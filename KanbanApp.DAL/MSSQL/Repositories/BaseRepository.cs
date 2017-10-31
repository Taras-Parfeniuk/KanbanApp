using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using KanbanApp.DAL.Abstract;
using KanbanApp.Common.Abstract;

namespace KanbanApp.DAL.MSSQL.Repositories
{
    public class BaseRepository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TKey : IComparable
        where TEntity : BaseEntity<TKey>
    {
        public IQueryable<TEntity> Query => _entities.AsQueryable();

        public BaseRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                _entities.Add(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                _context.Entry(entity).State = EntityState.Modified;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                _entities.Remove(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteByIdAsync(TKey id)
        {
            try
            {
                var entity = await GetByIdAsync(id);

                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                _entities.Remove(entity);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await Query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }
        
        private DbContext _context;
        private DbSet<TEntity> _entities;

    }
}
