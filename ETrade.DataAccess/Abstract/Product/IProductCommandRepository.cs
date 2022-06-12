using ETrade.Core.DataAccess.Abstract.Command;

namespace ETrade.DataAccess.Abstract.Product
{
    public interface IProductCommandRepository : ICommandRepository<Entities.Concrete.Product>
    {
    }
}
