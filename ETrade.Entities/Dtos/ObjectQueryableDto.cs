using ETrade.Core.Entities.Abstract;
using ETrade.Core.Utilities.Results.ResultStatusEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Entities.Dtos
{
    public class ObjectQueryableDto<TEntity> : IDto
        where TEntity : class, new()
    {
        public IQueryable<TEntity> Entities { get; set; }
        public ResultStatusEnum ResulStatus { get; set; }
    }
}
