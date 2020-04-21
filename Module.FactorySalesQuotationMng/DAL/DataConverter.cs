
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySalesQuotationMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // lst Factory Sale Quotation
                AutoMapper.Mapper.CreateMap<FactorySaleOrderMng_ListFactorySaleQuotion_View, DTO.FactorySaleQuotation>()
                 .IgnoreAllNonExisting()
                  .ForMember(d => d.Id, o => o.ResolveUsing(s => s.FactorySaleQuotationID))
                    .ForMember(d => d.Value, o => o.ResolveUsing(s => s.FactorySaleQuotationUD))
                    .ForMember(d => d.Label, o => o.ResolveUsing(s => s.FactorySaleQuotationUD));
                //get list Factory Sale Quotatio
                AutoMapper.Mapper.CreateMap<FactorySaleQuotationMng_FactorySaleQuotationSearchResult_View, DTO.FactorySaleQuotationSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                     .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                      .ForMember(d => d.ValidUntil, o => o.ResolveUsing(s => s.ValidUntil.HasValue ? s.ValidUntil.Value.ToString("dd/MM/yyyy") : ""))
                     .ForMember(d => d.DocumentDate, o => o.ResolveUsing(s => s.DocumentDate.HasValue ? s.DocumentDate.Value.ToString("dd/MM/yyyy") : ""))
                     .ForMember(d => d.ExpectedPaidDate, o => o.ResolveUsing(s => s.ExpectedPaidDate.HasValue ? s.ExpectedPaidDate.Value.ToString("dd/MM/yyyy") : ""))
                      .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                ///-------------------------------------------------------------------------------------------------------------------------------------
                //get list Client Contact
                AutoMapper.Mapper.CreateMap<FactorySaleQuotationMng_FactorySaleQuotation_ClientContact_View, DTO.ClientContact>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Id, o => o.ResolveUsing(s => s.ClientContactID))
                    .ForMember(d => d.Value, o => o.ResolveUsing(s => s.FullName))
                    .ForMember(d => d.Label, o => o.ResolveUsing(s => s.FullName));


                //get list Client Raw Material
                AutoMapper.Mapper.CreateMap<FactorySaleQuotationMng_FactorySaleQuotation_ListRawMaterial_View, DTO.RawMaterial>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Id, o => o.ResolveUsing(s => s.FactoryRawMaterialID))
                    .ForMember(d => d.Value, o => o.ResolveUsing(s => s.FactoryRawMaterialShortNM))
                    .ForMember(d => d.Label, o => o.ResolveUsing(s => s.FactoryRawMaterialOfficialNM));
                //get list raw payment tearm
                AutoMapper.Mapper.CreateMap<FactorySaleQuotationMng_FactorySaleQuotation_ListPaymentTearm_View, DTO.PaymentTerm>()
                    .IgnoreAllNonExisting();               
                //get list status
                AutoMapper.Mapper.CreateMap<SupportMng_FactorySaleQuotationStatus_View, DTO.Status>()
                    .IgnoreAllNonExisting();
                //get list lstShipmentToOption
                AutoMapper.Mapper.CreateMap<SupportMng_ShipmentToOption_View, DTO.ShipmentToOption>()
                    .IgnoreAllNonExisting();
                //get list ShipmentTypeOption
                AutoMapper.Mapper.CreateMap<SupportMng_ShipmentTypeOption_View, DTO.ShipmentTypeOption>()
                    .IgnoreAllNonExisting();
                //get list Warehouse
                AutoMapper.Mapper.CreateMap<FactorySaleQuotationMng_FactorySaleQuotation_ListWarehouse_View, DTO.Warehouse>()
                    .IgnoreAllNonExisting();
                //get list ProductionItem
                AutoMapper.Mapper.CreateMap<FactorySaleQuotationMng_FactorySaleQuotation_ListProductionItem_View, DTO.ProductionItem>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Id, o => o.ResolveUsing(s => s.ProductionItemID))
                    .ForMember(d => d.Value, o => o.ResolveUsing(s => s.ProductionItemUD))
                    .ForMember(d => d.Label, o => o.ResolveUsing(s => s.ProductionItemNM));

                AutoMapper.Mapper.CreateMap<FactorySaleQuotationMng_Employee_View, DTO.SaleEmployee>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.Id, o => o.ResolveUsing(s => s.EmployeeID))
                    .ForMember(d => d.Value, o => o.ResolveUsing(s => s.EmployeeNM))
                    .ForMember(d => d.Label, o => o.ResolveUsing(s => s.EmployeeNM));
                //--------------------------------------------------------------------------------------------------------------------------

                AutoMapper.Mapper.CreateMap<DTO.FactorySaleQuotationDetailDTO, FactorySaleQuotationDetail>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.FactorySaleQuotationDetailID, d => d.Ignore());


                AutoMapper.Mapper.CreateMap<DTO.FactorySaleQuotationAttachedFileDTO, FactorySaleQuotationAttachedFile>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.FactorySaleQuotationAttachedFileID, d => d.Ignore());

                AutoMapper.Mapper.CreateMap<FactorySaleQuotationMng_FactorySaleQuotation_View, DTO.FactorySaleQuotationDTO>()
                .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                     .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                      .ForMember(d => d.DocumentDate, o => o.ResolveUsing(s => s.DocumentDate.HasValue ? s.DocumentDate.Value.ToString("dd/MM/yyyy") : ""))
                       .ForMember(d => d.ValidUntil, o => o.ResolveUsing(s => s.ValidUntil.HasValue ? s.ValidUntil.Value.ToString("dd/MM/yyyy") : ""))
                        .ForMember(d => d.ExpectedPaidDate, o => o.ResolveUsing(s => s.ExpectedPaidDate.HasValue ? s.ExpectedPaidDate.Value.ToString("dd/MM/yyyy") : ""))
                          .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                            .ForMember(d => d.DeletedDate, o => o.ResolveUsing(s => s.DeletedDate.HasValue ? s.DeletedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LstFactorySaleQuotationAttachedFile, o => o.MapFrom(s => s.FactorySaleQuotationMng_FactorySaleQuotationFactorySaleQuotationAttachedFile_View))
                    .ForMember(d => d.LstFactorySaleQuotationDetail, o => o.MapFrom(s => s.FactorySaleQuotationMng_FactorySaleQuotationDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactorySaleQuotationDTO, FactorySaleQuotation>()
                     .IgnoreAllNonExisting()
                    .ForMember(d => d.FactorySaleQuotationID, d => d.Ignore())
                     .ForMember(d => d.FactorySaleQuotationUD, d => d.Ignore())
                    .ForMember(d => d.ValidUntil, d => d.Ignore())
                    .ForMember(d => d.UpdatedDate, d => d.Ignore())
                    .ForMember(d => d.ExpectedPaidDate, d => d.Ignore())
                    .ForMember(d => d.DocumentDate, d => d.Ignore())
                    .ForMember(d => d.DeletedDate, d => d.Ignore())
                    .ForMember(d => d.ConfirmedDate, d => d.Ignore())
                     .ForMember(d => d.CreatedDate, d => d.Ignore())
                    .ForMember(d => d.FactorySaleQuotationAttachedFile, d => d.Ignore())
                    .ForMember(d => d.FactorySaleQuotationDetail, d => d.Ignore());
                AutoMapper.Mapper.CreateMap<DTO.FactorySaleQuotationDetailDTO, FactorySaleQuotationDetail>()
                  .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DTO.FactorySaleQuotationAttachedFileDTO, FactorySaleQuotationAttachedFile>()
                 .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<FactorySaleQuotationMng_FactorySaleQuotationFactorySaleQuotationAttachedFile_View, DTO.FactorySaleQuotationAttachedFileDTO>()
                  .IgnoreAllNonExisting()                 
                   .ForMember(d => d.OtherFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.OtherFileUrl) ? s.OtherFileUrl : "no-image.jpg")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactorySaleQuotationMng_FactorySaleQuotationDetail_View, DTO.FactorySaleQuotationDetailDTO>()
                 .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_SupplierContactQuickInfo_View, DTO.SupplierContactQuickInfo>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d=>d.Condition(s=>!s.IsSourceValueNull));

                

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        //get list Factory Sale Quotation
        public List<DTO.FactorySaleQuotationSearchDTO> FactorySaleQuotationGetList(List<FactorySaleQuotationMng_FactorySaleQuotationSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactorySaleQuotationMng_FactorySaleQuotationSearchResult_View>, List<DTO.FactorySaleQuotationSearchDTO>>(dbItems);
        }
       
        //get list Client Contact
        public List<DTO.ClientContact> GetListClientContact(List<FactorySaleQuotationMng_FactorySaleQuotation_ClientContact_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<FactorySaleQuotationMng_FactorySaleQuotation_ClientContact_View>, List<DTO.ClientContact>>(dbItem);
        }

        //get list raw material
        public List<DTO.RawMaterial> GetListRawMaterial(List<FactorySaleQuotationMng_FactorySaleQuotation_ListRawMaterial_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<FactorySaleQuotationMng_FactorySaleQuotation_ListRawMaterial_View>, List<DTO.RawMaterial>>(dbItem);
        }

        //get list raw payment tearm
        public List<DTO.PaymentTerm> GetListPaymentTearm(List<FactorySaleQuotationMng_FactorySaleQuotation_ListPaymentTearm_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<FactorySaleQuotationMng_FactorySaleQuotation_ListPaymentTearm_View>, List<DTO.PaymentTerm>>(dbItem);
        }
        //get list Factory Sale Quotation
        public List<DTO.FactorySaleQuotation> GetListFactorySaleQuotation(List<FactorySaleOrderMng_ListFactorySaleQuotion_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<FactorySaleOrderMng_ListFactorySaleQuotion_View>, List<DTO.FactorySaleQuotation>>(dbItem);
        }
        //get list status
        public List<DTO.Status> GetListStatus(List<SupportMng_FactorySaleQuotationStatus_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactorySaleQuotationStatus_View>, List<DTO.Status>>(dbItem);
        }
        //get list lstShipmentToOption

        public List<DTO.ShipmentToOption> GetListShipmentToOption(List<SupportMng_ShipmentToOption_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ShipmentToOption_View>, List<DTO.ShipmentToOption>>(dbItem);
        }

        //get list ShipmentTypeOption
        public List<DTO.ShipmentTypeOption> GetListShipmentTypeOption(List<SupportMng_ShipmentTypeOption_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ShipmentTypeOption_View>, List<DTO.ShipmentTypeOption>>(dbItem);
        }

        //get list Warehouse
        public List<DTO.Warehouse> GetListListWarehouse(List<FactorySaleQuotationMng_FactorySaleQuotation_ListWarehouse_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<FactorySaleQuotationMng_FactorySaleQuotation_ListWarehouse_View>, List<DTO.Warehouse>>(dbItem);
        }

        //get list ProductionItem
        public List<DTO.ProductionItem> GetListListProductionItem(List<FactorySaleQuotationMng_FactorySaleQuotation_ListProductionItem_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<FactorySaleQuotationMng_FactorySaleQuotation_ListProductionItem_View>, List<DTO.ProductionItem>>(dbItem);
        }

        //get list Sale Employee
        public List<DTO.SaleEmployee> GetListListEmployee(List<FactorySaleQuotationMng_Employee_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<FactorySaleQuotationMng_Employee_View>, List<DTO.SaleEmployee>>(dbItem);
        }
        //----------------------------------------------------------------------------------------------------------

        public List<DTO.SupplierContactQuickInfo> getSupplierContactQuickInfo (List<FactoryRawMaterialMng_SupplierContactQuickInfo_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryRawMaterialMng_SupplierContactQuickInfo_View>, List<DTO.SupplierContactQuickInfo>>(dbItems);
        }
        // update Factory Sale Quotation
        public void updateFactorySaleQuotation(DTO.FactorySaleQuotationDTO dtoItem, ref FactorySaleQuotation dbItem)
        {


            //Update Factory Sale Quotation Detail
            if (dtoItem.LstFactorySaleQuotationDetail != null)
            {
                foreach (FactorySaleQuotationDetail item in dbItem.FactorySaleQuotationDetail.ToArray())
                {
                    if (!dtoItem.LstFactorySaleQuotationDetail.Select(x => x.FactorySaleQuotationDetailID).Contains(item.FactorySaleQuotationDetailID))
                        dbItem.FactorySaleQuotationDetail.Remove(item);
                }
                foreach (DTO.FactorySaleQuotationDetailDTO dto in dtoItem.LstFactorySaleQuotationDetail)
                {
                    FactorySaleQuotationDetail item;
                    if (dto.FactorySaleQuotationDetailID <= 0)
                    {
                        item = new FactorySaleQuotationDetail();
                        dbItem.FactorySaleQuotationDetail.Add(item);
                    }
                    else
                    {
                        item = dbItem.FactorySaleQuotationDetail.FirstOrDefault(s => s.FactorySaleQuotationDetailID == dto.FactorySaleQuotationDetailID);
                    }
                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.FactorySaleQuotationDetailDTO, FactorySaleQuotationDetail>(dto, item);
                }
            }
            //update Factory Sale Quotation Attached file             
            if (dtoItem.LstFactorySaleQuotationAttachedFile != null)
            {
                foreach (FactorySaleQuotationAttachedFile item in dbItem.FactorySaleQuotationAttachedFile.ToArray())
                {
                    if (!dtoItem.LstFactorySaleQuotationAttachedFile.Select(x => x.FactorySaleQuotationAttachedFileID).Contains(item.FactorySaleQuotationAttachedFileID))
                        dbItem.FactorySaleQuotationAttachedFile.Remove(item);
                }
                foreach (var item in dtoItem.LstFactorySaleQuotationAttachedFile)
                {
                    FactorySaleQuotationAttachedFile dbAttachedFile;
                    if(item.FactorySaleQuotationAttachedFileID < 0)
                    {
                        dbAttachedFile = new FactorySaleQuotationAttachedFile();
                        dbItem.FactorySaleQuotationAttachedFile.Add(dbAttachedFile);
                    }
                    else
                    {
                        dbAttachedFile = dbItem.FactorySaleQuotationAttachedFile.Where(o => o.FactorySaleQuotationAttachedFileID == item.FactorySaleQuotationAttachedFileID).FirstOrDefault();
                    }
                    if (dbAttachedFile != null)
                        AutoMapper.Mapper.Map<DTO.FactorySaleQuotationAttachedFileDTO, FactorySaleQuotationAttachedFile>(item, dbAttachedFile);
                   
                }
            }
            
            AutoMapper.Mapper.Map<DTO.FactorySaleQuotationDTO, FactorySaleQuotation>(dtoItem, dbItem);

        }
        //get detail a Factory Sale Quotation
        public DTO.FactorySaleQuotationDTO GetDetailFactorySaleQuotation(FactorySaleQuotationMng_FactorySaleQuotation_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactorySaleQuotationMng_FactorySaleQuotation_View, DTO.FactorySaleQuotationDTO>(dbItem);
        }
    }
}
