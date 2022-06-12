using ETrade.Core.DataAccess.Abstract.Repository;
using ETrade.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.DataAccess.Abstract.Command
{
    public interface ICommandRepository<TEntity> : IRepository<TEntity>
       where TEntity : class, IEntity, new()
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void HardDelete(TEntity entity);
        int SaveChanges();
    }
}
