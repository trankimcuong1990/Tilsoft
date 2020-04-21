using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;

namespace DAL.BookingMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "BookingMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<BookingMng_LoadingPlan_View, DTO.BookingMng.LoadingPlan>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BookingMng_Booking_View, DTO.BookingMng.Booking>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BookingDate, o => o.ResolveUsing(s => s.BookingDate.HasValue ? s.BookingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA2, o => o.ResolveUsing(s => s.ETA2.HasValue ? s.ETA2.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShipedDate, o => o.ResolveUsing(s => s.ShipedDate.HasValue ? s.ShipedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CutOffDate, o => o.ResolveUsing(s => s.CutOffDate.HasValue ? s.CutOffDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETAConfirmedDate, o => o.ResolveUsing(s => s.ETAConfirmedDate.HasValue ? s.ETAConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA2ConfirmedDate, o => o.ResolveUsing(s => s.ETA2ConfirmedDate.HasValue ? s.ETA2ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.BLFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.BLFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.BLFileURL)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.BookingMng.Booking, Booking>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.BookingDate, o => o.Ignore())
                   .ForMember(d => d.ETD, o => o.Ignore())
                   .ForMember(d => d.ETA, o => o.Ignore())
                   .ForMember(d => d.ETA2, o => o.Ignore())
                   .ForMember(d => d.CutOffDate, o => o.Ignore())
                   .ForMember(d => d.ShipedDate, o => o.Ignore())
                   .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                   .ForMember(d => d.ETAConfirmedDate, o => o.Ignore())
                   .ForMember(d => d.ETA2ConfirmedDate, o => o.Ignore())
                   .ForMember(d => d.CreatedDate, o => o.Ignore())
                   .ForMember(d => d.UpdatedDate, o => o.Ignore())

                   .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                   .ForMember(d => d.BookingDetail, o => o.Ignore())
                   .ForMember(d => d.BookingID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<BookingMng_BookingSearchResult_View, DTO.BookingMng.BookingSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BookingDate, o => o.ResolveUsing(s => s.BookingDate.HasValue ? s.BookingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA2, o => o.ResolveUsing(s => s.ETA2.HasValue ? s.ETA2.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShipedDate, o => o.ResolveUsing(s => s.ShipedDate.HasValue ? s.ShipedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.BookingMng.BookingSearchResult> DB2DTO_BookingSearchResultList(List<BookingMng_BookingSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BookingMng_BookingSearchResult_View>, List<DTO.BookingMng.BookingSearchResult>>(dbItems);
        }

        public DTO.BookingMng.Booking DB2DTO_Booking(BookingMng_Booking_View dbItem)
        {
            return AutoMapper.Mapper.Map<BookingMng_Booking_View, DTO.BookingMng.Booking>(dbItem);
        }

        public void DTO2DB(DTO.BookingMng.Booking dtoItem, ref Booking dbItem)
        {
            // map parent
            AutoMapper.Mapper.Map<DTO.BookingMng.Booking, Booking>(dtoItem, dbItem);
            dbItem.BookingDate = dtoItem.BookingDate.ConvertStringToDateTime();
            dbItem.ConfirmedDate = dtoItem.ConfirmedDate.ConvertStringToDateTime();
            dbItem.ETD = dtoItem.ETD.ConvertStringToDateTime();
            if (!dbItem.IsETAConfirmed.HasValue || dbItem.IsETAConfirmed == false) dbItem.ETA = dtoItem.ETA.ConvertStringToDateTime();
            if (!dbItem.IsETA2Confirmed.HasValue || dbItem.IsETA2Confirmed == false) dbItem.ETA2 = dtoItem.ETA2.ConvertStringToDateTime();
            dbItem.ShipedDate = dtoItem.ShipedDate.ConvertStringToDateTime();
            dbItem.CutOffDate = dtoItem.CutOffDate.ConvertStringToDateTime();
            dbItem.CreatedDate = dtoItem.CreatedDate.ConvertStringToDateTime();
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();

            // map child
            //if (dtoItem.Details != null && dtoItem.Details.Count > 0)
            //{
            //    // delete case
            //    foreach (BookingDetail dbDetailToBeDeleted in dbItem.BookingDetail.ToArray())
            //    {
            //        if (!dtoItem.Details.Select(o => o.BookingDetailID).Contains(dbDetailToBeDeleted.BookingDetailID))
            //        {
            //            dbItem.BookingDetail.Remove(dbDetailToBeDeleted);
            //        }
            //    }

            //    // create + update
            //    foreach (DTO.BookingMng.BookingDetail dtoDetail in dtoItem.Details)
            //    {
            //        BookingDetail dbDetail;
            //        if (dtoDetail.BookingDetailID <= 0)
            //        {
            //            dbDetail = new BookingDetail();
            //            dbDetail.Booking = dbItem;
            //            dbItem.BookingDetail.Add(dbDetail);
            //        }
            //        else
            //        {
            //            dbDetail = dbItem.BookingDetail.FirstOrDefault(o => o.BookingDetailID == dtoDetail.BookingDetailID);
            //        }

            //        if (dbDetail != null)
            //        {
            //            AutoMapper.Mapper.Map<DTO.BookingMng.BookingDetail, BookingDetail>(dtoDetail, dbDetail);
            //        }
            //    }
            //}
        }
    }
}
