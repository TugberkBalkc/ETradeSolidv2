﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.WebAPI.UserCommunication.Notifications
{
    public class BadRequestNotification : INotification
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
