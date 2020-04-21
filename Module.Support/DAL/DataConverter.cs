using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;
using Module.Support.DTO;

namespace Module.Support.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<SupportMng_ProductionItemUnit_View, DTO.ProductionItemUnit>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AgendaMng_Employee_View, DTO.EmployeeDepartmentDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<SupportMng_WorkOrderType_View, DTO.WorkOrderType>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_WorkOrderStatus_View, DTO.WorkOrderStatus>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_WorkCenter_View, DTO.WorkCenter>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_TransportCostChargeType_View, DTO.TransportCostChargeType>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_Client2_View, DTO.QuickSearchResult2>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_Factory2_View, DTO.QuickSearchResult2>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_TransportCostType_View, DTO.TransportCostType>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ProductionItemType_View, DTO.ProductionItemType>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_OPSequenceDetail_View, DTO.OPSequenceDetail>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ProductionTeam_View, DTO.ProductionTeam>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_FactoryWarehousePallet_View, DTO.FactoryWarehousePallet>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ProductionItem_View, DTO.ProductionItem>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductionItemUnits, o => o.MapFrom(s => s.SupportMng_ProductionItemUnit_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_Client_View, DTO.Client>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ClientPaymentMethod_View, DTO.ClientPaymentMethod>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // factory
                AutoMapper.Mapper.CreateMap<List_Factory, DTO.Factory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_AccessibleFactoy_View, DTO.Factory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // frame material 
                AutoMapper.Mapper.CreateMap<List_FrameMaterial_View, DTO.FrameMaterial>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //factory material unit
                AutoMapper.Mapper.CreateMap<SupportMng_Unit_View, DTO.Unit>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //factory material group
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryMaterialGroup_View, DTO.FactoryMaterialGroup>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //factory material type
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryMaterialType_View, DTO.FactoryMaterialType>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //factory material color
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryMaterialColor_View, DTO.FactoryMaterialColor>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //factory material
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryMaterial_View, DTO.FactoryMaterial>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //factory material image
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryMaterialImage_View, DTO.FactoryMaterialImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ThumbnailLocation) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ThumbnailLocation)))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.FileLocation) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.FileLocation)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //factory order
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryOrder_View, DTO.FactoryOrder>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // quotation status
                AutoMapper.Mapper.CreateMap<SupportMng_QuotationStatus_View, DTO.QuotationStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // factory team
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryTeam_View, DTO.FactoryTeam>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryTeamSteps, o => o.MapFrom(s => s.SupportMng_FactoryTeamStep_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // factory team step
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryTeamStep_View, DTO.FactoryTeamStep>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // factory area
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryArea_View, DTO.FactoryArea>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // factory step
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryStep_View, DTO.FactoryStep>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // plc image type
                AutoMapper.Mapper.CreateMap<SupportMng_PLCImageType_View, DTO.PLCImageType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // container type
                AutoMapper.Mapper.CreateMap<SupportMng_ContainerType_View, DTO.ContainerType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // supplier
                AutoMapper.Mapper.CreateMap<SupportMng_Supplier_View, DTO.Supplier>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // meeting location
                AutoMapper.Mapper.CreateMap<SupportMng_MeetingLocation_View, DTO.MeetingLocation>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // product type
                AutoMapper.Mapper.CreateMap<SupportMng_ProductType_View, DTO.ProductType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // product group
                AutoMapper.Mapper.CreateMap<SupportMng_ProductGroup_View, DTO.ProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // manufacturer
                AutoMapper.Mapper.CreateMap<SupportMng_ManufacturerCountry_View, DTO.ManufacturerCountry>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model status
                AutoMapper.Mapper.CreateMap<SupportMng_ModelStatus_View, DTO.ModelStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // product category
                AutoMapper.Mapper.CreateMap<SupportMng_ProductCategory_View, DTO.ProductCategory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // user
                AutoMapper.Mapper.CreateMap<SupportMng_ActiveUser_View, DTO.User>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // task status
                AutoMapper.Mapper.CreateMap<SupportMng_TaskStatus_View, DTO.TaskStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // POL 
                AutoMapper.Mapper.CreateMap<SupportMng_POL_View, DTO.POL>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // POD 
                AutoMapper.Mapper.CreateMap<SupportMng_POD_View, DTO.POD>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // movement
                AutoMapper.Mapper.CreateMap<SupportMng_MovementTerm_View, DTO.MovementTerm>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // delivery term
                AutoMapper.Mapper.CreateMap<SupportMng_DeliveryTerm_View, DTO.DeliveryTerm>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //ledger account
                AutoMapper.Mapper.CreateMap<SupportMng_LedgerAccount_View, DTO.LedgerAccount>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // AVF supplier
                AutoMapper.Mapper.CreateMap<SupportMng_AVFSupplier_View, DTO.AVFSupplier>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Employee
                AutoMapper.Mapper.CreateMap<SupportMng_Employee_View, DTO.Employee>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Saler
                AutoMapper.Mapper.CreateMap<SupportMng_Saler_View, DTO.Saler>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // forwarder
                AutoMapper.Mapper.CreateMap<SupportMng_Forwarder_View, DTO.Forwarder>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //factory goods procudure
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryGoodsProcedure_View, DTO.FactoryGoodsProcedure>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryGoodsProcedureDetails, o => o.MapFrom(s => s.SupportMng_FactoryGoodsProcedureDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //factory goods procudure detail
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryGoodsProcedureDetail_View, DTO.FactoryGoodsProcedureDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //factory cost type
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryCostType_View, DTO.FactoryCostType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // payment term
                AutoMapper.Mapper.CreateMap<SupportMng_PaymentTerm_View, DTO.PaymentTerm>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //factory finished product
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryFinishedProduct_View, DTO.FactoryFinishedProduct>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //internal company
                AutoMapper.Mapper.CreateMap<SupportMng_InternalCompany_View, DTO.InternalCompany>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //internal department
                AutoMapper.Mapper.CreateMap<SupportMng_InternalDepartment_View, DTO.InternalDepartment>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //task role
                AutoMapper.Mapper.CreateMap<SupportMng_TaskRole_View, DTO.TaskRole>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //job level
                AutoMapper.Mapper.CreateMap<SupportMng_JobLevel_View, DTO.JobLevel>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //LP Status
                AutoMapper.Mapper.CreateMap<SupportMng_LabelingPackaging_LPStatus_View, DTO.LabelingPackagingStatus>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //location
                AutoMapper.Mapper.CreateMap<SupportMng_Location_View, DTO.Location>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //location
                AutoMapper.Mapper.CreateMap<SupportMng_FactoryLocation_View, DTO.FactoryLocation>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //leave request time
                AutoMapper.Mapper.CreateMap<SupportMng_LeaveRequestTime_View, DTO.LeaveRequestTime>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //leave type
                AutoMapper.Mapper.CreateMap<SupportMng_LeaveType_View, DTO.LeaveType>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // sample order status
                AutoMapper.Mapper.CreateMap<SupportMng_SampleOrderStatus_View, DTO.SampleOrderStatus>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // sample transport type
                AutoMapper.Mapper.CreateMap<SupportMng_SampleTransportType_View, DTO.SampleTransportType>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // sample purpose
                AutoMapper.Mapper.CreateMap<SupportMng_SamplePurpose_View, DTO.SamplePurpose>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // sample request type
                AutoMapper.Mapper.CreateMap<SupportMng_SampleRequestType_View, DTO.SampleRequestType>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // sample product status
                AutoMapper.Mapper.CreateMap<SupportMng_SampleProductStatus_View, DTO.SampleProductStatus>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // QUICK SEARCH 
                // model 
                AutoMapper.Mapper.CreateMap<SupportMng_ModelSearchResult_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // material
                AutoMapper.Mapper.CreateMap<SupportMng_MaterialSearchResult_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // material type
                AutoMapper.Mapper.CreateMap<SupportMng_MaterialTypeSearchResult_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // material color
                AutoMapper.Mapper.CreateMap<SupportMng_MaterialColorSearchResult_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // frame material
                AutoMapper.Mapper.CreateMap<SupportMng_FrameMaterialSearchResult_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // frame material color
                AutoMapper.Mapper.CreateMap<SupportMng_FrameMaterialColorSearchResult_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // sub material
                AutoMapper.Mapper.CreateMap<SupportMng_SubMaterialSearchResult_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // sub material color
                AutoMapper.Mapper.CreateMap<SupportMng_SubMaterialColorSearchResult_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // cushion
                AutoMapper.Mapper.CreateMap<SupportMng_CushionSearchResult_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // back cushion
                AutoMapper.Mapper.CreateMap<SupportMng_BackCushionSearchResult_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // seat cushion
                AutoMapper.Mapper.CreateMap<SupportMng_SeatCushionSearchResult_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // client
                AutoMapper.Mapper.CreateMap<SupportMng_ClientSearchResult_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // cushion color
                AutoMapper.Mapper.CreateMap<SupportMng_CushionColorSearchResult_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // model image
                AutoMapper.Mapper.CreateMap<SupportMng_ModelImage_View, DTO.ModelImage>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                     .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // product info
                AutoMapper.Mapper.CreateMap<SupportMng_ProductInfo_View, DTO.ProductInfo>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                     .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // client 2 - client result with client name
                AutoMapper.Mapper.CreateMap<SupportMng_ClientSearchResult2_View, DTO.QuickSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //inspection type
                AutoMapper.Mapper.CreateMap<SupportMng_InspectionType_View, DTO.InspectionType>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //packaging type
                AutoMapper.Mapper.CreateMap<SupportMng_PackagingType_View, DTO.PackagingType>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //user group
                AutoMapper.Mapper.CreateMap<SupportMng_UserGroup_View, DTO.UserGroup>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //office
                AutoMapper.Mapper.CreateMap<SupportMng_Office_View, DTO.Office>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                //dev request history action
                AutoMapper.Mapper.CreateMap<SupportMng_DevRequestHistoryAction_View, DTO.DevRequestHistoryAction>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //dev request priority
                AutoMapper.Mapper.CreateMap<SupportMng_DevRequestPriority_View, DTO.DevRequestPriority>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //dev request project
                AutoMapper.Mapper.CreateMap<SupportMng_DevRequestProject_View, DTO.DevRequestProject>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //dev request status
                AutoMapper.Mapper.CreateMap<SupportMng_DevRequestStatus_View, DTO.DevRequestStatus>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //dev request type
                AutoMapper.Mapper.CreateMap<SupportMng_DevRequestType_View, DTO.DevRequestType>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // TypeOfCost 
                Mapper.CreateMap<SupportMng_TypeOfCost_View, DTO.TypeOfCost>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // TypeOfCharge 
                Mapper.CreateMap<SupportMng_TypeOfCharge_View, DTO.TypeOfCharge>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // forwarder
                AutoMapper.Mapper.CreateMap<SupportMng_Supplier_View, DTO.Supplier>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Type Currency: USD or EURO
                Mapper.CreateMap<SupportMng_TypeOfCurrency_View, DTO.TypeOfCurrency>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Type Currency: USD or EURO
                Mapper.CreateMap<SupportMng_ConstantEntry_View, DTO.ConstantEntry>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // user 2
                Mapper.CreateMap<SupportMng_User2_View, DTO.User2>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // product element
                Mapper.CreateMap<SupportMng_ProductElement_View, DTO.ProductElement>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<SupportMng_FSCType_View, DTO.FSCType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<PackagingMethodMng_PackagingMethod_View, DTO.PackagingMethod>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Factory Warehouse
                Mapper.CreateMap<SupportMng_FactoryWarehouse_View, FactoryWarehouseDto>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Production Team
                Mapper.CreateMap<SupportMng_ProductionTeam_View, ProductionTeamDto>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<SupportMng_FactoryOrderDetail_View, FactoryOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<SupportMng_SampleProduct_View, SampleProduct>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Label, o => o.ResolveUsing(s => string.IsNullOrEmpty(s.Lable) ? null : s.Lable))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<SupportMng_Model_View, Model>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<SupportMng_OPSequence_View, OPSequence>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Report template list
                Mapper.CreateMap<SupportMng_ReportTemplate_View, ReportTemplate>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // PriceDifference
                AutoMapper.Mapper.CreateMap<SupportMng_PriceDifference2_View, DTO.PriceDifference>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // AppointmentAttachedFileType
                AutoMapper.Mapper.CreateMap<SupportMng_AppointmentAttachedFileType_View, DTO.AppointmentAttachedFileType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // AppointmentAttachedFileType
                AutoMapper.Mapper.CreateMap<SupportMng_NotificationGroup_View, DTO.NotificationMember>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Production Item QuickSearch
                AutoMapper.Mapper.CreateMap<SupportMng_ProductionItem_QuickSearch_View, DTO.QuickSearchProductionItem>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Work Order is approved
                Mapper.CreateMap<SupportMng_WorkOrderApproved_View, DTO.WorkOrderApproved>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WorkOrderApprovedProductionItems, o => o.MapFrom(s => s.SupportMng_WorkOrderApprovedProductionItem_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<SupportMng_WorkOrderApprovedProductionItem_View, DTO.WorkOrderApprovedProductionItem>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Purchase order approved
                Mapper.CreateMap<SupportMng_PurchaseOrderApprove_View, DTO.PurchaseOrderApprove>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseOrderDetailApproves, o => o.MapFrom(s => s.SupportMng_PurchaseOrderDetailApprove_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<SupportMng_PurchaseOrderDetailApprove_View, DTO.PurchaseOrderDetailApprove>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductionItemUnits, o => o.MapFrom(s => s.SupportMng_PurchaseOrderDetailApproveProductionItemUnit_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<SupportMng_PurchaseOrderDetailApproveProductionItemUnit_View, DTO.PurchaseOrderDetailApproveProductionItemUnitData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<SupportMng_Country_View, DTO.Country>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<SupportMng_Model2_View, DTO.Model2>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<SupportMng_WorkOrder_View, DTO.WorkOrder>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<SupportMng_SubSupplier_View, DTO.FactoryRawMaterialData>()
                    .IgnoreAllNonExisting();

                Mapper.CreateMap<SupportMng_UserProfile_View, DTO.UserProfileData>()
                    .IgnoreAllNonExisting();

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.Client> DB2DTO_Client(List<SupportMng_Client_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Client_View>, List<DTO.Client>>(dbItems);
        }

        public List<DTO.ClientPaymentMethod> DB2DTO_ClientPaymentMethod(List<SupportMng_ClientPaymentMethod_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ClientPaymentMethod_View>, List<DTO.ClientPaymentMethod>>(dbItems);
        }

        // factory
        public List<DTO.Factory> DB2DTO_Factory(List<List_Factory> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_Factory>, List<DTO.Factory>>(dbItems);
        }
        public List<DTO.Factory> DB2DTO_AccessibleFactory(List<SupportMng_AccessibleFactoy_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_AccessibleFactoy_View>, List<DTO.Factory>>(dbItems);
        }

        // Frame Material
        public List<DTO.FrameMaterial> DB2DTO_FrameMaterial(List<List_FrameMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_FrameMaterial_View>, List<DTO.FrameMaterial>>(dbItems);
        }

        //factory material unit
        public List<DTO.Unit> DB2DTO_Unit(List<SupportMng_Unit_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Unit_View>, List<DTO.Unit>>(dbItems);
        }

        //factory material group
        public List<DTO.FactoryMaterialGroup> DB2DTO_FactoryMaterialGroup(List<SupportMng_FactoryMaterialGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryMaterialGroup_View>, List<DTO.FactoryMaterialGroup>>(dbItems);
        }

        //factory material type
        public List<DTO.FactoryMaterialType> DB2DTO_FactoryMaterialType(List<SupportMng_FactoryMaterialType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryMaterialType_View>, List<DTO.FactoryMaterialType>>(dbItems);
        }

        //factory material color
        public List<DTO.FactoryMaterialColor> DB2DTO_FactoryMaterialColor(List<SupportMng_FactoryMaterialColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryMaterialColor_View>, List<DTO.FactoryMaterialColor>>(dbItems);
        }

        //factory material
        public List<DTO.FactoryMaterial> DB2DTO_FactoryMaterial(List<SupportMng_FactoryMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryMaterial_View>, List<DTO.FactoryMaterial>>(dbItems);
        }

        //factory material image
        public List<DTO.FactoryMaterialImage> DB2DTO_FactoryMaterialImage(List<SupportMng_FactoryMaterialImage_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryMaterialImage_View>, List<DTO.FactoryMaterialImage>>(dbItems);
        }

        //factory order
        public List<DTO.FactoryOrder> DB2DTO_FactoryOrder(List<SupportMng_FactoryOrder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryOrder_View>, List<DTO.FactoryOrder>>(dbItems);
        }

        // quotation status
        public List<DTO.QuotationStatus> DB2DTO_QuotationStatus(List<SupportMng_QuotationStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_QuotationStatus_View>, List<DTO.QuotationStatus>>(dbItems);
        }

        //factory team
        public List<DTO.FactoryTeam> DB2DTO_FactoryTeam(List<SupportMng_FactoryTeam_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryTeam_View>, List<DTO.FactoryTeam>>(dbItems);
        }

        //factory area
        public List<DTO.FactoryArea> DB2DTO_FactoryArea(List<SupportMng_FactoryArea_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryArea_View>, List<DTO.FactoryArea>>(dbItems);
        }

        //factory step
        public List<DTO.FactoryStep> DB2DTO_FactoryStep(List<SupportMng_FactoryStep_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryStep_View>, List<DTO.FactoryStep>>(dbItems);
        }

        // plc image type
        public List<DTO.PLCImageType> DB2DTO_PLCImageType(List<SupportMng_PLCImageType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_PLCImageType_View>, List<DTO.PLCImageType>>(dbItems);
        }

        // container type
        public List<DTO.ContainerType> DB2DTO_ContainerType(List<SupportMng_ContainerType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ContainerType_View>, List<DTO.ContainerType>>(dbItems);
        }

        // supplier
        public List<DTO.Supplier> DB2DTO_Supplier(List<SupportMng_Supplier_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Supplier_View>, List<DTO.Supplier>>(dbItems);
        }
        public DTO.Supplier DB2DTO_Supplier(SupportMng_Supplier_View dbItem)
        {
            return AutoMapper.Mapper.Map<SupportMng_Supplier_View, DTO.Supplier>(dbItem);
        }

        // meeting location
        public List<DTO.MeetingLocation> DB2DTO_MeetingLocation(List<SupportMng_MeetingLocation_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_MeetingLocation_View>, List<DTO.MeetingLocation>>(dbItems);
        }

        // product type
        public List<DTO.ProductType> DB2DTO_ProductType(List<SupportMng_ProductType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductType_View>, List<DTO.ProductType>>(dbItems);
        }

        // product group
        public List<DTO.ProductGroup> DB2DTO_ProductGroup(List<SupportMng_ProductGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductGroup_View>, List<DTO.ProductGroup>>(dbItems);
        }

        // manufacturer country
        public List<DTO.ManufacturerCountry> DB2DTO_ManufacturerCountry(List<SupportMng_ManufacturerCountry_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ManufacturerCountry_View>, List<DTO.ManufacturerCountry>>(dbItems);
        }

        // model status
        public List<DTO.ModelStatus> DB2DTO_ModelStatus(List<SupportMng_ModelStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelStatus_View>, List<DTO.ModelStatus>>(dbItems);
        }

        // product category
        public List<DTO.ProductCategory> DB2DTO_ProductCategory(List<SupportMng_ProductCategory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductCategory_View>, List<DTO.ProductCategory>>(dbItems);
        }

        // user
        public List<DTO.User> DB2DTO_User(List<SupportMng_ActiveUser_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ActiveUser_View>, List<DTO.User>>(dbItems);
        }

        // task status
        public List<DTO.TaskStatus> DB2DTO_TaskStatus(List<SupportMng_TaskStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_TaskStatus_View>, List<DTO.TaskStatus>>(dbItems);
        }

        // POL
        public List<DTO.POL> DB2DTO_POL(List<SupportMng_POL_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_POL_View>, List<DTO.POL>>(dbItems);
        }

        // pod
        public List<DTO.POD> DB2DTO_POD(List<SupportMng_POD_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_POD_View>, List<DTO.POD>>(dbItems);
        }

        // movement
        public List<DTO.MovementTerm> DB2DTO_MovementTerm(List<SupportMng_MovementTerm_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_MovementTerm_View>, List<DTO.MovementTerm>>(dbItems);
        }

        // delivery term
        public List<DTO.DeliveryTerm> DB2DTO_DeliveryTerm(List<SupportMng_DeliveryTerm_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_DeliveryTerm_View>, List<DTO.DeliveryTerm>>(dbItems);
        }

        //ledger account
        public List<DTO.LedgerAccount> DB2DTO_LedgerAccount(List<SupportMng_LedgerAccount_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_LedgerAccount_View>, List<DTO.LedgerAccount>>(dbItems);
        }

        public List<DTO.Forwarder> DB2DTO_Forwarder(List<SupportMng_Forwarder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Forwarder_View>, List<DTO.Forwarder>>(dbItems);
        }

        //goods procedure
        public List<DTO.FactoryGoodsProcedure> DB2DTO_FactoryGoodsProcedure(List<SupportMng_FactoryGoodsProcedure_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryGoodsProcedure_View>, List<DTO.FactoryGoodsProcedure>>(dbItems);
        }

        public List<DTO.FactoryGoodsProcedureDetail> DB2DTO_FactoryGoodsProcedureDetail(List<SupportMng_FactoryGoodsProcedureDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryGoodsProcedureDetail_View>, List<DTO.FactoryGoodsProcedureDetail>>(dbItems);
        }

        //factory cost type
        public List<DTO.FactoryCostType> DB2DTO_FactoryCostType(List<SupportMng_FactoryCostType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryCostType_View>, List<DTO.FactoryCostType>>(dbItems);
        }

        //AVF Supplier
        public List<DTO.AVFSupplier> DB2DTO_AVFSupplier(List<SupportMng_AVFSupplier_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_AVFSupplier_View>, List<DTO.AVFSupplier>>(dbItems);
        }

        // Employee
        public List<DTO.Employee> DB2DTO_Employee(List<SupportMng_Employee_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Employee_View>, List<DTO.Employee>>(dbItems);
        }

        // Saler
        public List<DTO.Saler> DB2DTO_Saler(List<SupportMng_Saler_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Saler_View>, List<DTO.Saler>>(dbItems);
        }

        // delivery term
        public List<DTO.PaymentTerm> DB2DTO_PaymentTerm(List<SupportMng_PaymentTerm_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_PaymentTerm_View>, List<DTO.PaymentTerm>>(dbItems);
        }

        //factory finished product
        public List<DTO.FactoryFinishedProduct> DB2DTO_FactoryFinishedProduct(List<SupportMng_FactoryFinishedProduct_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryFinishedProduct_View>, List<DTO.FactoryFinishedProduct>>(dbItems);
        }


        //internal company
        public List<DTO.InternalCompany> DB2DTO_InternalCompany(List<SupportMng_InternalCompany_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_InternalCompany_View>, List<DTO.InternalCompany>>(dbItems);
        }

        //internal department
        public List<DTO.InternalDepartment> DB2DTO_InternalDepartment(List<SupportMng_InternalDepartment_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_InternalDepartment_View>, List<DTO.InternalDepartment>>(dbItems);
        }

        //task role
        public List<DTO.TaskRole> DB2DTO_TaskRole(List<SupportMng_TaskRole_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_TaskRole_View>, List<DTO.TaskRole>>(dbItems);
        }

        //job level
        public List<DTO.JobLevel> DB2DTO_JobLevel(List<SupportMng_JobLevel_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_JobLevel_View>, List<DTO.JobLevel>>(dbItems);
        }

        //LP status
        public List<DTO.LabelingPackagingStatus> DB2DTO_LabelingPackagingStatus(List<SupportMng_LabelingPackaging_LPStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_LabelingPackaging_LPStatus_View>, List<DTO.LabelingPackagingStatus>>(dbItems);
        }

        //Location
        public List<DTO.Location> DB2DTO_Location(List<SupportMng_Location_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Location_View>, List<DTO.Location>>(dbItems);
        }

        //Factory Location
        public List<DTO.FactoryLocation> DB2DTO_Location(List<SupportMng_FactoryLocation_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryLocation_View>, List<DTO.FactoryLocation>>(dbItems);
        }

        //leave request time 
        public List<DTO.LeaveRequestTime> DB2DTO_LeaveRequestTime(List<SupportMng_LeaveRequestTime_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_LeaveRequestTime_View>, List<DTO.LeaveRequestTime>>(dbItems);
        }

        //leave type
        public List<DTO.LeaveType> DB2DTO_LeaveType(List<SupportMng_LeaveType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_LeaveType_View>, List<DTO.LeaveType>>(dbItems);
        }

        // sample order status
        public List<DTO.SampleOrderStatus> DB2DTO_SampleOrderStatus(List<SupportMng_SampleOrderStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SampleOrderStatus_View>, List<DTO.SampleOrderStatus>>(dbItems);
        }

        // sample trasport type
        public List<DTO.SampleTransportType> DB2DTO_SampleTransportType(List<SupportMng_SampleTransportType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SampleTransportType_View>, List<DTO.SampleTransportType>>(dbItems);
        }

        // sample purpose
        public List<DTO.SamplePurpose> DB2DTO_SamplePurpose(List<SupportMng_SamplePurpose_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SamplePurpose_View>, List<DTO.SamplePurpose>>(dbItems);
        }

        // sample request type
        public List<DTO.SampleRequestType> DB2DTO_SampleRequestType(List<SupportMng_SampleRequestType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SampleRequestType_View>, List<DTO.SampleRequestType>>(dbItems);
        }

        // sample product status
        public List<DTO.SampleProductStatus> DB2DTO_SampleProductStatus(List<SupportMng_SampleProductStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SampleProductStatus_View>, List<DTO.SampleProductStatus>>(dbItems);
        }

        // QUICK SEARCH
        // model
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchModel(List<SupportMng_ModelSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelSearchResult_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        // material
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchMaterial(List<SupportMng_MaterialSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_MaterialSearchResult_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        // material type
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchMaterialType(List<SupportMng_MaterialTypeSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_MaterialTypeSearchResult_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        // material color
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchMaterialColor(List<SupportMng_MaterialColorSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_MaterialColorSearchResult_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        // frame material
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchFrameMaterial(List<SupportMng_FrameMaterialSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FrameMaterialSearchResult_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        // frame material color
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchFrameMaterialColor(List<SupportMng_FrameMaterialColorSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FrameMaterialColorSearchResult_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        // sub material
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchSubMaterial(List<SupportMng_SubMaterialSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SubMaterialSearchResult_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        // sub material color
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchSubMaterialColor(List<SupportMng_SubMaterialColorSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SubMaterialColorSearchResult_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        // cushion
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchCushion(List<SupportMng_CushionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_CushionSearchResult_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        // back cushion
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchBackCushion(List<SupportMng_BackCushionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_BackCushionSearchResult_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        // seat cushion
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchSeatCushion(List<SupportMng_SeatCushionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SeatCushionSearchResult_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        // client
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchClient(List<SupportMng_ClientSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ClientSearchResult_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        // cushion color
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchCushionColor(List<SupportMng_CushionColorSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_CushionColorSearchResult_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        // model image
        public DTO.ModelImage DB2DTO_ModelImage(SupportMng_ModelImage_View dbItems)
        {
            return AutoMapper.Mapper.Map<SupportMng_ModelImage_View, DTO.ModelImage>(dbItems);
        }

        // product info
        public DTO.ProductInfo DB2DTO_ProductInfo(SupportMng_ProductInfo_View dbItems)
        {
            return AutoMapper.Mapper.Map<SupportMng_ProductInfo_View, DTO.ProductInfo>(dbItems);
        }

        // user 2
        public List<DTO.User2> DB2DTO_User2(List<SupportMng_User2_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_User2_View>, List<DTO.User2>>(dbItems);
        }

        // product element
        public List<DTO.ProductElement> DB2DTO_ProductElement(List<SupportMng_ProductElement_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductElement_View>, List<DTO.ProductElement>>(dbItems);
        }

        // client 2 - client search result with client name
        public List<DTO.QuickSearchResult> DB2DTO_QuickSearchClient2(List<SupportMng_ClientSearchResult2_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ClientSearchResult2_View>, List<DTO.QuickSearchResult>>(dbItems);
        }

        //inspection type
        public List<DTO.InspectionType> DB2DTO_InspectionType(List<SupportMng_InspectionType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_InspectionType_View>, List<DTO.InspectionType>>(dbItems);
        }

        //packaging type
        public List<DTO.PackagingType> DB2DTO_PackagingType(List<SupportMng_PackagingType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_PackagingType_View>, List<DTO.PackagingType>>(dbItems);
        }

        // user group
        public List<DTO.UserGroup> DB2DTO_UserGroup(List<SupportMng_UserGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_UserGroup_View>, List<DTO.UserGroup>>(dbItems);
        }

        // office
        public List<DTO.Office> DB2DTO_Office(List<SupportMng_Office_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Office_View>, List<DTO.Office>>(dbItems);
        }

        // dev request history action
        public List<DTO.DevRequestHistoryAction> DB2DTO_DevRequestHistoryAction(List<SupportMng_DevRequestHistoryAction_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_DevRequestHistoryAction_View>, List<DTO.DevRequestHistoryAction>>(dbItems);
        }

        // dev request priority
        public List<DTO.DevRequestPriority> DB2DTO_DevRequestPriority(List<SupportMng_DevRequestPriority_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_DevRequestPriority_View>, List<DTO.DevRequestPriority>>(dbItems);
        }

        // dev request project
        public List<DTO.DevRequestProject> DB2DTO_DevRequestProject(List<SupportMng_DevRequestProject_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_DevRequestProject_View>, List<DTO.DevRequestProject>>(dbItems);
        }

        // dev request status
        public List<DTO.DevRequestStatus> DB2DTO_DevRequestStatus(List<SupportMng_DevRequestStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_DevRequestStatus_View>, List<DTO.DevRequestStatus>>(dbItems);
        }

        // dev request type
        public List<DTO.DevRequestType> DB2DTO_DevRequestType(List<SupportMng_DevRequestType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_DevRequestType_View>, List<DTO.DevRequestType>>(dbItems);
        }

        public List<DTO.TypeOfCost> DB2DTO_TypeOfCost(List<SupportMng_TypeOfCost_View> dbItems)
        {
            return Mapper.Map<List<SupportMng_TypeOfCost_View>, List<DTO.TypeOfCost>>(dbItems);
        }

        public List<DTO.TypeOfCharge> DB2DTO_TypeOfCharge(List<SupportMng_TypeOfCharge_View> dbItems)
        {
            return Mapper.Map<List<SupportMng_TypeOfCharge_View>, List<DTO.TypeOfCharge>>(dbItems);
        }

        public List<DTO.TypeOfCurrency> DB2DTO_TypeOfCurrency(List<SupportMng_TypeOfCurrency_View> dbItems)
        {
            return Mapper.Map<List<SupportMng_TypeOfCurrency_View>, List<DTO.TypeOfCurrency>>(dbItems);
        }

        public List<DTO.ConstantEntry> DB2DTO_ConstantEntry(List<SupportMng_ConstantEntry_View> dbItems)
        {
            return Mapper.Map<List<SupportMng_ConstantEntry_View>, List<DTO.ConstantEntry>>(dbItems);
        }

        public List<DTO.FSCType> DB2DTO_FSCType(List<SupportMng_FSCType_View> dbItems)
        {
            return Mapper.Map<List<SupportMng_FSCType_View>, List<DTO.FSCType>>(dbItems);
        }

        public List<DTO.PackagingMethod> DB2DTO_PackagingMethod(List<PackagingMethodMng_PackagingMethod_View> dbItems)
        {
            return Mapper.Map<List<PackagingMethodMng_PackagingMethod_View>, List<DTO.PackagingMethod>>(dbItems);
        }

        // Factory warehouse
        public List<FactoryWarehouseDto> DB2DTO_FactoryWarehouse(List<SupportMng_FactoryWarehouse_View> list)
        {
            return Mapper.Map<List<SupportMng_FactoryWarehouse_View>, List<FactoryWarehouseDto>>(list);
        }

        // Production team
        public List<ProductionTeamDto> DB2DTO_ProductionTeam(List<SupportMng_ProductionTeam_View> list)
        {
            return Mapper.Map<List<SupportMng_ProductionTeam_View>, List<ProductionTeamDto>>(list);
        }

        // Report template list
        public List<ReportTemplate> DB2DTO_ReportTemplate(List<SupportMng_ReportTemplate_View> list)
        {
            return Mapper.Map<List<SupportMng_ReportTemplate_View>, List<ReportTemplate>>(list);
        }

        // PriceDifference
        public List<DTO.PriceDifference> DB2DTO_PriceDifference(List<SupportMng_PriceDifference2_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_PriceDifference2_View>, List<DTO.PriceDifference>>(dbItem);
        }

        public List<DTO.QuickSearchResult2> DB2DTO_Client2(List<SupportMng_Client2_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Client2_View>, List<DTO.QuickSearchResult2>>(dbItem);
        }

        public List<DTO.QuickSearchResult2> DB2DTO_Factory2(List<SupportMng_Factory2_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Factory2_View>, List<DTO.QuickSearchResult2>>(dbItem);
        }

        public List<DTO.AppointmentAttachedFileType> DB2DTO_AppointmentAttachedFileType(List<SupportMng_AppointmentAttachedFileType_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_AppointmentAttachedFileType_View>, List<DTO.AppointmentAttachedFileType>>(dbItem);
        }

        public List<DTO.NotificationMember> DB2DTO_NotificationMember(List<SupportMng_NotificationGroup_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_NotificationGroup_View>, List<DTO.NotificationMember>>(dbItem);
        }

        public List<DTO.QuickSearchProductionItem> DB2DTO_QuickSearchProductionItem(List<SupportMng_ProductionItem_QuickSearch_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductionItem_QuickSearch_View>, List<DTO.QuickSearchProductionItem>>(dbItem);
        }

        public List<DTO.WorkOrderApproved> DB2DTO_WorkOrderApproved(List<SupportMng_WorkOrderApproved_View> dbItem)
        {
            return Mapper.Map<List<SupportMng_WorkOrderApproved_View>, List<DTO.WorkOrderApproved>>(dbItem);
        }

        public List<DTO.PurchaseOrderApprove> DB2DTO_PurchaseOrderApprove(List<SupportMng_PurchaseOrderApprove_View> dbItem)
        {
            return Mapper.Map<List<SupportMng_PurchaseOrderApprove_View>, List<DTO.PurchaseOrderApprove>>(dbItem);
        }

        public List<DTO.Country> DB2DTO_Country(List<SupportMng_Country_View> dbItem)
        {
            return Mapper.Map<List<SupportMng_Country_View>, List<DTO.Country>>(dbItem);
        }

        public List<DTO.Model2> DB2DTO_Model2(List<SupportMng_Model2_View> dbItem)
        {
            return Mapper.Map<List<SupportMng_Model2_View>, List<DTO.Model2>>(dbItem);
        }

        public List<DTO.WorkOrder> DB2DTO_WorkOrder(List<SupportMng_WorkOrder_View> dbItem)
        {
            return Mapper.Map<List<SupportMng_WorkOrder_View>, List<DTO.WorkOrder>>(dbItem);
        }

        public List<DTO.UserProfileData> DB2DTO_UserProfile(List<SupportMng_UserProfile_View> dbItem)
        {
            return Mapper.Map<List<SupportMng_UserProfile_View>, List<DTO.UserProfileData>>(dbItem);
        }
        public List<DTO.EmployeeDepartmentDTO> DB2DTO_EmployeeDeparment(List<AgendaMng_Employee_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AgendaMng_Employee_View>, List<DTO.EmployeeDepartmentDTO>>(dbItems);
        }

    }
}
