using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.Order;
using Microsoft.EntityFrameworkCore;

namespace ETrade.DataAccess.Concrete.EntityFramework.Order
{
    public class EfOrderCommandRepository : EfCommandRepositoryBase<Entities.Concrete.Order>, IOrderCommandRepository
    {
        public EfOrderCommandRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
