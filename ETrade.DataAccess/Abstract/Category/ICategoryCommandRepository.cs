using ETrade.Core.DataAccess.Abstract.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Abstract.Category
{
    public interface ICategoryCommandRepository : ICommandRepository<Entities.Concrete.Category>
    {
    }
}
