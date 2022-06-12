using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.UserAddress;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Concrete.EntityFramework.UserAddress
{
    public class EfUserAddressQueryRepository : EfQueryRepositoryBase<Entities.Concrete.UserAddress>, IUserAddressQueryRepository
    {
        public EfUserAddressQueryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
