using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.Results.Result
{
    public interface IResult
    {
        string Title { get; }
        string Message { get; }
        bool Success { get; }
    }
}
