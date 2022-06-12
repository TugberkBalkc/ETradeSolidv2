using ETrade.Core.DataAccess.Abstract.Query;
using ETrade.Core.Entities.Concrete;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Abstract.User
{
    public interface IUserQueryRepository : IQueryRepository<Core.Entities.Concrete.User>
    {
        IQueryable<UserDetailsDto> GetAllUserDetails(Expression<Func<UserDetailsDto, bool>> filter = null);
        UserDetailsDto GetUserDetails(Expression<Func<UserDetailsDto, bool>> filter);
    }
}
