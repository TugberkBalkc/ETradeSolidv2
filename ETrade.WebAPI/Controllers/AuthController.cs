using ETrade.Business.Abstract;
using ETrade.Business.Abstract.AuthenticationAndAuthorization;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Core.Entities.Concrete;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Security;
using ETrade.Core.Utilities.WebAPI.UserCommunication.Notifications;
using ETrade.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETrade.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserService _userService;

        public AuthController(IAuthenticationService authenticationService,
            IAuthorizationService authorizationService,
            IUserService userService)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var loginResult = _authorizationService.Login(userForLoginDto);

            if (!loginResult.Success)
            {
                return BadRequest(new BadRequestNotification { Title = loginResult.Title, Message = loginResult.Message });
            }

            var userResult = (IDataResult<ObjectDto<User>>)loginResult;

            var result = _authorizationService.CreateAccessToken(userResult.Data.Entity);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);

        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var logicResult = BusinessLogicEngine.Run(_userService.CheckIfUserAddedBefore(userForRegisterDto.Email));
            if (logicResult != null)
            {
                return BadRequest(new BadRequestNotification { Title = logicResult.Title, Message = logicResult.Message });
            }

            var registerResult = _authorizationService.Register(userForRegisterDto);
            if (!registerResult.Success)
            {
                return BadRequest(new BadRequestNotification { Title = registerResult.Title, Message = registerResult.Message });
            }

            var userResult = (IDataResult<ObjectDto<User>>)registerResult;

            var result = _authorizationService.CreateAccessToken(userResult.Data.Entity);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message});
            }
        }

        [HttpPost("verifyaccount")]
        public IActionResult VerificateUser(string email)
        {
            var result = _authenticationService.VerificateUser(email);
            return result.Success == true ? Ok(result) : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }
    }
}
