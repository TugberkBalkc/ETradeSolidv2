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
    public interface IOperationClaimService
    {

        IResult Add(OperationClaim operationClaim);
        IResult Update(OperationClaim operationClaim);
        IResult Delete(int operationClaimId);
        IResult HardDelete(OperationClaim operationClaim);

        IDataResult<ObjectDto<OperationClaim>> GetById(int operationClaimId);

        IDataResult<ObjectQueryableDto<OperationClaim>> GetAll();

        IDataResult<ObjectQueryableDto<OperationClaim>> GetAllActive();
        IDataResult<ObjectQueryableDto<OperationClaim>> GetAllNonDeleted();
        IDataResult<ObjectQueryableDto<OperationClaim>> GetAllActiveAndNonDeleted();

        IResult CheckIfOperationClaimExists(int operationClaimId);
    }
}
