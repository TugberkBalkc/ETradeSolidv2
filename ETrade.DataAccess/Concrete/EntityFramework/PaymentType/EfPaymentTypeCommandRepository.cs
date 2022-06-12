using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.PaymentType;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Concrete.EntityFramework.PaymentType
{
    public class EfPaymentTypeCommandRepository : EfCommandRepositoryBase<Entities.Concrete.PaymentType>, IPaymentTypeCommandRepository
    {
        public EfPaymentTypeCommandRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
