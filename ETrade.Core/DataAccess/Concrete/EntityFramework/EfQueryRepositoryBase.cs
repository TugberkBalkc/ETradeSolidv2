using ETrade.Core.DataAccess.Abstract.Query;
using ETrade.Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.DataAccess.Concrete.EntityFramework
{
    public class EfQueryRepositoryBase<TEntity> : IQueryRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly DbContext _dbContext;

        public EfQueryRepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<TEntity> GetDbSet => _dbContext.Set<TEntity>();

        protected DbContext GetContext => _dbContext;

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return GetDbSet.SingleOrDefault(filter);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? GetDbSet
                : GetDbSet.Where(filter);
        }
    }
}
