using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.WarehouseTransferMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "WarehouseTransferMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<WarehouseTransferMng_WarehouseTransferSearch_View, DTO.WarehouseTransferSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.showFactoryhouseNMs, o => o.MapFrom(s => s.WarehouseTransferMng_FactoryWarehouseFromAndToNM_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseTransferMng_WarehouseTransfer_View, DTO.WarehouseTransferDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ReceiptDate, o => o.ResolveUsing(s => s.ReceiptDate.HasValue ? s.ReceiptDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.WarehouseTransferDetails, o => o.MapFrom(s => s.WarehouseTransferMng_WarehouseTransferDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.WarehouseTransferDTO, WarehouseTransfer>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ReceiptDate, o => o.Ignore())
                    .ForMember(d => d.WarehouseTransferID, o => o.Ignore())
                    .ForMember(d => d.WarehouseTransferDetail, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.WarehouseTransferDetailDTO, WarehouseTransferDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehouseTransferDetailID, o => o.Ignore())
                    .ForMember(d => d.WarehouseTransferID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseTransferMng_Branch_View, DTO.BranchDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseTransferMng_FactoryWarehouse_View, DTO.FactoryWarehouseDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseTransferMng_ProductionItem_View, DTO.ProductionItemDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductionItemUnits, o => o.MapFrom(s => s.WarehouseTransferMng_ProductionItemUnit_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseTransferMng_ProductionItemUnit_View, DTO.ProductionItemUnitDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                
                AutoMapper.Mapper.CreateMap<WarehouseTransferMng_WarehouseTransferDetail_View, DTO.WarehouseTransferDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductionItemUnits, o => o.MapFrom(s => s.WarehouseTransferMng_ProductionItemUnit_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseTransferMng_GetStockQnt_View, DTO.StockQntFromWarehouse>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseTransferMng_FactoryWarehouseFromAndToNM_View, DTO.ShowFactoryhouseNM>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.WarehouseTransferSearchDTO> DB2DTO_WarehouseTransferSearch(List<WarehouseTransferMng_WarehouseTransferSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseTransferMng_WarehouseTransferSearch_View>, List<DTO.WarehouseTransferSearchDTO>>(dbItems);
        }

        public List<DTO.ShowFactoryhouseNM> DB2DTO_WarehouseTransferNM(List<WarehouseTransferMng_FactoryWarehouseFromAndToNM_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseTransferMng_FactoryWarehouseFromAndToNM_View>, List<DTO.ShowFactoryhouseNM>>(dbItems);
        }

        public List<DTO.FactoryWarehouseDTO> DB2DTO_FactoryWarehouseDTO(List<WarehouseTransferMng_FactoryWarehouse_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseTransferMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(dbItems);
        }

        public DTO.WarehouseTransferDTO DB2DTO_WarehouseTransfer(WarehouseTransferMng_WarehouseTransfer_View dbItem)
        {
            return AutoMapper.Mapper.Map<WarehouseTransferMng_WarehouseTransfer_View, DTO.WarehouseTransferDTO>(dbItem);
        }

        public void DTO2DB_WarehouseTransfer(int userID, int? companyID, DTO.WarehouseTransferDTO dtoWarehouseTransfer, ref WarehouseTransfer dbWarehouseTransfer)
        {
            AutoMapper.Mapper.Map<DTO.WarehouseTransferDTO, WarehouseTransfer>(dtoWarehouseTransfer, dbWarehouseTransfer);
            dbWarehouseTransfer.CompanyID = companyID;
            dbWarehouseTransfer.UpdatedBy = userID;
            dbWarehouseTransfer.UpdatedDate = DateTime.Now;
            dbWarehouseTransfer.ReceiptDate = dtoWarehouseTransfer.ReceiptDate.ConvertStringToDateTime();

            if (dtoWarehouseTransfer.WarehouseTransferID == 0)
            {
                dbWarehouseTransfer.CreatedBy = userID;
                dbWarehouseTransfer.CreatedDate = DateTime.Now;
            }

            if (dtoWarehouseTransfer.WarehouseTransferDetails != null)
            {
                foreach (WarehouseTransferDetail dbWarehouseTransferDetail in dbWarehouseTransfer.WarehouseTransferDetail.ToList())
                {
                    if (!dtoWarehouseTransfer.WarehouseTransferDetails.Select(s => s.WarehouseTransferDetailID).Contains(dbWarehouseTransferDetail.WarehouseTransferDetailID))
                    {
                        dbWarehouseTransfer.WarehouseTransferDetail.Remove(dbWarehouseTransferDetail);
                    }
                }

                foreach (DTO.WarehouseTransferDetailDTO dtoWarehouseTransferDetail in dtoWarehouseTransfer.WarehouseTransferDetails.ToList())
                {
                    WarehouseTransferDetail dbWarehouseTransferDetail;

                    if (dtoWarehouseTransferDetail.WarehouseTransferDetailID <= 0)
                    {
                        dbWarehouseTransferDetail = new WarehouseTransferDetail();
                        dbWarehouseTransfer.WarehouseTransferDetail.Add(dbWarehouseTransferDetail);
                    }
                    else
                    {
                        dbWarehouseTransferDetail = dbWarehouseTransfer.WarehouseTransferDetail.FirstOrDefault(o => o.WarehouseTransferDetailID == dtoWarehouseTransferDetail.WarehouseTransferDetailID);
                    }

                    if (dbWarehouseTransferDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.WarehouseTransferDetailDTO, WarehouseTransferDetail>(dtoWarehouseTransferDetail, dbWarehouseTransferDetail);
                    }
                }
            }
            ////set base info
            //dbItem.CompanyID = companyID;
            //dbItem.ReceiptDate = dtoItem.ReceiptDate.ConvertStringToDateTime();
            //if (dtoItem.WarehouseTransferID>0)
            //{
            //    dbItem.UpdatedDate = DateTime.Now;
            //    dbItem.UpdatedBy = userId;                
            //}
            //else {
            //    dbItem.CreatedDate = DateTime.Now;
            //    dbItem.CreatedBy = userId;
            //}
            ////create product
            //if (dbItem.WarehouseTransferID > 0)
            //{
            //    //delete delivery note item
            //    var deliveryNote = dbItem.DeliveryNote.FirstOrDefault();
            //    foreach (var item in deliveryNote.DeliveryNoteDetail.ToArray())
            //    {
            //        deliveryNote.DeliveryNoteDetail.Remove(item);
            //    }

            //    //delete receiving note item
            //    var receivingNote = dbItem.ReceivingNote.FirstOrDefault();
            //    foreach (var item in receivingNote.ReceivingNoteDetail.ToArray())
            //    {
            //        receivingNote.ReceivingNoteDetail.Remove(item);
            //    }

            //    //create transfer item
            //    DeliveryNoteDetail dbDeliveryNoteDetail;
            //    ReceivingNoteDetail dbReceivingNoteDetail;
            //    foreach (var item in dtoItem.WarehouseTransferProductDTOs)
            //    {
            //        //add deliverynote detail
            //        dbDeliveryNoteDetail = new DeliveryNoteDetail();
            //        dbDeliveryNoteDetail.ProductionItemID = item.ProductionItemID;
            //        dbDeliveryNoteDetail.Qty = item.Quantity;
            //        dbDeliveryNoteDetail.QNTBarCode = item.QNTBarCode;
            //        dbDeliveryNoteDetail.FromFactoryWarehouseID = dtoItem.FromFactoryWarehouseID;
            //        deliveryNote.DeliveryNoteDetail.Add(dbDeliveryNoteDetail);

            //        //add receivingNote detail
            //        dbReceivingNoteDetail = new ReceivingNoteDetail();
            //        dbReceivingNoteDetail.ProductionItemID = item.ProductionItemID;
            //        dbReceivingNoteDetail.Quantity = item.Quantity;
            //        dbReceivingNoteDetail.QNTBarCode = item.QNTBarCode;
            //        dbReceivingNoteDetail.ToFactoryWarehouseID = dtoItem.ToFactoryWarehouseID;
            //        receivingNote.ReceivingNoteDetail.Add(dbReceivingNoteDetail);
            //    }
            //}
            //else {
            //    //create delivery note
            //    DeliveryNote deliveryNote = new DeliveryNote();
            //    deliveryNote.DeliveryNoteTypeID = 3;
            //    deliveryNote.DeliveryNoteDate = DateTime.Now;
            //    deliveryNote.CompanyID = companyID;
            //    deliveryNote.UpdatedBy = userId;
            //    deliveryNote.UpdatedDate = DateTime.Now;
            //    dbItem.DeliveryNote.Add(deliveryNote);

            //    //create receiving note
            //    ReceivingNote receivingNote = new ReceivingNote();
            //    receivingNote.ReceivingNoteTypeID = 3;
            //    receivingNote.ReceivingNoteDate = DateTime.Now;
            //    receivingNote.CompanyID = companyID;
            //    receivingNote.UpdatedBy = userId;
            //    receivingNote.UpdatedDate = DateTime.Now;
            //    dbItem.ReceivingNote.Add(receivingNote);

            //    //create transfer item
            //    DeliveryNoteDetail dbDeliveryNoteDetail;
            //    ReceivingNoteDetail dbReceivingNoteDetail;
            //    foreach (var item in dtoItem.WarehouseTransferProductDTOs)
            //    {
            //        //add deliverynote detail
            //        dbDeliveryNoteDetail = new DeliveryNoteDetail();
            //        dbDeliveryNoteDetail.ProductionItemID = item.ProductionItemID;
            //        dbDeliveryNoteDetail.Qty = item.Quantity;
            //        dbDeliveryNoteDetail.QNTBarCode = item.QNTBarCode;
            //        dbDeliveryNoteDetail.FromFactoryWarehouseID = dtoItem.FromFactoryWarehouseID;
            //        deliveryNote.DeliveryNoteDetail.Add(dbDeliveryNoteDetail);

            //        //add receivingNote detail
            //        dbReceivingNoteDetail = new ReceivingNoteDetail();
            //        dbReceivingNoteDetail.ProductionItemID = item.ProductionItemID;
            //        dbReceivingNoteDetail.Quantity = item.Quantity;
            //        dbReceivingNoteDetail.QNTBarCode = item.QNTBarCode;
            //        dbReceivingNoteDetail.ToFactoryWarehouseID = dtoItem.ToFactoryWarehouseID;
            //        receivingNote.ReceivingNoteDetail.Add(dbReceivingNoteDetail);
            //    }
            //}
        }

        public List<DTO.ProductionItemDTO> DB2DTO_ProductionItem(List<WarehouseTransferMng_ProductionItem_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<WarehouseTransferMng_ProductionItem_View>, List<DTO.ProductionItemDTO>>(dbItem);
        }

        public List<DTO.ProductionItemUnitDTO> DB2DTO_ProductionItemUnit(List<WarehouseTransferMng_ProductionItemUnit_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<WarehouseTransferMng_ProductionItemUnit_View>, List<DTO.ProductionItemUnitDTO>>(dbItem);
        }

        public List<DTO.StockQntFromWarehouse> DB2DTO_StockQntFromWarehouse(List<WarehouseTransferMng_GetStockQnt_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<WarehouseTransferMng_GetStockQnt_View>, List<DTO.StockQntFromWarehouse>>(dbItem);
        }
    }
}
