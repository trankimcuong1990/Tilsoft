using Library;
using System.Collections.Generic;
using System.Linq;

namespace Module.FactorySalesOrderMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                //get list Factory Sale order
                AutoMapper.Mapper.CreateMap<FactorySaleOrderMng_SearchResult_View, DTO.FactorySalesOrderSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ValidUntil, o => o.ResolveUsing(s => s.ValidUntil.HasValue ? s.ValidUntil.Value.ToString("dd/MM/yyyy") : ""))
                     .ForMember(d => d.DocumentDate, o => o.ResolveUsing(s => s.DocumentDate.HasValue ? s.DocumentDate.Value.ToString("dd/MM/yyyy") : ""))
                     .ForMember(d => d.ExpectedPaidDate, o => o.ResolveUsing(s => s.ExpectedPaidDate.HasValue ? s.ExpectedPaidDate.Value.ToString("dd/MM/yyyy") : ""))
                      .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                //--------------------------------------------------------------------------------------------------------------
                //get list Client Contact
                AutoMapper.Mapper.CreateMap<FactorySaleQuotationMng_FactorySaleQuotation_ClientContact_View, DTO.ClientContact>()
                    .IgnoreAllNonExisting()
                     .ForMember(d => d.Id, o => o.ResolveUsing(s => s.ClientContactID))
                    .ForMember(d => d.Value, o => o.ResolveUsing(s => s.FullName))
                    .ForMember(d => d.Label, o => o.ResolveUsing(s => s.FullName));
                FrameworkSetting.Setting.Maps.Add(mapName);
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

                // lst Factory Sale Quotation
                AutoMapper.Mapper.CreateMap<FactorySaleOrderMng_ListFactorySaleQuotion_View, DTO.FactorySaleQuotation>()
                 .IgnoreAllNonExisting()
                  .ForMember(d => d.Id, o => o.ResolveUsing(s => s.FactorySaleQuotationID))
                    .ForMember(d => d.Value, o => o.ResolveUsing(s => s.FactorySaleQuotationUD))
                    .ForMember(d => d.Label, o => o.ResolveUsing(s => s.FactorySaleQuotationUD));

                // lst Factory Sale Quotation
                AutoMapper.Mapper.CreateMap<FactorySaleOrderMng_FilterSaleOrder_View, DTO.FactorySaleOrder>()
                 .IgnoreAllNonExisting()
                  .ForMember(d => d.Id, o => o.ResolveUsing(s => s.FactorySaleOrderID))
                    .ForMember(d => d.Value, o => o.ResolveUsing(s => s.FactorySaleOrderUD))
                    .ForMember(d => d.Label, o => o.ResolveUsing(s => s.FactorySaleOrderUD));
                //---------------------------------------------------------------------------------------              

                AutoMapper.Mapper.CreateMap<DTO.FactorySalesOrderDetailDTO, FactorySaleOrderDetail>()
                 .IgnoreAllNonExisting()
                  .ForMember(d => d.FactorySaleOrderDetailID, d => d.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactorySalesOrderAttachedFileDTO, FactorySaleOrderAttachedFile>()
               .IgnoreAllNonExisting()
               .ForMember(d => d.FactorySaleOrderAttachedFileID, d => d.Ignore());

                AutoMapper.Mapper.CreateMap<FactorySaleOrderMng_FactorySaleOrder_View, DTO.FactorySalesOrderDTO>()
                .IgnoreAllNonExisting()
                 .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                     .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                      .ForMember(d => d.DocumentDate, o => o.ResolveUsing(s => s.DocumentDate.HasValue ? s.DocumentDate.Value.ToString("dd/MM/yyyy") : ""))
                       .ForMember(d => d.ValidUntil, o => o.ResolveUsing(s => s.ValidUntil.HasValue ? s.ValidUntil.Value.ToString("dd/MM/yyyy") : ""))
                        .ForMember(d => d.ExpectedPaidDate, o => o.ResolveUsing(s => s.ExpectedPaidDate.HasValue ? s.ExpectedPaidDate.Value.ToString("dd/MM/yyyy") : ""))
                         .ForMember(d => d.DeletedDate, o => o.ResolveUsing(s => s.DeletedDate.HasValue ? s.DeletedDate.Value.ToString("dd/MM/yyyy") : ""))
                          .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LstFactorySaleOrderAttachedFile, o => o.MapFrom(s => s.FactorySaleOrderMng_FactorySaleOrderAttachedFile_View))
                    .ForMember(d => d.LstFactorySaleOrderDetail, o => o.MapFrom(s => s.FactorySaleOrderMng_FactorySaleOrderDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactorySalesOrderDTO, FactorySaleOrder>()
                .IgnoreAllNonExisting()
                 .ForMember(d => d.FactorySaleOrderID, d => d.Ignore())
                 .ForMember(d => d.FactorySaleOrderUD, d => d.Ignore())
                    .ForMember(d => d.ValidUntil, d => d.Ignore())
                    .ForMember(d => d.UpdatedDate, d => d.Ignore())
                    .ForMember(d => d.ExpectedPaidDate, d => d.Ignore())
                      .ForMember(d => d.CreatedDate, d => d.Ignore())
                    .ForMember(d => d.DocumentDate, d => d.Ignore())
                    .ForMember(d => d.DeletedDate, d => d.Ignore())
                    .ForMember(d => d.ConfirmedDate, d => d.Ignore())
                    .ForMember(d => d.FactorySaleOrderAttachedFile, d => d.Ignore())
                    .ForMember(d => d.FactorySaleOrderDetail, d => d.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactorySaleQuotationDetailDTO, FactorySaleOrderDetail>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DTO.FactorySalesOrderAttachedFileDTO, FactorySaleOrderAttachedFile>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<FactorySaleOrderMng_FactorySaleOrderAttachedFile_View, DTO.FactorySalesOrderAttachedFileDTO>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.OtherFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.OtherFileUrl) ? s.OtherFileUrl : "no-image.jpg")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactorySaleOrderMng_FactorySaleOrderDetail_View, DTO.FactorySalesOrderDetailDTO>()
                 .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<FactorySaleOrderMng_QuickSearchFactorySaleQuotation_View, DTO.FactorySaleQuotationDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ValidUntil, o => o.ResolveUsing(s => s.ValidUntil.HasValue ? s.ValidUntil.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DocumentDate, o => o.ResolveUsing(s => s.DocumentDate.HasValue ? s.DocumentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ExpectedPaidDate, o => o.ResolveUsing(s => s.ExpectedPaidDate.HasValue ? s.ExpectedPaidDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FactorySaleQuotationDetailDTOs, o => o.MapFrom(s => s.FactorySaleOrderMng_QuickSearchFactorySaleQuotationDetail_View))
                    .ForMember(d => d.FactorySaleQuotationAttachedFileDTOs, o => o.MapFrom(s => s.FactorySaleOrderMng_QuickSearhFactorySaleQuotationAttachedFile_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactorySaleOrderMng_QuickSearchFactorySaleQuotationDetail_View, DTO.FactorySaleQuotationDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactorySaleOrderMng_QuickSearhFactorySaleQuotationAttachedFile_View, DTO.FactorySaleQuotationAttachedFileDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_SupplierContactQuickInfo_View, DTO.SupplierContactQuickInfo>()
               .IgnoreAllNonExisting()
               .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        //get list Factory Sale Order
        public List<DTO.FactorySalesOrderSearchDTO> FactorySaleOrderGetList(List<FactorySaleOrderMng_SearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactorySaleOrderMng_SearchResult_View>, List<DTO.FactorySalesOrderSearchDTO>>(dbItems);
        }
        public List<DTO.SupplierContactQuickInfo> getSupplierContactQuickInfo(List<FactoryRawMaterialMng_SupplierContactQuickInfo_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryRawMaterialMng_SupplierContactQuickInfo_View>, List<DTO.SupplierContactQuickInfo>>(dbItems);
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
        //get list Factory Sale Quotation
        public List<DTO.FactorySaleQuotation> GetListFactorySaleQuotation(List<FactorySaleOrderMng_ListFactorySaleQuotion_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<FactorySaleOrderMng_ListFactorySaleQuotion_View>, List<DTO.FactorySaleQuotation>>(dbItem);
        }
        //get list Factory Sale Order
        public List<DTO.FactorySaleOrder> GetFilterFactorySaleOrder(List<FactorySaleOrderMng_FilterSaleOrder_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<FactorySaleOrderMng_FilterSaleOrder_View>, List<DTO.FactorySaleOrder>>(dbItem);
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

        // update Factory Sale Order
        public void updateFactorySaleOrder(DTO.FactorySalesOrderDTO dtoItem, ref FactorySaleOrder dbItem)
        {
            //Update Factory Sale Order Detail
            if (dtoItem.LstFactorySaleOrderDetail != null)
            {
                foreach (FactorySaleOrderDetail item in dbItem.FactorySaleOrderDetail.ToArray())
                {
                    if (!dtoItem.LstFactorySaleOrderDetail.Select(x => x.FactorySaleOrderDetailID).Contains(item.FactorySaleOrderDetailID))
                        dbItem.FactorySaleOrderDetail.Remove(item);
                }
                foreach (DTO.FactorySalesOrderDetailDTO dto in dtoItem.LstFactorySaleOrderDetail)
                {
                    FactorySaleOrderDetail item;
                    if (dto.FactorySaleOrderDetailID <= 0)
                    {
                        item = new FactorySaleOrderDetail();
                        dbItem.FactorySaleOrderDetail.Add(item);
                    }
                    else
                    {
                        item = dbItem.FactorySaleOrderDetail.FirstOrDefault(s => s.FactorySaleOrderDetailID == dto.FactorySaleOrderDetailID);
                    }
                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.FactorySalesOrderDetailDTO, FactorySaleOrderDetail>(dto, item);
                }
            }
            //update Factory Sale Order Attached file            

            if (dtoItem.LstFactorySaleOrderAttachedFile != null)
            {
                foreach (FactorySaleOrderAttachedFile item in dbItem.FactorySaleOrderAttachedFile.ToArray())
                {
                    if (!dtoItem.LstFactorySaleOrderAttachedFile.Select(x => x.FactorySaleOrderAttachedFileID).Contains(item.FactorySaleOrderAttachedFileID))
                        dbItem.FactorySaleOrderAttachedFile.Remove(item);
                }
                foreach (var item in dtoItem.LstFactorySaleOrderAttachedFile)
                {
                    FactorySaleOrderAttachedFile dbAttachedFile;
                    if (item.FactorySaleOrderAttachedFileID < 0)
                    {
                        dbAttachedFile = new FactorySaleOrderAttachedFile();
                        dbItem.FactorySaleOrderAttachedFile.Add(dbAttachedFile);
                    }
                    else
                    {
                        dbAttachedFile = dbItem.FactorySaleOrderAttachedFile.Where(o => o.FactorySaleOrderAttachedFileID == item.FactorySaleOrderAttachedFileID).FirstOrDefault();
                    }
                    if (dbAttachedFile != null)
                        AutoMapper.Mapper.Map<DTO.FactorySalesOrderAttachedFileDTO, FactorySaleOrderAttachedFile>(item, dbAttachedFile);

                }
            }
            AutoMapper.Mapper.Map<DTO.FactorySalesOrderDTO, FactorySaleOrder>(dtoItem, dbItem);

        }
        //get detail a Factory Sale Order
        public DTO.FactorySalesOrderDTO GetDetailFactorySaleOrder(FactorySaleOrderMng_FactorySaleOrder_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactorySaleOrderMng_FactorySaleOrder_View, DTO.FactorySalesOrderDTO>(dbItem);
        }
    }
}
