using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.User;
using Microsoft.EntityFrameworkCore;

namespace ETrade.DataAccess.Concrete.EntityFramework.User
{
    public class EfUserCommandRepository : EfCommandRepositoryBase<Core.Entities.Concrete.User>, IUserCommandRepository
    {
        public EfUserCommandRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
