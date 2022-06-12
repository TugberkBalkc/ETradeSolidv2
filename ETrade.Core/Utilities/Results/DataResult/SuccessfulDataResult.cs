using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.Results.DataResult
{
    public class SuccessfulDataResult<TData> : DataResultBase<TData>
    {
        public SuccessfulDataResult(TData data, string message, string title) : base(true, message, title, data)
        {

        }
        public SuccessfulDataResult(TData data, string message) : base(true,message,data)
        {

        }
        public SuccessfulDataResult(TData data) : base(true, data)
        {

        }
        public SuccessfulDataResult() : base(true)
        {

        }
    }
}
