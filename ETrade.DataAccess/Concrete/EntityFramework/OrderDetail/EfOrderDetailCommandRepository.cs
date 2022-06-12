using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.OrderDetail;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Concrete.EntityFramework.OrderDetail
{
    public class EfOrderDetailCommandRepository : EfCommandRepositoryBase<Entities.Concrete.OrderDetail>, IOrderDetailCommandRepository
    {
        public EfOrderDetailCommandRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
