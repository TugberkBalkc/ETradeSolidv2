using ETrade.Business.Abstract;
using ETrade.Entities.Concrete;
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
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost("addaddress")]
        public IActionResult Add(Address address)
        {
            var result = _addressService.Add(address);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpPut("updateaddress")]
        public IActionResult Update(Address address)
        {
            var result = _addressService.Update(address);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpPut("deleteaddress")]
        public IActionResult Delete(int roleId)
        {
            var result = _addressService.Delete(roleId);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpDelete("harddeleteaddress")]
        public IActionResult HarDelete(Address address)
        {
            var result = _addressService.HardDelete(address);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getaddressbyid")]
        public IActionResult GetById(int addressId)
        {
            var result = _addressService.GetById(addressId);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getaddressbypostalcode")]
        public IActionResult GetByPostalCode(string postalCode)
        {
            var result = _addressService.GetByPostalCode(postalCode);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getaddresses")]
        public IActionResult GetAll()
        {
            var result = _addressService.GetAll();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getaddressesbycityname")]
        public IActionResult GetAllByCityName(string cityName)
        {
            var result = _addressService.GetAllByCityName(cityName);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getactiveaddresses")]
        public IActionResult GetAllActive()
        {
            var result = _addressService.GetAllActive();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getnondeletedaddresses")]
        public IActionResult GetAllNonDeleted()
        {
            var result = _addressService.GetAllNonDeleted();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getactiveandnondeletedaddresses")]
        public IActionResult GetAllActiveAndNonDeleted()
        {
            var result = _addressService.GetAllActiveAndNonDeleted();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }
    }
}
