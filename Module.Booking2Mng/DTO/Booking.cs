using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Booking2Mng.DTO
{
    public class Booking
    {
        public int BookingID { get; set; }
        public string BookingUD { get; set; }
        public string BookingDate { get; set; }
        public string ConfirmationNo { get; set; }
        public string BLNo { get; set; }
        public int? SupplierID { get; set; }
        public int? ForwarderID { get; set; }
        public string ForwarderInfo { get; set; }
        public string ShipperInfo { get; set; }
        public string ConsigneeInfo { get; set; }
        public string NotifyParty1Info { get; set; }
        public string NotifyParty2Info { get; set; }
        public string Feeder { get; set; }
        public string Vessel { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }
        public int? POLID { get; set; }
        public string PlaceOfReceipt { get; set; }
        public int? PODID { get; set; }
        public string PlaceOfDelivery { get; set; }
        public int? DeliveryTermID { get; set; }
        public int? MovementTermID { get; set; }
        public string OcceanFreight { get; set; }
        public decimal? PKGs { get; set; }
        public decimal? KGs { get; set; }
        public decimal? CBMs { get; set; }
        public string ShippingMark { get; set; }
        public string DescriptionOfGoods { get; set; }
        public string OriginalBL { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedDate { get; set; }
        public int? DeletedBy { get; set; }
        public string ShipedDate { get; set; }
        public string Season { get; set; }
        public string DeleteRemark { get; set; }
        public string CutOffDate { get; set; }
        public string ETA2 { get; set; }
        public int? ETAConfirmedBy { get; set; }
        public int? ETA2ConfirmedBy { get; set; }
        public int? ClientID { get; set; }
        public bool? IsETAConfirmed { get; set; }
        public string ETAConfirmedDate { get; set; }
        public bool? IsETA2Confirmed { get; set; }
        public string ETA2ConfirmedDate { get; set; }
        public string BLFile { get; set; }
        public string ConcurrencyFlag { get; set; }
        public string SupplierNM { get; set; }
        public string ForwarderNM { get; set; }
        public string PoDName { get; set; }
        public string PoLname { get; set; }
        public string ClientUD { get; set; }
        public string DeliveryTermNM { get; set; }
        public string MovementTermNM { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public string ConfirmerName { get; set; }
        public string UpdatorName { get; set; }
        public string ETAConfirmerName { get; set; }
        public string ETA2ConfirmerName { get; set; }
        public string OceanFreightNM { get; set; }

        public List<DTO.LoadingPlan> LoadingPlans { get; set; }

        public bool BLFile_HasChanged { get; set; }
        public string BLFile_NewFile { get; set; }

        public string EUTRFile { get; set; }

        public string EUTRFriendlyName { get; set; }

        public string EUTRFileLocation { get; set; }

        public bool? EUTRFile_HasChange { get; set; }

        public string EUTRFile_NewFile { get; set; }

        public bool StatusAllShipped { get; set; }

        public Nullable<int> LoadinplanCTNs { get; set; }
        public Nullable<decimal> LoadinplanNettWeight { get; set; }
        public Nullable<decimal> LoadinplanGrossWeight { get; set; }
        public Nullable<decimal> LoadinplanCBMs { get; set; }
        public Nullable<bool> IsConfirmAllLoading { get; set; }
        public string ConfirmAllLoadinger { get; set; }
        public string ConfirmAllLoadingDate { get; set; }
        public Nullable<bool> IsReset { get; set; }
        public string Reseter { get; set; }
        public string ResetDate { get; set; }
        public string Confirmeer { get; set; }
        public string Creater { get; set; }
        public Nullable<int> ConfirmAllLoadingBy { get; set; }
        public Nullable<int> ResetBy { get; set; }
        public string Updater { get; set; }
        public string StatusNM { get; set; }

        public string BookingConfirmationFile { get; set; }
        public string BookingConfirmationFriendlyName { get; set; }
        public string BookingConfirmationFileLocation { get; set; }
        public bool? BookingConfirmationFile_HasChange { get; set; }
        public string BookingConfirmationFile_NewFile { get; set; }

        public string COFile { get; set; }
        public string COFriendlyName { get; set; }
        public string COFileLocation { get; set; }
        public bool? COFile_HasChange { get; set; }
        public string COFile_NewFile { get; set; }

        public string FumigationFile { get; set; }
        public string FumigationFriendlyName { get; set; }
        public string FumigationFileLocation { get; set; }
        public bool? FumigationFile_HasChange { get; set; }
        public string FumigationFile_NewFile { get; set; }

        public string OtherFile { get; set; }
        public string OtherFriendlyName { get; set; }
        public string OtherFileLocation { get; set; }
        public bool? OtherFile_HasChange { get; set; }
        public string OtherFile_NewFile { get; set; }

        public string ConfirmationFile { get; set; }
        public string ConfirmationFriendlyName { get; set; }
        public string ConfirmationFileLocation { get; set; }
        public bool? ConfirmationFile_HasChange { get; set; }
        public string ConfirmationFile_NewFile { get; set; }

        public bool? IsBLFileNA { get; set; }
        public bool? IsEUTRFileNA { get; set; }
        public bool? IsCOFileNA { get; set; }
        public bool? IsFumigationFileNA { get; set; }
        public bool? IsOtherFileNA { get; set; }
        public bool? IsConfirmationFileNA { get; set; }
        public int? PurchasingInvoiceID { get; set; }
        public int? PackingListID { get; set; }
        public int CompanyID { get; set; }
        public int? viewTypeID { get; set; }
        public List<DTO.BookingPlanFactoryOrderDetailDTO> bookingPlanFactoryOrderDetailDTOs { get; set; }
        public List<DTO.BookingPlanFactoryOrderSampleDetailDTO> bookingPlanFactoryOrderSampleDetailDTOs { get; set; }
        public List<DTO.BookingPlanFactoryOrderSparepartDetailDTO> bookingPlanFactoryOrderSparepartDetailDTOs { get; set; }
        public List<DTO.BookingPlanFactoryOrderDTO> bookingPlanFactoryOrderDTOs { get; set; }
        public Booking ()
        {
            bookingPlanFactoryOrderDetailDTOs = new List<BookingPlanFactoryOrderDetailDTO>();
            bookingPlanFactoryOrderSampleDetailDTOs = new List<BookingPlanFactoryOrderSampleDetailDTO>();
            bookingPlanFactoryOrderSparepartDetailDTOs = new List<BookingPlanFactoryOrderSparepartDetailDTO>();
            bookingPlanFactoryOrderDTOs = new List<BookingPlanFactoryOrderDTO>();
        }
    }
}
