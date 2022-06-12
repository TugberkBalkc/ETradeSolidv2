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
    public interface IRoleService
    {
        IResult Add(Role role);
        IResult Update(Role role);
        IResult Delete(int roleId);
        IResult HardDelete(Role role);

        IDataResult<ObjectDto<Role>> GetById(int roleId);

        IDataResult<ObjectQueryableDto<Role>> GetAll();

        IDataResult<ObjectQueryableDto<Role>> GetAllActive();
        IDataResult<ObjectQueryableDto<Role>> GetAllNonDeleted();
        IDataResult<ObjectQueryableDto<Role>> GetAllActiveAndNonDeleted();

        IDataResult<ObjectQueryableDto<OperationClaim>> GetOperationClaimsByRoleId(int roleId);

        IResult CheckIfRoleExists(int roleId);
    }
}
