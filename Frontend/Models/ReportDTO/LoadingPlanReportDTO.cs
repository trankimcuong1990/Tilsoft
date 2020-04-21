using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Models.ReportDTO
{
    public class LoadingPlanReportDTO
    {
        public int? LoadingPlanID { get; set; }
        public string LoadingPlanUD { get; set; }
        public string FactoryUD { get; set; }
        public string ContainerRefNo { get; set; }
        public string ContainerNo { get; set; }
        public string ReportDate { get; set; }
        public string LoadingDate { get; set; }
        public string SealNo { get; set; }
        public string ControllerName { get; set; }
        public string SignatureImage { get; set; }
        public string POLName { get; set; }
        public string PODName { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }
        public int? Cont20DC { get; set; }
        public int? Cont40DC { get; set; }
        public int? Cont40HC { get; set; }
        public string ProductPicture1 { get; set; }
        public string ProductPicture2 { get; set; }
        public string ContainerPicture1 { get; set; }
        public string ContainerPicture2 { get; set; }
        public string ContainerPicture3 { get; set; }
        public string ContainerPicture4 { get; set; }
        public string ContainerPicture5 { get; set; }
        public string ContainerPicture6 { get; set; }
        public string MerchandisesInside1Per3ContImage { get; set; }
        public string MerchandisesInside1Per2ContImage { get; set; }
        public string MerchandisesInsideFullContImage { get; set; }
        public string ContainerDoorAndLockImage { get; set; }
        public string ContainerSealImage { get; set; }
        public string DryPackImage { get; set; }
        public string ApprovedImage { get; set; }
        public bool? IsConfirmed { get; set; }
        public string ReportLogo { get; set; }
        
        public List<Frontend.Models.ReportDTO.LoadingPlanDetailReportDTO> loadingPlanDetailReportDTOs { get; set; }
    }
}