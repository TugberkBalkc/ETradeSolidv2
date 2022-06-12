using ETrade.Core.DataAccess.Abstract.Command;

namespace ETrade.DataAccess.Abstract.PaymentType
{
    public interface IPaymentTypeCommandRepository : ICommandRepository<Entities.Concrete.PaymentType>
    {
    }
}
