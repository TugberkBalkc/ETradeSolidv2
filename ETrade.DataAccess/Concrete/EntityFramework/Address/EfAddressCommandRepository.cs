using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ETrade.DataAccess.Concrete.EntityFramework.Address
{
    public class EfAddressCommandRepository : EfCommandRepositoryBase<Entities.Concrete.Address>, IAddressCommandRepository
    {
        public EfAddressCommandRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
