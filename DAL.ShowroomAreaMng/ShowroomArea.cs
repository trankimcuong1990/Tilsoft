//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ShowroomAreaMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShowroomArea
    {
        public ShowroomArea()
        {
            this.ShowroomAreaImage = new HashSet<ShowroomAreaImage>();
        }
    
        public int ShowroomAreaID { get; set; }
        public string ShowroomAreaUD { get; set; }
        public string ShowroomAreaNM { get; set; }
    
        public virtual ICollection<ShowroomAreaImage> ShowroomAreaImage { get; set; }
    }
}
