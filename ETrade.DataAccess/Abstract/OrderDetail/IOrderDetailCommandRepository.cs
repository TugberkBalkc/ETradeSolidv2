using ETrade.Core.DataAccess.Abstract.Command;

namespace ETrade.DataAccess.Abstract.OrderDetail
{
    public interface IOrderDetailCommandRepository : ICommandRepository<Entities.Concrete.OrderDetail>
    {
    }
}
