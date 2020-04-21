using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.APIDTO
{
    public class APINotifyMessage
    {
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public List<string> DetailMessage { get; set; }
    }

    public enum NotificationType
    {
        Success,
        Warning,
        Error
    }
}