using ETrade.Core.DataAccess.Abstract.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Abstract.Role
{
    public interface IRoleQueryRepository : IQueryRepository<Core.Entities.Concrete.Role>
    {
        public IQueryable<Core.Entities.Concrete.OperationClaim> GetOperationClaimsByRoleId(int roleId);
        public IQueryable<Core.Entities.Concrete.OperationClaim> GetOperationClaimsByRole(Core.Entities.Concrete.Role role);
    }
}
