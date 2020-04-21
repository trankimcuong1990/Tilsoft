using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceiptProductionRpt.DAL
{
    internal class DataConverter
    {
        // Constructor.
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            // Check mapName exist in Maps.
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }

            // Declare method mapping data.
            AutoMapper.Mapper.CreateMap<ReceiptProductionRpt_ReceiptProduction_View, DTO.ReceiptProduction>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ReceiptProductionDetails, o => o.MapFrom(s => s.ReceiptProductionRpt_ReceiptProductionDetail_View));

            AutoMapper.Mapper.CreateMap<ReceiptProductionRpt_ReceiptProductionDetail_View, DTO.ReceiptProductionDetail>()
                .IgnoreAllNonExisting();

            AutoMapper.Mapper.CreateMap<ReceiptProductionRpt_ReceivingNoteQuantity_View, DTO.ReceiptProductionQuantity>()
                .IgnoreAllNonExisting();

            AutoMapper.Mapper.CreateMap<SupportMng_WorkOrder_View, DTO.WorkOrder>()
                .IgnoreAllNonExisting();

            AutoMapper.Mapper.CreateMap<DTO.ReceivingNote, ReceivingNote>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ReceivingNoteID, o => o.Ignore())
                .ForMember(d => d.ReceivingNoteUD, o => o.Ignore())
                .ForMember(d => d.ReceivingNoteDetail, o => o.Ignore());

            AutoMapper.Mapper.CreateMap<DTO.ReceivingNoteDetail, ReceivingNoteDetail>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ReceivingNoteDetailID, o => o.Ignore());

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        // Define method mapping data.
        // 1.
        public List<DTO.ReceiptProduction> DB2DTO_ReceiptProductions(List<ReceiptProductionRpt_ReceiptProduction_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceiptProductionRpt_ReceiptProduction_View>, List<DTO.ReceiptProduction>>(dbItems);
        }

        // 2.
        public List<DTO.WorkOrder> DB2DTO_WorkOrder(List<SupportMng_WorkOrder_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_WorkOrder_View>, List<DTO.WorkOrder>>(dbItem);
        }

        // 3.
        public void DTO2DB_UpdateReceivingNote(DTO.ReceivingNote dtoItem, ref ReceivingNote dbItem)
        {
            if (dtoItem.ReceivingNoteDetails != null)
            {
                foreach (var item in dbItem.ReceivingNoteDetail)
                {
                    if (!dtoItem.ReceivingNoteDetails.Select(s => s.ReceivingNoteDetailID).Contains(item.ReceivingNoteDetailID))
                    {
                        dbItem.ReceivingNoteDetail.Remove(item);
                    }
                }

                foreach (var item in dtoItem.ReceivingNoteDetails)
                {
                    ReceivingNoteDetail receivingNoteDetail;

                    if (item.ReceivingNoteDetailID < 0)
                    {
                        receivingNoteDetail = new ReceivingNoteDetail();
                        dbItem.ReceivingNoteDetail.Add(receivingNoteDetail);
                    }
                    else
                    {
                        receivingNoteDetail = dbItem.ReceivingNoteDetail.FirstOrDefault(s => s.ReceivingNoteDetailID == item.ReceivingNoteDetailID);
                    }

                    if (receivingNoteDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ReceivingNoteDetail, ReceivingNoteDetail>(item, receivingNoteDetail);
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.ReceivingNote, ReceivingNote>(dtoItem, dbItem);
        }

        // 4.
        public List<DTO.ReceiptProductionQuantity> DB2DTO_ReceiptProductionQuantities(List<ReceiptProductionRpt_ReceivingNoteQuantity_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceiptProductionRpt_ReceivingNoteQuantity_View>, List<DTO.ReceiptProductionQuantity>>(dbItems);
        }
    }
}
