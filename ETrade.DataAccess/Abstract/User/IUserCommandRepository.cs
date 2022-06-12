using ETrade.Core.DataAccess.Abstract.Command;

namespace ETrade.DataAccess.Abstract.User
{
    public interface IUserCommandRepository : ICommandRepository<Core.Entities.Concrete.User>
    {
    }
}
