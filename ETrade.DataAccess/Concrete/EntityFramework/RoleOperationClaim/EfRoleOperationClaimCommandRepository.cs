using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.UserOperationClaim;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Concrete.EntityFramework.UserOperationClaim
{
    public class EfRoleOperationClaimCommandRepository : EfCommandRepositoryBase<Core.Entities.Concrete.RoleOperationClaim>, IRoleOperationClaimCommandRepository
    {
        public EfRoleOperationClaimCommandRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
