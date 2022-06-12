using ETrade.Business.Abstract;
using ETrade.Core.Entities.Concrete;
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
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("addrole")]
        public IActionResult Add(Role role)
        {
            var result = _roleService.Add(role);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpPut("updaterole")]
        public IActionResult Update(Role role)
        {
            var result = _roleService.Update(role);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpPut("deleterole")]
        public IActionResult Delete(int roleId)
        {
            var result = _roleService.Delete(roleId);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpDelete("harddeleterole")]
        public IActionResult HardDelete(Role role)
        {
            var result = _roleService.HardDelete(role);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getrole")]
        public IActionResult GetById(int roleId)
        {
            var result = _roleService.GetById(roleId);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getroles")]
        public IActionResult GetAll()
        {
            var result = _roleService.GetAll();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getactiveroles")]
        public IActionResult GetAllActive()
        {
            var result = _roleService.GetAllActive();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getnondeletedroles")]
        public IActionResult GetAllNonDeleted()
        {
            var result = _roleService.GetAllNonDeleted();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getactiveandnondeletedroles")]
        public IActionResult GetAllActiveAndNonDeleted()
        {
            var result = _roleService.GetAllActiveAndNonDeleted();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getauthorities")]
        public IActionResult GetOperationClaimsByRoleId(int roleId)
        {
            var result = _roleService.GetOperationClaimsByRoleId(roleId);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

    }
}
