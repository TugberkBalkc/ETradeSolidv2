using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.Results.DataResult
{
    public class UnSuccessfulDataResult<TData> : DataResultBase<TData> 
    {
        public UnSuccessfulDataResult(TData data, string message, string title) : base(false, message, title, data)
        {

        }
        public UnSuccessfulDataResult(string message, string title) : base(false, message, title)
        {

        }
        public UnSuccessfulDataResult(TData data, string message) : base(false, message, data)
        {

        }
        public UnSuccessfulDataResult(string message) : base(false, message)
        {

        }
        public UnSuccessfulDataResult(TData data) : base(false, data)
        {

        }
        public UnSuccessfulDataResult() : base(false)
        {

        }
    }
}
