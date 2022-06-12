using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.Role;
using ETrade.DataAccess.Abstract.User;
using Microsoft.EntityFrameworkCore;

namespace ETrade.DataAccess.Concrete.EntityFramework.Role
{
    public class EfRoleCommandRepository : EfCommandRepositoryBase<Core.Entities.Concrete.Role>, IRoleCommandRepository
    {
        public EfRoleCommandRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
