//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.DeliveryNote2.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailNotificationMessage
    {
        public int EmailNotificationMessageID { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string SendTo { get; set; }
        public Nullable<System.DateTime> LastAttempt { get; set; }
        public string ErrorMessage { get; set; }
        public Nullable<bool> IsSent { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}