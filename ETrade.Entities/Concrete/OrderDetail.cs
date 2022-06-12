using ETrade.Core.Entities.Abstract;

namespace ETrade.Entities.Concrete
{
    public class OrderDetail : EntityBase, IEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public double Discount { get; set; } = 0;

    }

}
