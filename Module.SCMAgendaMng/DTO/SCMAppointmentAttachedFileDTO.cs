using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SCMAgendaMng.DTO
{
    public class SCMAppointmentAttachedFileDTO
    {
        public int SCMAppointmentAttachedFileID { get; set; }
        public string FileUD { get; set; }
        public int? SCMAppointmentAttachedFileTypeID { get; set; }
        public string Description { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string SCMAppointmentAttachedFileTypeNM { get; set; }

        // Updated By, UpdatedDate
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public bool? HasLink { get; set; }

        public string NewFile { get; set; }
        public bool HasChanged { get; set; }
    }
}
