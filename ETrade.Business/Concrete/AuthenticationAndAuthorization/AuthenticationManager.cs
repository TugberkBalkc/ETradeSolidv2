using ETrade.Business.Abstract;
using ETrade.Business.Abstract.AuthenticationAndAuthorization;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Business.Constants.BusinessTitles;
using ETrade.Core.Entities.Concrete;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.Core.Utilities.Security;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete.AuthenticationAndAuthorization
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly IUserService _userService;

        public AuthenticationManager(IUserService userService)
        {
            _userService = userService;
        }

        public IResult VerificateUser(string email)
        {
           /* var logicResult =
                BusinessLogicEngine.Run
                (_userService.CheckIfUserExists(email));
            if (logicResult !=null)
            {
                return logicResult;
            }*/

            var userResult = _userService.GetUserByEmail(email);
            var entity = userResult.Data.Entity;
            entity.IsVerificated = true;
            var result = _userService.Update(entity);

            return new SuccessfulResult(BusinessMessages.UserVerificated, BusinessTitles.Successful); 
        }
    }
}
