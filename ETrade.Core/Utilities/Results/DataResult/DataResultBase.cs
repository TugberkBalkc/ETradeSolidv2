using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.Results.DataResult
{
    public class DataResultBase<TData> : IDataResult<TData>
    {
        public TData Data { get; }

        public string Title { get; }

        public string Message { get; }

        public bool Success { get; }


        public DataResultBase(bool success, string message, string title, TData data) : this(success, message)
        {
            Title = title;
            Data = data;
        }
        public DataResultBase(bool success, string message, string title) : this(success, message)
        {
            Title = title;
        }
        public DataResultBase(bool success, string message, TData data) : this(success, message)
        {
            Data = data;
        }
        public DataResultBase(bool success, string message) : this(success)
        {
            Message = message;
        }
        public DataResultBase(bool success, TData data) : this(success)
        {
            Data = data;
        }
        public DataResultBase(bool success)
        {
            Success = success;
        }
    }
}
