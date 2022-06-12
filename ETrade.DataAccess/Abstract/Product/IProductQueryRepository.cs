using ETrade.Core.DataAccess.Abstract.Query;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Abstract.Product
{
    public interface IProductQueryRepository : IQueryRepository<Entities.Concrete.Product>
    {
        IQueryable<ProductDetailsDto> GetAllProductDetails(Expression<Func<ProductDetailsDto, bool>> filter = null);
        ProductDetailsDto GetProductDetails(Expression<Func<ProductDetailsDto, bool>> filter);
    }
}
