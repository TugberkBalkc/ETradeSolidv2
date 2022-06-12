using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.PaymentType;
using Microsoft.EntityFrameworkCore;

namespace ETrade.DataAccess.Concrete.EntityFramework.PaymentType
{
    public class EfPaymentTypeQueryRepository : EfQueryRepositoryBase<Entities.Concrete.PaymentType>, IPaymentTypeQueryRepository
    {
        public EfPaymentTypeQueryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
