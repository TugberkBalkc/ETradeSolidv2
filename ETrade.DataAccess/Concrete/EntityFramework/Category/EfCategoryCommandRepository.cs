using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.Category;
using Microsoft.EntityFrameworkCore;

namespace ETrade.DataAccess.Concrete.EntityFramework.Category
{
    public class EfCategoryCommandRepository : EfCommandRepositoryBase<Entities.Concrete.Category>, ICategoryCommandRepository
    {
        public EfCategoryCommandRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
