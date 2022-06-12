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
    public interface IUserService
    {
        IDataResult<ObjectDto<User>> GetUserByEmail(string email);
        IDataResult<ObjectDto<UserDetailsDto>> GetUserDetailsByEmail(string email);
        IDataResult<ObjectQueryableDto<User>> GetUsers();
        IDataResult<ObjectQueryableDto<UserDetailsDto>> GetUserDetails();
        IDataResult<ObjectQueryableDto<UserDetailsDto>> GetVerifiedUserDetails();



        IDataResult<ObjectQueryableDto<OperationClaim>> GetUsersClaimsById(int userId);
        IDataResult<ObjectQueryableDto<OperationClaim>> GetActiveUsersClaimsById(int userId);
        IDataResult<ObjectQueryableDto<OperationClaim>> GetNonDeletedUsersClaimsById(int userId);
        IDataResult<ObjectQueryableDto<OperationClaim>> GetActiveAndNonDeletedUsersClaimsById(int userId);

        IDataResult<ObjectDto<User>> GetById(int userId);
        IDataResult<ObjectDto<UserDetailsDto>> GetUserDetailsById(int userId);

        IResult Add(User user);
        IResult Delete(int userId);
        IResult HardDelete(User user);
        IResult Update(User user);

        IDataResult<ObjectQueryableDto<User>> GetUsersActive();
        IDataResult<ObjectQueryableDto<User>> GetUsersNonDeleted();
        IDataResult<ObjectQueryableDto<User>> GetUsersActiveAndNonDeleted();

        IResult CheckIfUserExists(int userId);
        IResult CheckIfUserExists(string email);
        IResult CheckIfUserAddedBefore(string email);
    }
}
