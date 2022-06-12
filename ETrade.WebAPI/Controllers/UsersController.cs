using ETrade.Business.Abstract;
using ETrade.Core.Entities.Concrete;
using ETrade.Core.Utilities.WebAPI.UserCommunication.Notifications;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("adduser")]
        public IActionResult Add(User user)
        {
            var result = _userService.Add(user);
            return result.Success == true 
                ? Ok(result) 
                : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpPut("updateuser")]
        public IActionResult Update(User user)
        {
            var result = _userService.Update(user);
            return result.Success == true
                ? Ok(result)
                : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpPut("deleteuser")]
        public IActionResult Delete(int userId)
        {
            var result = _userService.Delete(userId);
            return result.Success == true
                ? Ok(result)
                : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpDelete("harddeleteuser")]
        public IActionResult HardDelete(User user)
        {
            var result = _userService.HardDelete(user);
            return result.Success == true
                ? Ok(result)
                : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getuserdetailsbyid")]
        public IActionResult GetUserDetailsById(int userId)
        {
            var result = _userService.GetUserDetailsById(userId);
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getuserdetailsbyemail")]
        public IActionResult GetUserDetailsByEmail(string email)
        {
            var result = _userService.GetUserDetailsByEmail(email);
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getuserbyid")]
        public IActionResult GetUserById(int userId)
        {
            var result = _userService.GetById(userId);
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getuserbyemail")]
        public IActionResult GetUserByEmail(string email)
        {
            var result = _userService.GetUserByEmail(email);
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getusers")]
        public IActionResult GetAll()
        {
            var result = _userService.GetUsers();
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getactiveusers")]
        public IActionResult GetAllActive()
        {
            var result = _userService.GetUsersActive();
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getnondeletedusers")]
        public IActionResult GetAllNonDeleted()
        {
            var result = _userService.GetUsersNonDeleted();
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getactiveandnondeletedusers")]
        public IActionResult GetAllActiveAndNonDeleted()
        {
            var result = _userService.GetUsersActiveAndNonDeleted();
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getverifiedusersdetails")]
        public IActionResult GetVerifiedUserDetails()
        {
            var result = _userService.GetVerifiedUserDetails();
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getusersdetails")]
        public IActionResult GetAllUserDetails()
        {
            var result = _userService.GetUserDetails();
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getusersauthoritiesbyid")]
        public IActionResult GetUsersClaimsByIn(int userId)
        {
            var result = _userService.GetUsersClaimsById(userId);
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getactiveusersauthoritiesbyid")]
        public IActionResult GetActiveUsersClaimsById(int userId)
        {
            var result = _userService.GetActiveUsersClaimsById(userId);
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getnondeletedusersauthoritiesbyid")]
        public IActionResult GetNonDeletedUsersClaimsById(int userId)
        {
            var result = _userService.GetNonDeletedUsersClaimsById(userId);
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }

        [HttpGet("getnondeletedandactiveusersauthoritiesbyid")]
        public IActionResult GetNonDeletedAndActiveUsersClaimsById(int userId)
        {
            var result = _userService.GetActiveAndNonDeletedUsersClaimsById(userId);
            return result.Success == true
            ? Ok(result)
            : BadRequest(new BadRequestNotification { Title = result.Title, Message = result.Message });
        }
    }
}
