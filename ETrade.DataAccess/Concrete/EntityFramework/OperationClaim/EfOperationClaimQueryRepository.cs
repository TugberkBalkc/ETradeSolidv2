using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.OperationClaim;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Concrete.EntityFramework.OperationClaim
{
    public class EfOperationClaimQueryRepository : EfQueryRepositoryBase<Core.Entities.Concrete.OperationClaim>, IOperationClaimQueryRepository
    {
        public EfOperationClaimQueryRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
