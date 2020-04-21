using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySaleInvoice.DAL
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
                AutoMapper.Mapper.CreateMap<FactorySaleInvoice_FactorySaleInvoice_SeachView, DTO.FactorySaleInvoiceSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PostingDate, o => o.ResolveUsing(s => s.PostingDate.HasValue ? s.PostingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactorySaleInvoice_FactorySaleInvoice_View, DTO.FactorySaleInvoiceDTO>()
                    .IgnoreAllNonExisting()
                   .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.PostingDate, o => o.ResolveUsing(s => s.PostingDate.HasValue ? s.PostingDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.FactorySaleInvoiceDetailDTOs, o => o.MapFrom(s => s.FactorySaleInvoice_FactorySaleInvoiceDetail_View))
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactorySaleInvoice_FactorySaleInvoiceDetail_View, DTO.FactorySaleInvoiceDetailDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactorySaleInvoice_SupplierPaymentTerm_View, DTO.PaymentTermDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // 
                // MAPPING DTO 2 DB
                //

                AutoMapper.Mapper.CreateMap<DTO.FactorySaleInvoiceDetailDTO, FactorySaleInvoiceDetail>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.FactorySaleInvoiceDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactorySaleInvoiceDTO, FactorySaleInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.PostingDate, o => o.Ignore())
                    //.ForMember(d => d.SetStatusDate, o => o.Ignore())
                    .ForMember(d => d.FactorySaleInvoiceDetail, o => o.Ignore())
                    .ForMember(d => d.FactorySaleInvoiceID, o => o.Ignore());

                //
                // Custom Mapping
                //
                AutoMapper.Mapper.CreateMap<FactorySaleInvoice_ProductionItem_View, DTO.ProductionItemDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_FactorySaleInvoiceStatus_View, DTO.FactorySaleInvoiceStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_SubSupplier_View, DTO.FactoryRawMaterialDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_FactorySaleInvoiceStatus_View, DTO.FactorySaleInvoiceStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactorySaleInvoice_FactorySaleOrderDetail_View, DTO.FactorySaleOrderItemDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DocumentDate, o => o.ResolveUsing(s => s.DocumentDate.HasValue ? s.DocumentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);

            }
        }
        public List<DTO.FactorySaleInvoiceSearchDTO> DB2DTO_Search(List<FactorySaleInvoice_FactorySaleInvoice_SeachView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactorySaleInvoice_FactorySaleInvoice_SeachView>, List<DTO.FactorySaleInvoiceSearchDTO>>(dbItems);
        }

        public DTO.FactorySaleInvoiceDTO DB2DTO_FactorySaleInvoice(FactorySaleInvoice_FactorySaleInvoice_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactorySaleInvoice_FactorySaleInvoice_View, DTO.FactorySaleInvoiceDTO>(dbItem);
        }

        public List<DTO.FactoryRawMaterialDTO> DB2DTO_GetFactoryRawMaterial(List<SupportMng_SubSupplier_View> Flist)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SubSupplier_View>, List<DTO.FactoryRawMaterialDTO>>(Flist);
        }

        public void DTO2DB_FactorySaleInvoice(DTO.FactorySaleInvoiceDTO dtoItem, ref FactorySaleInvoice dbItem, int userId)
        {
            if (dtoItem.FactorySaleInvoiceDetailDTOs != null)
            {
                foreach (var item in dbItem.FactorySaleInvoiceDetail.ToArray())
                {
                    if (!dtoItem.FactorySaleInvoiceDetailDTOs.Select(s => s.FactorySaleInvoiceDetailID).Contains(item.FactorySaleInvoiceDetailID))
                    {
                        dbItem.FactorySaleInvoiceDetail.Remove(item);
                    }
                }
                foreach (var item in dtoItem.FactorySaleInvoiceDetailDTOs)
                {
                    FactorySaleInvoiceDetail dbDetail = new FactorySaleInvoiceDetail();

                    if (item.FactorySaleInvoiceDetailID < 0)
                    {
                        dbItem.FactorySaleInvoiceDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactorySaleInvoiceDetail.Where(s => s.FactorySaleInvoiceDetailID == item.FactorySaleInvoiceDetailID).FirstOrDefault();
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactorySaleInvoiceDetailDTO, FactorySaleInvoiceDetail>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.FactorySaleInvoiceDTO, FactorySaleInvoice>(dtoItem, dbItem);
            if (!string.IsNullOrEmpty(dtoItem.InvoiceDate))
            {
                dbItem.InvoiceDate = dtoItem.InvoiceDate.ConvertStringToDateTime();
            }

            if (!string.IsNullOrEmpty(dtoItem.PostingDate))
            {
                dbItem.PostingDate = dtoItem.PostingDate.ConvertStringToDateTime();
            }
            if (dbItem.FactorySaleInvoiceID > 0)
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

        public List<DTO.FactorySaleOrderItemDTO> DB2DTO_SearchFactorySaleOrderData(List<FactorySaleInvoice_FactorySaleOrderDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactorySaleInvoice_FactorySaleOrderDetail_View>, List<DTO.FactorySaleOrderItemDTO>>(dbItems);
        }
    }
}
