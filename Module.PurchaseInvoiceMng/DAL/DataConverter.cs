using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DB 2 DTO
                //
                AutoMapper.Mapper.CreateMap<PurchaseInvoiceMng_PurchaseInvoice_SearchView, DTO.PurchaseInvoiceSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseInvoiceDate, o => o.ResolveUsing(s => s.PurchaseInvoiceDate.HasValue ? s.PurchaseInvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PostingDate, o => o.ResolveUsing(s => s.PostingDate.HasValue ? s.PostingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SetStatusDate, o => o.ResolveUsing(s => s.SetStatusDate.HasValue ? s.SetStatusDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseInvoiceMng_PurchaseInvoice_View, DTO.PurchaseInvoiceDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.PurchaseInvoiceDate, o => o.ResolveUsing(s => s.PurchaseInvoiceDate.HasValue ? s.PurchaseInvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.PostingDate, o => o.ResolveUsing(s => s.PostingDate.HasValue ? s.PostingDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.PurchaseInvoiceDetailDTOs, o => o.MapFrom(s => s.PurchaseInvoiceMng_PurchaseInvoiceDetail_View))
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseInvoiceMng_PurchaseInvoiceDetail_View, DTO.PurchaseInvoiceDetailDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                // 
                // MAPPING DTO 2 DB
                //

                AutoMapper.Mapper.CreateMap<DTO.PurchaseInvoiceDetailDTO, PurchaseInvoiceDetail>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.PurchaseInvoiceDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PurchaseInvoiceDTO, PurchaseInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseInvoiceDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.PostingDate, o => o.Ignore())
                    .ForMember(d => d.SetStatusDate, o => o.Ignore())
                    .ForMember(d => d.PurchaseInvoiceDetail, o => o.Ignore())
                    .ForMember(d => d.PurchaseInvoiceID, o => o.Ignore());

                //
                //custom mapping
                //

                AutoMapper.Mapper.CreateMap<PurchaseInvoiceMng_ProductionItem_View, DTO.ProductionItemDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseInvoiceMng_SupplierPaymentTerm_View, DTO.PaymentTermDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseInvoiceMng_PurchaseOrder_View, DTO.PurchaseOrderItemDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseOrderDate, o => o.ResolveUsing(s => s.PurchaseOrderDate.HasValue ? s.PurchaseOrderDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ReceivingNoteDate, o => o.ResolveUsing(s => s.ReceivingNoteDate.HasValue ? s.ReceivingNoteDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_PurchaseInvoiceStatus_View, DTO.PurchaseInvoiceStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_PurchaseInvoiceType_View, DTO.PurchaseInvoiceTypeDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseInvoiceMng_GetFactoryRawMaterial_View, DTO.FactoryRawMaterialDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseInvoiceMng_FactoryWarehouse_View, DTO.FactoryWarehouseDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.PurchaseInvoiceSearchDTO> DB2DTO_Search(List<PurchaseInvoiceMng_PurchaseInvoice_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchaseInvoiceMng_PurchaseInvoice_SearchView>, List<DTO.PurchaseInvoiceSearchDTO>>(dbItems);
        }

        public DTO.PurchaseInvoiceDTO DB2DTO_PurchaseInvoice(PurchaseInvoiceMng_PurchaseInvoice_View dbItem)
        {
            return AutoMapper.Mapper.Map<PurchaseInvoiceMng_PurchaseInvoice_View, DTO.PurchaseInvoiceDTO>(dbItem);
        }

        public void DTO2DB_PurchaseInvoice(DTO.PurchaseInvoiceDTO dtoItem, ref PurchaseInvoice dbItem, int userId)
        {
            if (dtoItem.PurchaseInvoiceDetailDTOs != null)
            {
                foreach (var item in dbItem.PurchaseInvoiceDetail.ToArray())
                {
                    if (!dtoItem.PurchaseInvoiceDetailDTOs.Select(s => s.PurchaseInvoiceDetailID).Contains(item.PurchaseInvoiceDetailID))
                    {
                        dbItem.PurchaseInvoiceDetail.Remove(item);
                    }
                }
                foreach (var item in dtoItem.PurchaseInvoiceDetailDTOs)
                {
                    PurchaseInvoiceDetail dbDetail = new PurchaseInvoiceDetail();

                    if (item.PurchaseInvoiceDetailID < 0)
                    {
                        dbItem.PurchaseInvoiceDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.PurchaseInvoiceDetail.Where(s => s.PurchaseInvoiceDetailID == item.PurchaseInvoiceDetailID).FirstOrDefault();
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PurchaseInvoiceDetailDTO, PurchaseInvoiceDetail>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.PurchaseInvoiceDTO, PurchaseInvoice>(dtoItem, dbItem);
            if (!string.IsNullOrEmpty(dtoItem.PurchaseInvoiceDate))
            {
                dbItem.PurchaseInvoiceDate = dtoItem.PurchaseInvoiceDate.ConvertStringToDateTime();
            }

            if (!string.IsNullOrEmpty(dtoItem.PostingDate))
            {
                dbItem.PostingDate = dtoItem.PostingDate.ConvertStringToDateTime();
            }
            if (dbItem.PurchaseInvoiceID > 0)
            {
                dbItem.UpdatedBy = userId;
                dbItem.UpdatedDate = DateTime.Now;
            }
            else
            {
                dbItem.CreatedBy = userId;
                dbItem.CreatedDate = DateTime.Now;
            }
        }

        //custom mapping
        public List<DTO.PurchaseOrderItemDTO> DB2DTO_SearchPurchaseOrderData(List<PurchaseInvoiceMng_PurchaseOrder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchaseInvoiceMng_PurchaseOrder_View>, List<DTO.PurchaseOrderItemDTO>>(dbItems);
        }
        public List<DTO.FactoryRawMaterialDTO> DB2DTO_GetFactoryRawMaterial(List<PurchaseInvoiceMng_GetFactoryRawMaterial_View> Flist)
        {
            return AutoMapper.Mapper.Map<List<PurchaseInvoiceMng_GetFactoryRawMaterial_View>, List<DTO.FactoryRawMaterialDTO>>(Flist);
        }
    }
}
