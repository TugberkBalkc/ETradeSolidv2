﻿using ETrade.Core.DataAccess.Abstract.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Abstract.OperationClaim
{
    public interface IOperationClaimQueryRepository : IQueryRepository<Core.Entities.Concrete.OperationClaim>
    {
    }
}
