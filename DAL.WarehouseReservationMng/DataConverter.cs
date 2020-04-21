using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.WarehouseReservationMng
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "CushionColorMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<WarehouseReservationProductDetail, DTO.WarehouseReservationMng.WarehouseReservationProductDetail>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseReservation, DTO.WarehouseReservationMng.WarehouseReservation>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReservedDate, o => o.ResolveUsing(s => s.ReservedDate.HasValue ? s.ReservedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.Details, o => o.MapFrom(s => s.WarehouseReservationProductDetail))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.WarehouseReservationMng.WarehouseReservationProductDetail, WarehouseReservationProductDetail>()
                                .IgnoreAllNonExisting()
                                .ForMember(d => d.WarehouseReservationProductDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.WarehouseReservationMng.WarehouseReservation, WarehouseReservation>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedBy, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ProcessingStatusID, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.WarehouseReservationID, o => o.Ignore())
                    .ForMember(d => d.WarehouseReservationProductDetail, o => o.Ignore());



                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public DTO.WarehouseReservationMng.WarehouseReservation DB2DTO_WarehouseReservation(WarehouseReservation dbItem)
        {
            return AutoMapper.Mapper.Map<WarehouseReservation, DTO.WarehouseReservationMng.WarehouseReservation>(dbItem);
        }

        public void DTO2DB(DTO.WarehouseReservationMng.WarehouseReservation dtoItem, ref WarehouseReservation dbItem)
        {
            // map main object
            AutoMapper.Mapper.Map<DTO.WarehouseReservationMng.WarehouseReservation, WarehouseReservation>(dtoItem, dbItem);

            // map child collection
            // delete case
            List<int> toBeDeletedDetailIDs = new List<int>();
            foreach (WarehouseReservationProductDetail toBeDeletedDetail in dbItem.WarehouseReservationProductDetail)
            {
                if (!dtoItem.Details.Select(o => o.WarehouseReservationProductDetailID).Contains(toBeDeletedDetail.WarehouseReservationProductDetailID))
                {
                    toBeDeletedDetailIDs.Add(toBeDeletedDetail.WarehouseReservationProductDetailID);
                }
            }
            foreach (int toBeDeletedDetailID in toBeDeletedDetailIDs)
            {
                dbItem.WarehouseReservationProductDetail.Remove(dbItem.WarehouseReservationProductDetail.FirstOrDefault(o => o.WarehouseReservationProductDetailID == toBeDeletedDetailID));
            }

            foreach (DTO.WarehouseReservationMng.WarehouseReservationProductDetail dtoDetail in dtoItem.Details)
            {
                // add new case
                WarehouseReservationProductDetail dbDetail = null;
                if (dtoDetail.WarehouseReservationProductDetailID <= 0)
                {
                    dbDetail = new WarehouseReservationProductDetail();
                    dbDetail.WarehouseReservation = dbItem;
                    dbItem.WarehouseReservationProductDetail.Add(dbDetail);
                }
                else
                {
                    dbDetail = dbItem.WarehouseReservationProductDetail.FirstOrDefault(o => o.WarehouseReservationProductDetailID == dtoDetail.WarehouseReservationProductDetailID);
                }

                if (dbDetail != null)
                {
                    AutoMapper.Mapper.Map<DTO.WarehouseReservationMng.WarehouseReservationProductDetail, WarehouseReservationProductDetail>(dtoDetail, dbDetail);
                }
            }
        }
    }
}
