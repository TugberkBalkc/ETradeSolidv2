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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("addproduct")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpPut("updateproduct")]
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpPut("deleteproduct")]
        public IActionResult Delete(int productId)
        {
            var result = _productService.Delete(productId);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpDelete("harddeleteproduct")]
        public IActionResult HardDelete(Product product)
        {
            var result = _productService.HardDelete(product);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }


        [HttpGet("getproductbyid")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getproductbystockcode")]
        public IActionResult GetByStockCode(string stockCode)
        {
            var result = _productService.GetByStockCode(stockCode);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getproductsdetails")]
        public IActionResult GetAllDetails()
        { 
            var result = _productService.GetProductDetails();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getproductsdetailsbycategoryid")]
        public IActionResult GetAllDetailsByCategoryId(int categoryId)
        {
            var result = _productService.GetProductDetailsByCategoryId(categoryId);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getproducts")]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getactiveproducts")]
        public IActionResult GetAllActive()
        {
            var result = _productService.GetAllActive();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getnondeletedproducts")]
        public IActionResult GetAllNonDelted()
        {
            var result = _productService.GetAllNonDeleted();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getactiveandnondeletedproducts")]
        public IActionResult GetAllActiveAndNonDelted()
        {
            var result = _productService.GetAllActiveAndNonDeleted();
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getproductsbycategoryid")]
        public IActionResult GetAllById(int categoryId)
        {
            var result = _productService.GetAllByCategoryId(categoryId);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getproductsbyname")]
        public IActionResult GetAllByName(string productName)
        {
            var result = _productService.GetAllByName(productName);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getproductsbystock")]
        public IActionResult GetAllByStockAmount(short minStock)
        {
            var result = _productService.GetAllByStockAmount(minStock);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getproductsbystock")]
        public IActionResult GetAllByStockAmount(short minStock, short maxStock)
        {
            var result = _productService.GetAllByStockAmount(minStock, maxStock);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getproductsbyprice")]
        public IActionResult GetAllByUnitPrice(short minPrice)
        {
            var result = _productService.GetAllByStockAmount(minPrice);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }

        [HttpGet("getproductsbyprice")]
        public IActionResult GetAllByUnitPrice(short minPrice, short maxPrice)
        {
            var result = _productService.GetAllByStockAmount(minPrice, maxPrice);
            return result.Success == true ? Ok(result) : BadRequest(result.Title + "  " + result.Message);
        }


    }
}
