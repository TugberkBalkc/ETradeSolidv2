using ETrade.Core.DataAccess.Abstract.Repository;
using ETrade.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.DataAccess.Abstract.Query
{
    public interface IQueryRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
    }
   
}
