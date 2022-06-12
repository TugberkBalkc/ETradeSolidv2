using ETrade.Core.DataAccess.Abstract.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Abstract.OrderDetail
{
    public interface IOrderDetailQueryRepository : IQueryRepository<Entities.Concrete.OrderDetail>
    {
    }
}
