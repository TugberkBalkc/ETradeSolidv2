using ETrade.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.Security
{
    public interface ITokenHelper
    {
        AccessToken CreateAccessToken(User user, IQueryable<OperationClaim> operationClaims);
    }
}
