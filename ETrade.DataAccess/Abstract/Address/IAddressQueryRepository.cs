using ETrade.Core.DataAccess.Abstract.Command;
using ETrade.Core.DataAccess.Abstract.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Abstract.Address
{
    public interface IAddressQueryRepository : IQueryRepository<Entities.Concrete.Address>
    { 
        public IQueryable<Entities.Concrete.Address> GetAddressesByUserId(int userId);
        public IQueryable<Entities.Concrete.Address> GetAddressesByUser(Core.Entities.Concrete.User user);
    }
}
