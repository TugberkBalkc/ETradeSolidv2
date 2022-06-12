using ETrade.Core.Entities.Abstract;

namespace ETrade.Entities.Concrete
{
    public class Category : EntityBase, IEntity
    {
        public string Name { get; set; }
        public string Detail { get; set; }
    }

}
