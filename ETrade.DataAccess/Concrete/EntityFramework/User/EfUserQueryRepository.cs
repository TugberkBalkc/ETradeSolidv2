using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.Core.Entities.Concrete;
using ETrade.DataAccess.Abstract.User;
using ETrade.DataAccess.Concrete.EntityFramework.Contexts;
using ETrade.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Concrete.EntityFramework.User
{
    public class EfUserQueryRepository : EfQueryRepositoryBase<Core.Entities.Concrete.User>, IUserQueryRepository
    {
        private DbContext _context => this.GetContext;
        public EfUserQueryRepository(DbContext dbContext) :base(dbContext)
        {

        }

        public IQueryable<UserDetailsDto> GetAllUserDetails(Expression<Func<UserDetailsDto, bool>> filter = null)
        {
            var result = from user in _context.Set<Core.Entities.Concrete.User>()
                         join role in _context.Set<Core.Entities.Concrete.Role>()
                         on user.RoleId equals role.Id
                         select new UserDetailsDto
                         {
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Email = user.Email,
                             ContactNumber = user.ContactNumber,
                             IsActive = user.IsActive,
                             RoleName = role.Name,
                             LastProfileUpdateDate = user.UpdatedDate,
                             RegisteredDate = user.CreatedDate,
                             IsVerificated = user.IsVerificated
                         };

            return filter == null
                ? result
                : result.Where(filter);
        }

        public UserDetailsDto GetUserDetails(Expression<Func<UserDetailsDto, bool>> filter)
        {
            var result = from user in _context.Set<Core.Entities.Concrete.User>()
                         join role in _context.Set<Core.Entities.Concrete.Role>()
                         on user.RoleId equals role.Id
                         select new UserDetailsDto
                         {
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Email = user.Email,
                             ContactNumber = user.ContactNumber,
                             IsActive = user.IsActive,
                             RoleName = role.Name,
                             LastProfileUpdateDate = user.UpdatedDate,
                             RegisteredDate = user.CreatedDate
                         };

            return result.SingleOrDefault(filter);
        }
    }
}
