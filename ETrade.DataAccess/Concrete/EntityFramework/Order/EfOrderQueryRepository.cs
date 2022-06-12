using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Concrete.EntityFramework.Order
{
    public class EfOrderQueryRepository : EfQueryRepositoryBase<Entities.Concrete.Order>, IOrderQueryRepository
    {
        public EfOrderQueryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
