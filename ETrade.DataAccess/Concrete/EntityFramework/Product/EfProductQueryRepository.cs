using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.Product;
using ETrade.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ETrade.DataAccess.Concrete.EntityFramework.Product
{
    public class EfProductQueryRepository : EfQueryRepositoryBase<Entities.Concrete.Product>, IProductQueryRepository
    {
        private DbContext _context => this.GetContext;
        public EfProductQueryRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<ProductDetailsDto> GetAllProductDetails(Expression<Func<ProductDetailsDto, bool>> filter = null)
        {
            var result = from product in _context.Set<Entities.Concrete.Product>()
                         join category in _context.Set<Entities.Concrete.Category>()
                         on product.CategoryId equals category.Id
                         select new ProductDetailsDto
                         {
                             ProductId = product.Id,
                             CategoryId = category.Id,
                             CategoryName = category.Name,
                             ProductName = product.Name,
                             ProductDetail = product.Detail,
                             ProductUnitPrice = product.UnitPrice,
                             ProductStockAmount = product.StockAmount,
                             ProductStockCode = product.StockCode,
                             ProductCreatedDate = product.CreatedDate,
                             ProductUpdatedDate = product.UpdatedDate,
                             ProductIsActive = product.IsActive
                         };

            return filter == null
                ? result
                : result.Where(filter);
        }

        public ProductDetailsDto GetProductDetails(Expression<Func<ProductDetailsDto, bool>> filter)
        {
            var result = from product in _context.Set<Entities.Concrete.Product>()
                         join category in _context.Set<Entities.Concrete.Category>()
                         on product.CategoryId equals category.Id
                         select new ProductDetailsDto
                         {
                             ProductId = product.Id,
                             CategoryName = category.Name,
                             ProductName = product.Name,
                             ProductDetail = product.Detail,
                             ProductUnitPrice = product.UnitPrice,
                             ProductStockAmount = product.StockAmount,
                             ProductStockCode = product.StockCode,
                             ProductCreatedDate = product.CreatedDate,
                             ProductUpdatedDate = product.UpdatedDate,
                             ProductIsActive = product.IsActive
                         };

            return result.SingleOrDefault(filter);
        }
    }
}
