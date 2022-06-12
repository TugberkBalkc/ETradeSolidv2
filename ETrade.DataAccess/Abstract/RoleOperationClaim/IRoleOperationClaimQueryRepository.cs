using ETrade.Core.DataAccess.Abstract.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Abstract.UserOperationClaim
{
    public interface IRoleOperationClaimQueryRepository : IQueryRepository<Core.Entities.Concrete.RoleOperationClaim>
    {
    }
}
