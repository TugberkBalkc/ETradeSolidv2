using ETrade.Core.Entities.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Entities.Concrete
{
    public class Product : EntityBase, IEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string StockCode
        {
            get
            {
                return this.CategoryId.ToString() + this.Name.Substring(0, 3) + this.Name.Length;
            }
            set { }
        } 

        public short StockAmount { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
