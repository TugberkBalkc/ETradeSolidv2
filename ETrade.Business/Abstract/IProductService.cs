using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.Entities.Concrete;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Abstract
{
    public interface IProductService
    {
        IResult Add(Product product);
        IResult Update(Product product);
        IResult Delete(int productId);
        IResult HardDelete(Product product);

        IDataResult<ObjectDto<Product>> GetById(int productId);
        IDataResult<ObjectDto<Product>> GetByStockCode(string stockCode);

        IDataResult<ObjectQueryableDto<Product>> GetAll();
        IDataResult<ObjectQueryableDto<Product>> GetAllByCategoryId(int categoryId);
        IDataResult<ObjectQueryableDto<Product>> GetAllByName(string productName);
        IDataResult<ObjectQueryableDto<Product>> GetAllByStockAmount(short minStockAmount);
        IDataResult<ObjectQueryableDto<Product>> GetAllByStockAmount(short minStockAmount, short maxStockAmount);
        IDataResult<ObjectQueryableDto<Product>> GetAllByUnitPrice(decimal minUnitPrice);
        IDataResult<ObjectQueryableDto<Product>> GetAllByUnitPrice(decimal minUnitPrice, decimal maxUnitPrice);
       
        IDataResult<ObjectQueryableDto<Product>> GetAllActive();
        IDataResult<ObjectQueryableDto<Product>> GetAllNonDeleted();
        IDataResult<ObjectQueryableDto<Product>> GetAllActiveAndNonDeleted();

        IDataResult<ObjectQueryableDto<ProductDetailsDto>> GetProductDetails();
        IDataResult<ObjectQueryableDto<ProductDetailsDto>> GetProductDetailsByCategoryId(int categoryId);

    }
}
