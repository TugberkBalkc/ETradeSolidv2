using ETrade.Core.DataAccess.Abstract.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Abstract.UserAddress
{
    public interface IUserAddressQueryRepository : IQueryRepository<Entities.Concrete.UserAddress>
    {
    }
}
