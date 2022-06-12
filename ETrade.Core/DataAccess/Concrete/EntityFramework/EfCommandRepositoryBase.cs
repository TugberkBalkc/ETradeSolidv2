using ETrade.Core.DataAccess.Abstract.Command;
using ETrade.Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.DataAccess.Concrete.EntityFramework
{
    public class EfCommandRepositoryBase<TEntity> : ICommandRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly DbContext _dbContext;

        public EfCommandRepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<TEntity> GetDbSet => _dbContext.Set<TEntity>();

        public TEntity Add(TEntity entity)
        {
            GetDbSet.Add(entity);
            return entity;
        }

        public void HardDelete(TEntity entity)
        {
            GetDbSet.Remove(entity);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            GetDbSet.Update(entity);
            return entity;
        }
    }
}
