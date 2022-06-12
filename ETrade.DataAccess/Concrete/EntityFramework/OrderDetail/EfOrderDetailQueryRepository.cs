using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.OrderDetail;
using Microsoft.EntityFrameworkCore;

namespace ETrade.DataAccess.Concrete.EntityFramework.OrderDetail
{
    public class EfOrderDetailQueryRepository : EfQueryRepositoryBase<Entities.Concrete.OrderDetail>, IOrderDetailQueryRepository
    {
        public EfOrderDetailQueryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
