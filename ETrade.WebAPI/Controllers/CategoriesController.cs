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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpPost("addcategory")]
        public IActionResult Add(Category category)
        {
            var result = _categoryService.Add(category);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpPut("updatecategory")]
        public IActionResult Update(Category category)
        {
            var result = _categoryService.Update(category);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpPut("deletecategory")]
        public IActionResult Delete(int categoryId)
        {
            var result = _categoryService.Delete(categoryId);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpDelete("harddeletecategory")]
        public IActionResult HardDelete(Category category)
        {
            var result = _categoryService.HardDelete(category);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getcategorybyid")]
        public IActionResult GetById(int categoryId)
        {
            var result = _categoryService.GetById(categoryId);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getcategories")]
        public IActionResult GetAll()
        {
            var result = _categoryService.GetAll();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getactivecategories")]
        public IActionResult GetAllActive()
        {
            var result = _categoryService.GetAllActive();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getnondeletedcategories")]
        public IActionResult GetAllNonDelted()
        {
            var result = _categoryService.GetAllNonDeleted();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getactiveandnondeletedcategories")]
        public IActionResult GetAllActiveAndNonDelted()
        {
            var result = _categoryService.GetAllActiveAndNonDeleted();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

    }
}
