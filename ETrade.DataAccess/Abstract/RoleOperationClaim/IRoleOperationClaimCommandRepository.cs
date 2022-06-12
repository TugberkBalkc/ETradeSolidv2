using ETrade.Core.DataAccess.Abstract.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Abstract.UserOperationClaim
{
    public interface IRoleOperationClaimCommandRepository : ICommandRepository<Core.Entities.Concrete.RoleOperationClaim>
    {
    }
}
