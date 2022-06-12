using ETrade.Core.Entities.Abstract;

namespace ETrade.Entities.Concrete
{
    public class Address : EntityBase, IEntity
    {
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
