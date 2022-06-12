using ETrade.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Entities.Dtos
{
    public class ProductDetailsDto : IDto
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductDetail { get; set; }
        public string ProductStockCode { get; set; }
        public short ProductStockAmount { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public DateTime ProductCreatedDate { get; set; }
        public DateTime ProductUpdatedDate { get; set; }
        public bool ProductIsActive { get; set; }
    }
}
