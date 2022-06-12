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
    public class EfRoleOperationClaimQueryRepository : EfQueryRepositoryBase<Core.Entities.Concrete.RoleOperationClaim>, IRoleOperationClaimQueryRepository
    {
        public EfRoleOperationClaimQueryRepository(DbContext dbContext) : base(dbContext) 
        {

        }
    }
}
