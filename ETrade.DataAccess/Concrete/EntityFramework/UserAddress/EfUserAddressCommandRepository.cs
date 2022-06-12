using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.UserAddress;
using Microsoft.EntityFrameworkCore;

namespace ETrade.DataAccess.Concrete.EntityFramework.UserAddress
{
    public class EfUserAddressCommandRepository : EfCommandRepositoryBase<Entities.Concrete.UserAddress>, IUserAddressCommandRepository
    {
        public EfUserAddressCommandRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
