using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class Notification
    {
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public List<string> DetailMessage
        {
            get
            {
                if (_DetailMessage == null)
                {
                    _DetailMessage = new List<string>();
                }
                return _DetailMessage;
            }
        }
        private List<string> _DetailMessage;
    }

    public enum NotificationType
    {
        Success,
        Warning,
        Error,
    }
}
