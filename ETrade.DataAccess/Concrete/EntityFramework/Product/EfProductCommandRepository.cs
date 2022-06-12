using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Concrete.EntityFramework.Product
{
    public class EfProductCommandRepository : EfCommandRepositoryBase<Entities.Concrete.Product>, IProductCommandRepository
    {
        public EfProductCommandRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
