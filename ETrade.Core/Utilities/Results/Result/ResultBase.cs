using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.Results.Result
{
    public class ResultBase : IResult
    {

        public string Title { get; }

        public string Message { get; }

        public bool Success { get; }

        public ResultBase(bool success, string message, string title) : this(success, message)
        {
            Title = title;
        }
        public ResultBase(bool success, string message) : this(success)
        {
            Message = message;
        }
        public ResultBase(bool success)
        {
            Success = success;
        }
    }
}
