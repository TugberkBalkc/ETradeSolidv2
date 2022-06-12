using ETrade.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Entities.Concrete
{
    public class OperationClaim : EntityBase, IEntity
    {
        public string Name { get; set; }
    }
}
