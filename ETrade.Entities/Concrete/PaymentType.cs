using ETrade.Core.Entities.Abstract;

namespace ETrade.Entities.Concrete
{
    public class PaymentType : EntityBase, IEntity
    {
        public string Name { get; set; }
    }
}
