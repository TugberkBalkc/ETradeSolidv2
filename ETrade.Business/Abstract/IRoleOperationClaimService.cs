using ETrade.Core.Entities.Concrete;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Abstract
{
    public interface IRoleOperationClaimService
    {
        IResult AddClaimToRole(int roleId,int operationClaimId);
        IResult Delete(int roleId, int operationClaimId);
        IResult HardDelete(RoleOperationClaim roleOperationClaim);

        IDataResult<ObjectQueryableDto<OperationClaim>> GetOperationClaimsByRoleId(int roleId);
        IDataResult<ObjectQueryableDto<OperationClaim>> GetActiveOperationClaimsByRoleId(int roleId);
        IDataResult<ObjectQueryableDto<OperationClaim>> GetNonDeletedOperationClaimsByRoleId(int roleId);
        IDataResult<ObjectQueryableDto<OperationClaim>> GetActiveAndNonDeletedOperationClaimsByRoleId(int roleId);
    }
}
