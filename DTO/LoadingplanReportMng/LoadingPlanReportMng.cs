using System;

namespace DTO.LoadingplanReportMng
{
    // ReSharper disable once InconsistentNaming
    public class LoadingPlanReportMngDTO
    {
        public int LoadingPlanId { get; set; }
        public bool IsSelected { get; set; }
        public bool IsLoaded { get; set; }
        public bool IsShipped { get; set; }
        public bool IsConfirmed { get; set; }
        public string Season { get; set; }
        public string LoadingPlanUd { get; set; }
        public string ContainerRefNo { get; set; }
        public string FactoryUd { get; set; }
        public string Description { get; set; }
        public byte[] ProductImage1 { get; set; }
        public byte[] ProductImage2 { get; set; }
        public byte[] ContainerImage1 { get; set; }
        public byte[] ContainerImage2 { get; set; }
        public string Client { get; set; }
        public string SendingDate { get; set; }
        public string SenderName { get; set; }
        public string ControllerName { get; set; }
        public string LoadingDate { get; set; }
        public string ShippedDate { get; set; }
        public string Forwarder { get; set; }
        public string BlNo { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerType { get; set; }
        public string SealNo { get; set; }
        public string Feeder { get; set; }
        public string Vessel { get; set; }
        public string Etd { get; set; }
        public string Eta { get; set; }
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int FactoryId { get; set; }
        public string Sale { get; set; }
        public string Sale2 { get; set; }
        public string ShipmentDate { get; set; }
        public string CutOffDate { get; set; }
        public string ProductPicture1_Url { get; set; }
        public string ProductPicture2_Url { get; set; }
        public string ContainPicture1_Url { get; set; }
        public string ContainPicture2_Url { get; set; }

 
    }
}
