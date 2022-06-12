using ETrade.Core.DataAccess.Abstract.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Abstract.OperationClaim
{
    public interface IOperationClaimCommandRepository : ICommandRepository<Core.Entities.Concrete.OperationClaim>
    {
    }
}
