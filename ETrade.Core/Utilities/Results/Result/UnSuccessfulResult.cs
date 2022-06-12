using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.Results.Result
{
    public class UnSuccessfulResult : ResultBase
    {
        public UnSuccessfulResult(string message, string title) : base(false, message, title)
        {

        }
        public UnSuccessfulResult(string message) : base(false, message)
        {

        }
        public UnSuccessfulResult() : base(false)
        {

        }
    }
}
