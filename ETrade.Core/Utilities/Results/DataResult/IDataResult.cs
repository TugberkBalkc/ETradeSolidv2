using ETrade.Core.Utilities.Results.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.Results.DataResult
{
    public interface IDataResult<TData> : IResult
    {
        TData Data { get; }
    }
}
