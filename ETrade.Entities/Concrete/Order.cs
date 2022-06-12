using ETrade.Core.Entities.Abstract;
using System;

namespace ETrade.Entities.Concrete
{
    public class Order : EntityBase, IEntity
    {
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public int AddressId { get; set; }
        public DateTime ShippedDate { get; set; }
        public bool IsDelivered { get; set; } = false;
    }

}
