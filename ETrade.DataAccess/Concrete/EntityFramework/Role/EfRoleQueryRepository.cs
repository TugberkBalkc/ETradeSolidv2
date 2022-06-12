using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.Role;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ETrade.DataAccess.Concrete.EntityFramework.Role
{
    public class EfRoleQueryRepository : EfQueryRepositoryBase<Core.Entities.Concrete.Role>, IRoleQueryRepository
    {
        private DbContext _context => this.GetContext;
        public EfRoleQueryRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Core.Entities.Concrete.OperationClaim> GetOperationClaimsByRoleId(int roleId)
        {
            var result = from operationClaim in _context.Set<Core.Entities.Concrete.OperationClaim>()
                         join roleOperationClaim in _context.Set<Core.Entities.Concrete.RoleOperationClaim>()
                         on operationClaim.Id equals roleOperationClaim.OperationClaimId
                         where roleOperationClaim.RoleId == roleId
                         select new Core.Entities.Concrete.OperationClaim
                         {
                             Id = operationClaim.Id,
                             Name = operationClaim.Name
                         };
            return result;

        }

        public IQueryable<Core.Entities.Concrete.OperationClaim> GetOperationClaimsByRole(Core.Entities.Concrete.Role role)
        {
            var result = from operationClaim in _context.Set<Core.Entities.Concrete.OperationClaim>()
                         join roleOperationClaim in _context.Set<Core.Entities.Concrete.RoleOperationClaim>()
                         on operationClaim.Id equals roleOperationClaim.OperationClaimId
                         where roleOperationClaim.RoleId == role.Id
                         select new Core.Entities.Concrete.OperationClaim
                         {
                             Id = operationClaim.Id,
                             Name = operationClaim.Name
                         };
            return result;

        }
    }
}
