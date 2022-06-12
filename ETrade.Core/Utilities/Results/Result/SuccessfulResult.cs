using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.Results.Result
{
    public class SuccessfulResult : ResultBase
    {
        public SuccessfulResult(string message, string title) : base(true,message,title)
        {

        }
        public SuccessfulResult(string message) : base(true, message)
        {

        }
        public SuccessfulResult() :base(true)
        {

        }
    }
}
