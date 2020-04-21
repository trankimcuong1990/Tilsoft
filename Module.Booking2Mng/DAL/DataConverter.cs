using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.Booking2Mng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = "Booking2Mng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<Booking2Mng_BookingSearchResult_View, DTO.BookingSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BookingDate, o => o.ResolveUsing(s => s.BookingDate.HasValue ? s.BookingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShipedDate, o => o.ResolveUsing(s => s.ShipedDate.HasValue ? s.ShipedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA2, o => o.ResolveUsing(s => s.ETA2.HasValue ? s.ETA2.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Booking2Mng_Booking_View, DTO.Booking>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BookingDate, o => o.ResolveUsing(s => s.BookingDate.HasValue ? s.BookingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShipedDate, o => o.ResolveUsing(s => s.ShipedDate.HasValue ? s.ShipedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA2, o => o.ResolveUsing(s => s.ETA2.HasValue ? s.ETA2.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmAllLoadingDate, o => o.ResolveUsing(s => s.ConfirmAllLoadingDate.HasValue ? s.ConfirmAllLoadingDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ResetDate, o => o.ResolveUsing(s => s.ResetDate.HasValue ? s.ResetDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.EUTRFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.EUTRFileLocation) ? s.EUTRFileLocation : "")))
                    .ForMember(d => d.COFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.COFileLocation) ? s.COFileLocation : "")))
                    .ForMember(d => d.FumigationFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FumigationFileLocation) ? s.FumigationFileLocation : "")))
                    .ForMember(d => d.OtherFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.OtherFileLocation) ? s.OtherFileLocation : "")))
                    .ForMember(d => d.ConfirmationFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ConfirmationFileLocation) ? s.ConfirmationFileLocation : "")))
                    .ForMember(d => d.ConcurrencyFlag, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.LoadingPlans, o => o.MapFrom(d => d.Booking2Mng_LoadingPlan_View))
                    .ForMember(d => d.bookingPlanFactoryOrderDetailDTOs, o => o.MapFrom(d => d.Booking2Mng_BookingPlanFactoryOrderDetail_View))
                    .ForMember(d => d.bookingPlanFactoryOrderSampleDetailDTOs, o => o.MapFrom(d => d.Booking2Mng_BookingPlanFactoryOrderSampleDetail_View))
                    .ForMember(d => d.bookingPlanFactoryOrderSparepartDetailDTOs, o => o.MapFrom(d => d.Booking2Mng_BookingPlanFactoryOrderSparepartDetail_View))
                    .ForMember(d => d.bookingPlanFactoryOrderDTOs, o => o.MapFrom(d => d.Booking2Mng_BookingPlanningFactoryOrder_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Booking2Mng_LoadingPlan_View, DTO.LoadingPlan>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Booking2Mng_BookingPlanFactoryOrderDetail_View, DTO.BookingPlanFactoryOrderDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Booking2Mng_BookingPlanFactoryOrderSampleDetail_View, DTO.BookingPlanFactoryOrderSampleDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Booking2Mng_BookingPlanFactoryOrderSparepartDetail_View, DTO.BookingPlanFactoryOrderSparepartDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Booking2Mng_BookingPlanningFactoryOrder_View, DTO.BookingPlanFactoryOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.Booking, Booking>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BookingID, o => o.Ignore())
                    .ForMember(d => d.BookingUD, o => o.Ignore())
                    .ForMember(d => d.BookingDate, o => o.Ignore())
                    .ForMember(d => d.ETD, o => o.Ignore())
                    .ForMember(d => d.ETA, o => o.Ignore())
                    .ForMember(d => d.ETA2, o => o.Ignore())
                    .ForMember(d => d.IsConfirmed, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ShipedDate, o => o.Ignore())
                    .ForMember(d => d.CutOffDate, o => o.Ignore())
                    .ForMember(d => d.IsETAConfirmed, o => o.Ignore())
                    .ForMember(d => d.ETAConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.ETAConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.IsETA2Confirmed, o => o.Ignore())
                    .ForMember(d => d.ETA2ConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.ETA2ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.BLFile, o => o.Ignore())
                    .ForMember(d => d.EUTRFile, o => o.Ignore())
                    .ForMember(d => d.COFile, o => o.Ignore())
                    .ForMember(d => d.FumigationFile, o => o.Ignore())
                    .ForMember(d => d.OtherFile, o => o.Ignore())
                    .ForMember(d => d.ConfirmationFile, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.ConfirmAllLoadingDate, o => o.Ignore())
                    .ForMember(d => d.ResetDate, o => o.Ignore())
                     .ForMember(d => d.BookingPlanFactoryOrderDetail, o => o.Ignore())
                      .ForMember(d => d.BookingPlanFactoryOrderSampleDetail, o => o.Ignore())
                       .ForMember(d => d.BookingPlanFactoryOrderSparepartDetail, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.BookingPlanFactoryOrderDetailDTO, BookingPlanFactoryOrderDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.BookingPlanFactoryOrderDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BookingPlanFactoryOrderSampleDetailDTO, BookingPlanFactoryOrderSampleDetail>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.BookingPlanFactoryOrderSampleDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BookingPlanFactoryOrderSparepartDetailDTO, BookingPlanFactoryOrderSparepartDetail>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.BookingPlanFactoryOrderSparepartDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BookingPlanFactoryOrderDetailDTO, BookingPlanFactoryOrderDetail>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.BookingPlanFactoryOrderDetailID, o => o.Ignore());


                AutoMapper.Mapper.CreateMap<Booking2Mng_ShippingInstruction_View, DTO.ShippingInstruction>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Booking2Mng_PersonInCharge_View, DTO.PersonInChargeDTO>()
                       .IgnoreAllNonExisting()
                       .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Booking2Mng_PersonInChargeFactory_View, DTO.PersonInChargeFactoryDTO>()
                      .IgnoreAllNonExisting()
                      .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //List FactoryOrder
                AutoMapper.Mapper.CreateMap<Booking2Mng_function_ListFactoryOrder_Result, DTO.FactoryOrderDTO>()
                      .IgnoreAllNonExisting()
                      .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                      .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Booking2Mng_function_ListFactoryOrderDetail_Result, DTO.FactoryOrderDetailDTO>()
                      .IgnoreAllNonExisting()
                      .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                      .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Booking2Mng_function_ListFactoryOrderSampleDetail_Result, DTO.FactoryOrderSampleDetailDTO>()
                      .IgnoreAllNonExisting()
                      .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                      .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Booking2Mng_function_ListFactoryOrderSparepartDetail_Result, DTO.FactoryOrderSparepartDetailDTO>()
                      .IgnoreAllNonExisting()
                      .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                      .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.BookingSearchResult> DB2DTO_BookingSearchResultList(List<Booking2Mng_BookingSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Booking2Mng_BookingSearchResult_View>, List<DTO.BookingSearchResult>>(dbItems);
        }

        public DTO.Booking DB2DTO_Booking(Booking2Mng_Booking_View dbItem)
        {
            return AutoMapper.Mapper.Map<Booking2Mng_Booking_View, DTO.Booking>(dbItem);
        }

        public void DTO2DB(DTO.Booking dtoItem, ref Booking dbItem, string _tempFolder)
        {
            if ((!dbItem.IsETAConfirmed.HasValue || !dbItem.IsETAConfirmed.Value) && !string.IsNullOrEmpty(dtoItem.ETA))
            {
                if (DateTime.TryParse(dtoItem.ETA, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.ETA = tmpDate;
                }
            }
            if ((!dbItem.IsETA2Confirmed.HasValue || !dbItem.IsETA2Confirmed.Value) && !string.IsNullOrEmpty(dtoItem.ETA2))
            {
                if (DateTime.TryParse(dtoItem.ETA2, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.ETA2 = tmpDate;
                }
            }
            if (!string.IsNullOrEmpty(dtoItem.BookingDate))
            {
                if (DateTime.TryParse(dtoItem.BookingDate, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.BookingDate = tmpDate;
                }
            }
            if (!string.IsNullOrEmpty(dtoItem.ShipedDate))
            {
                if (DateTime.TryParse(dtoItem.ShipedDate, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.ShipedDate = tmpDate;
                }
            }
            if (!string.IsNullOrEmpty(dtoItem.ETD))
            {
                if (DateTime.TryParse(dtoItem.ETD, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.ETD = tmpDate;
                }
            }
            if (!string.IsNullOrEmpty(dtoItem.CutOffDate))
            {
                if (DateTime.TryParse(dtoItem.CutOffDate, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.CutOffDate = tmpDate;
                }
            }

            if (!string.IsNullOrEmpty(dtoItem.ConfirmAllLoadingDate))
            {
                if (DateTime.TryParse(dtoItem.ConfirmAllLoadingDate, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.ConfirmAllLoadingDate = tmpDate;
                }
            }

            if (!string.IsNullOrEmpty(dtoItem.ResetDate))
            {
                if (DateTime.TryParse(dtoItem.ResetDate, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.ResetDate = tmpDate;
                }
            }

            
            if (dtoItem.bookingPlanFactoryOrderDetailDTOs != null)
            {
                //delete item in db that exist in dto but not exist in db
                foreach (var item in dbItem.BookingPlanFactoryOrderDetail.ToArray())
                {
                    if (!dtoItem.bookingPlanFactoryOrderDetailDTOs.Select(s => s.BookingPlanFactoryOrderDetailID).Contains(item.BookingPlanFactoryOrderDetailID))
                    {
                        //delete purchase order detail
                        dbItem.BookingPlanFactoryOrderDetail.Remove(item);
                    }
                }
                //read from dto to db
                BookingPlanFactoryOrderDetail dbFactoryOrderDetail;
                foreach (var item in dtoItem.bookingPlanFactoryOrderDetailDTOs)
                {
                    if (item.BookingPlanFactoryOrderDetailID > 0)
                    {
                        dbFactoryOrderDetail = dbItem.BookingPlanFactoryOrderDetail.Where(o => o.BookingPlanFactoryOrderDetailID == item.BookingPlanFactoryOrderDetailID).FirstOrDefault();
                    }
                    else
                    {
                        dbFactoryOrderDetail = new BookingPlanFactoryOrderDetail();
                        dbItem.BookingPlanFactoryOrderDetail.Add(dbFactoryOrderDetail);
                    }
                    //read purchase request detail dto to db
                    if (dbFactoryOrderDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.BookingPlanFactoryOrderDetailDTO, BookingPlanFactoryOrderDetail>(item, dbFactoryOrderDetail);
                        dbFactoryOrderDetail.BookingPlanFactoryOrderDetailID = item.BookingPlanFactoryOrderDetailID;
                    }
                }
            }
            if (dtoItem.bookingPlanFactoryOrderSampleDetailDTOs != null)
            {
                //delete item in db that exist in dto but not exist in db
                foreach (var item in dbItem.BookingPlanFactoryOrderSampleDetail.ToArray())
                {
                    if (!dtoItem.bookingPlanFactoryOrderSampleDetailDTOs.Select(s => s.BookingPlanFactoryOrderSampleDetailID).Contains(item.BookingPlanFactoryOrderSampleDetailID))
                    {
                        //delete purchase order detail
                        dbItem.BookingPlanFactoryOrderSampleDetail.Remove(item);
                    }
                }
                //read from dto to db
                BookingPlanFactoryOrderSampleDetail dbFactoryOrderSampleDetail;
                foreach (var item in dtoItem.bookingPlanFactoryOrderSampleDetailDTOs)
                {
                    if (item.BookingPlanFactoryOrderSampleDetailID > 0)
                    {
                        dbFactoryOrderSampleDetail = dbItem.BookingPlanFactoryOrderSampleDetail.Where(o => o.BookingPlanFactoryOrderSampleDetailID == item.BookingPlanFactoryOrderSampleDetailID).FirstOrDefault();
                    }
                    else
                    {
                        dbFactoryOrderSampleDetail = new BookingPlanFactoryOrderSampleDetail();
                        dbItem.BookingPlanFactoryOrderSampleDetail.Add(dbFactoryOrderSampleDetail);
                    }
                    //read purchase request detail dto to db
                    if (dbFactoryOrderSampleDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.BookingPlanFactoryOrderSampleDetailDTO, BookingPlanFactoryOrderSampleDetail>(item, dbFactoryOrderSampleDetail);
                        dbFactoryOrderSampleDetail.BookingPlanFactoryOrderSampleDetailID = item.BookingPlanFactoryOrderSampleDetailID;
                    }
                }
            }
            if (dtoItem.bookingPlanFactoryOrderSparepartDetailDTOs != null)
            {
                //delete item in db that exist in dto but not exist in db
                foreach (var item in dbItem.BookingPlanFactoryOrderSparepartDetail.ToArray())
                {
                    if (!dtoItem.bookingPlanFactoryOrderSparepartDetailDTOs.Select(s => s.BookingPlanFactoryOrderSparepartDetailID).Contains(item.BookingPlanFactoryOrderSparepartDetailID))
                    {
                        //delete purchase order detail
                        dbItem.BookingPlanFactoryOrderSparepartDetail.Remove(item);
                    }
                }
                //read from dto to db
                BookingPlanFactoryOrderSparepartDetail dbBookingPlanFactoryOrderSparepartDetail;
                foreach (var item in dtoItem.bookingPlanFactoryOrderSparepartDetailDTOs)
                {
                    if (item.BookingPlanFactoryOrderSparepartDetailID > 0)
                    {
                        dbBookingPlanFactoryOrderSparepartDetail = dbItem.BookingPlanFactoryOrderSparepartDetail.Where(o => o.BookingPlanFactoryOrderSparepartDetailID == item.BookingPlanFactoryOrderSparepartDetailID).FirstOrDefault();
                    }
                    else
                    {
                        dbBookingPlanFactoryOrderSparepartDetail = new BookingPlanFactoryOrderSparepartDetail();
                        dbItem.BookingPlanFactoryOrderSparepartDetail.Add(dbBookingPlanFactoryOrderSparepartDetail);
                    }
                    //read purchase request detail dto to db
                    if (dbBookingPlanFactoryOrderSparepartDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.BookingPlanFactoryOrderSparepartDetailDTO, BookingPlanFactoryOrderSparepartDetail>(item, dbBookingPlanFactoryOrderSparepartDetail);
                        dbBookingPlanFactoryOrderSparepartDetail.BookingPlanFactoryOrderSparepartDetailID = item.BookingPlanFactoryOrderSparepartDetailID;
                    }
                }
            }
            // map fields
            AutoMapper.Mapper.Map<DTO.Booking, Booking>(dtoItem, dbItem);

            if (dtoItem.BLFile_HasChanged)
            {
                dbItem.BLFile = (new Module.Framework.DAL.DataFactory()).CreateFilePointer(_tempFolder, dtoItem.BLFile_NewFile, dtoItem.BLFile, dtoItem.FriendlyName);
            }

            // EUTR File
            if (dtoItem.EUTRFile_HasChange.HasValue && dtoItem.EUTRFile_HasChange.Value)
            {
                dbItem.EUTRFile = (new Module.Framework.DAL.DataFactory()).CreateFilePointer(_tempFolder, dtoItem.EUTRFile_NewFile, dtoItem.EUTRFile, dtoItem.EUTRFriendlyName);
            }

            // CO File
            if (dtoItem.COFile_HasChange.HasValue && dtoItem.COFile_HasChange.Value)
            {
                dbItem.COFile = (new Module.Framework.DAL.DataFactory()).CreateFilePointer(_tempFolder, dtoItem.COFile_NewFile, dtoItem.COFile, dtoItem.COFriendlyName);
            }

            // Fumigation File
            if (dtoItem.FumigationFile_HasChange.HasValue && dtoItem.FumigationFile_HasChange.Value)
            {
                dbItem.FumigationFile = (new Module.Framework.DAL.DataFactory()).CreateFilePointer(_tempFolder, dtoItem.FumigationFile_NewFile, dtoItem.FumigationFile, dtoItem.FumigationFriendlyName);
            }

            // Other File
            if (dtoItem.OtherFile_HasChange.HasValue && dtoItem.OtherFile_HasChange.Value)
            {
                dbItem.OtherFile = (new Module.Framework.DAL.DataFactory()).CreateFilePointer(_tempFolder, dtoItem.OtherFile_NewFile, dtoItem.OtherFile, dtoItem.OtherFriendlyName);
            }

            // Confirm File
            if (dtoItem.ConfirmationFile_HasChange.HasValue && dtoItem.ConfirmationFile_HasChange.Value)
            {
                dbItem.ConfirmationFile = (new Module.Framework.DAL.DataFactory()).CreateFilePointer(_tempFolder, dtoItem.ConfirmationFile_NewFile, dtoItem.ConfirmationFile, dtoItem.ConfirmationFriendlyName);
            }

            // Booking Confirmation File
            if (dtoItem.BookingConfirmationFile_HasChange.HasValue && dtoItem.BookingConfirmationFile_HasChange.Value)
            {
                dbItem.BookingConfirmationFile = (new Module.Framework.DAL.DataFactory()).CreateFilePointer(_tempFolder, dtoItem.BookingConfirmationFile_NewFile, dtoItem.BookingConfirmationFile, dtoItem.BookingConfirmationFriendlyName);
            }
        }

        public List<DTO.ShippingInstruction> DB2DTO_ShippingInstructions(List<Booking2Mng_ShippingInstruction_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Booking2Mng_ShippingInstruction_View>, List<DTO.ShippingInstruction>>(dbItems);
        }

        public List<DTO.PersonInChargeDTO> DB2DTO_PersonInCharge(List<Booking2Mng_PersonInCharge_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Booking2Mng_PersonInCharge_View>, List<DTO.PersonInChargeDTO>>(dbItems);
        }

        public List<DTO.PersonInChargeFactoryDTO> DB2DTO_PersonInChargeCreate(List<Booking2Mng_PersonInChargeFactory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Booking2Mng_PersonInChargeFactory_View>, List<DTO.PersonInChargeFactoryDTO>>(dbItems);
        }

        //List FactoryOrder
        public List<DTO.FactoryOrderDTO> DB2DTO_FactoryOrder(List<Booking2Mng_function_ListFactoryOrder_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Booking2Mng_function_ListFactoryOrder_Result>, List<DTO.FactoryOrderDTO>>(dbItems);
        }
        public List<DTO.FactoryOrderDetailDTO> DB2DTO_FactoryOrderDetail(List<Booking2Mng_function_ListFactoryOrderDetail_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Booking2Mng_function_ListFactoryOrderDetail_Result>, List<DTO.FactoryOrderDetailDTO>>(dbItems);
        }
        public List<DTO.FactoryOrderSampleDetailDTO> DB2DTO_FactoryOrderSampleDetail(List<Booking2Mng_function_ListFactoryOrderSampleDetail_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Booking2Mng_function_ListFactoryOrderSampleDetail_Result>, List<DTO.FactoryOrderSampleDetailDTO>>(dbItems);
        }
        public List<DTO.FactoryOrderSparepartDetailDTO> DB2DTO_FactoryOrderSparepartDetail(List<Booking2Mng_function_ListFactoryOrderSparepartDetail_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Booking2Mng_function_ListFactoryOrderSparepartDetail_Result>, List<DTO.FactoryOrderSparepartDetailDTO>>(dbItems);
        }

    }
}
