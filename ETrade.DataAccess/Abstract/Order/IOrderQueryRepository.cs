using ETrade.Core.DataAccess.Abstract.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Abstract.Order
{
    public interface IOrderQueryRepository : IQueryRepository<Entities.Concrete.Order>
    {
    }
}
