using ETrade.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.DataAccess.Abstract.Repository
{
    public interface IRepository <TEntity>
        where TEntity : class, IEntity, new()
    {
    }
}
