using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.WebAPI.UserCommunication.Notifications
{
    public interface INotification
    {
        string Title { get; set; }
        string Message { get; set; }
    }
}
