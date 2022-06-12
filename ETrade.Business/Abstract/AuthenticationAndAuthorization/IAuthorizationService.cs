using ETrade.Core.Entities.Concrete;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.Core.Utilities.Security;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Abstract.AuthenticationAndAuthorization
{
    public interface IAuthorizationService
    {
        IDataResult<ObjectDto<AccessToken>> CreateAccessToken(User user);
        IResult Login(UserForLoginDto userForLoginDto);
        IResult Register(UserForRegisterDto userForRegisterDto);

    }
}
