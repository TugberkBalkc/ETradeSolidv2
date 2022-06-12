using ETrade.Core.DataAccess.Abstract.Command;

namespace ETrade.DataAccess.Abstract.Order
{
    public interface IOrderCommandRepository : ICommandRepository<Entities.Concrete.Order>
    {
    }
}
