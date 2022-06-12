using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.Category;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Concrete.EntityFramework.Category
{
    public class EfCategoryQueryRepository : EfQueryRepositoryBase<Entities.Concrete.Category>, ICategoryQueryRepository
    {
        public EfCategoryQueryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
