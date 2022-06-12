using ETrade.Core.Entities.Abstract;

namespace ETrade.Entities.Concrete
{
    public class UserAddress : EntityBase, IEntity
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
    }
}
