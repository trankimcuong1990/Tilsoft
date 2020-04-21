using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.FactoryOrderMng
{
    internal class DataConverter
    {
        private const string DateTimeFormat = "dd/MM/yyyy";
        public DataConverter()
        {
            string mapName = "FactoryOrderMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryOrderMng_SaleOrderSearch_View, DTO.FactoryOrderMng.SaleOrderSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_SaleOrderDetail_View, DTO.FactoryOrderMng.FactoryOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_SaleOrderDetailSparepart_View, DTO.FactoryOrderMng.FactoryOrderSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_SaleOrderDetailSample_View, DTO.FactoryOrderMng.FactoryOrderSampleDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_FactoryOrderSearch_View, DTO.FactoryOrderMng.FactoryOrderSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_FactoryOrderDetailSearch_View, DTO.FactoryOrderMng.FactoryOrderDetailSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_FactoryOrderDetail_View, DTO.FactoryOrderMng.FactoryOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryOrderColliDetails, o => o.MapFrom(s => s.FactoryOrderMng_FactoryOrderColliDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_FactoryOrderSparepartDetail_View, DTO.FactoryOrderMng.FactoryOrderSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_FactoryOrderSampleDetail_View, DTO.FactoryOrderMng.FactoryOrderSampleDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_FactoryOrderColliDetail_View, DTO.FactoryOrderMng.FactoryOrderColliDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_ProductColli_View, DTO.FactoryOrderMng.ProductColli>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_FactoryOrder_View, DTO.FactoryOrderMng.FactoryOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryOrderDetails, o => o.MapFrom(s => s.FactoryOrderMng_FactoryOrderDetail_View))
                    .ForMember(d => d.FactoryOrderSparepartDetails, o => o.MapFrom(s => s.FactoryOrderMng_FactoryOrderSparepartDetail_View))
                    .ForMember(d => d.FactoryOrderSampleDetails, o => o.MapFrom(s => s.FactoryOrderMng_FactoryOrderSampleDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.FactoryOrderMng.FactoryOrder, FactoryOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryOrderID, o => o.Ignore())
                    .ForMember(d => d.FactoryOrderDetail, o => o.Ignore())
                    .ForMember(d => d.FactoryOrderSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.FactoryOrderHistory, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.FactoryOrderMng.FactoryOrderDetail, FactoryOrderDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.FactoryOrderDetailID, o => o.Ignore())
                     ;

                AutoMapper.Mapper.CreateMap<DTO.FactoryOrderMng.FactoryOrderSparepartDetail, FactoryOrderSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryOrderSparepartDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.FactoryOrderMng.FactoryOrderSampleDetail, FactoryOrderSampleDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryOrderSampleDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.FactoryOrderMng.FactoryOrder, FactoryOrderHistory>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.FactoryOrderHistoryID, o => o.Ignore())
                     .ForMember(d => d.FactoryOrderHistoryDetail, o => o.Ignore())
                     ;

                AutoMapper.Mapper.CreateMap<DTO.FactoryOrderMng.FactoryOrderDetail, FactoryOrderHistoryDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.FactoryOrderHistoryDetailID, o => o.Ignore())
                     ;

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_QCReport_View, DTO.FactoryOrderMng.QCReport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ThumbnailLocation) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ThumbnailLocation)))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.FileLocation) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.FileLocation)))
                    ;

                //FactoryOrderOverView
                AutoMapper.Mapper.CreateMap<FactoryOrderMng_FactoryOrderDetail_OverView, DTO.FactoryOrderMng.FactoryOrderDetailOverView>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryOrderColliDetails, o => o.MapFrom(s => s.FactoryOrderMng_FactoryOrderColliDetail_OverView))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_FactoryOrderSparepartDetail_OverView, DTO.FactoryOrderMng.FactoryOrderSparepartDetailOverView>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_FactoryOrderColliDetail_OverView, DTO.FactoryOrderMng.FactoryOrderColliDetailOverView>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_FactoryOrder_OverView, DTO.FactoryOrderMng.FactoryOrderOverView>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.FactoryOrderDetails, o => o.MapFrom(s => s.FactoryOrderMng_FactoryOrderDetail_OverView))
                  .ForMember(d => d.FactoryOrderSparepartDetails, o => o.MapFrom(s => s.FactoryOrderMng_FactoryOrderSparepartDetail_OverView))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_EmailNotificationCofirmed_View, DTO.FactoryOrderMng.EmailNotificationConfirmed>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_GeneralBreakDown_OverView, DTO.FactoryOrderMng.GeneralBreakDownDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderMng_GetFactories_View, DTO.FactoryOrderMng.FactoryListByStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientComplaint_ClientComplaint_View, DTO.FactoryOrderMng.ClientComplaint_View>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ReceivedDate, o => o.ResolveUsing(s => s.ReceivedDate.HasValue ? s.ReceivedDate.Value.ToString(DateTimeFormat) : ""))
                   .ForMember(d => d.ClientComplaintItems, o => o.MapFrom(s => s.ClientComplaint_ClientComplaintItem_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientComplaint_ClientComplaintItem_View, DTO.FactoryOrderMng.ClientComplaintItem_View>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ConstantEntry_View, DTO.FactoryOrderMng.ConstantEntry>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryOrderMng.FactoryOrderDetail> DB2DTO_FactoryOrderDetail(List<FactoryOrderMng_SaleOrderDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderMng_SaleOrderDetail_View>, List<DTO.FactoryOrderMng.FactoryOrderDetail>>(dbItems);
        }
        public List<DTO.FactoryOrderMng.FactoryOrderSparepartDetail> DB2DTO_FactoryOrderSparepartDetail(List<FactoryOrderMng_SaleOrderDetailSparepart_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderMng_SaleOrderDetailSparepart_View>, List<DTO.FactoryOrderMng.FactoryOrderSparepartDetail>>(dbItems);
        }
        
        public List<DTO.FactoryOrderMng.FactoryOrderSampleDetail> DB2DTO_FactoryOrderSampleDetail(List<FactoryOrderMng_SaleOrderDetailSample_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderMng_SaleOrderDetailSample_View>, List<DTO.FactoryOrderMng.FactoryOrderSampleDetail>>(dbItems);
        }

        public List<DTO.FactoryOrderMng.SaleOrderSearch> DB2DTO_SaleOrderSearch(List<FactoryOrderMng_SaleOrderSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderMng_SaleOrderSearch_View>, List<DTO.FactoryOrderMng.SaleOrderSearch>>(dbItems);
        }

        public List<DTO.FactoryOrderMng.FactoryListByStatus> DB2DTO_GetFacctories(List<FactoryOrderMng_GetFactories_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderMng_GetFactories_View>, List<DTO.FactoryOrderMng.FactoryListByStatus>>(dbItems);
        }

        public List<DTO.FactoryOrderMng.FactoryOrderSearch> DB2DTO_FactoryOrderSearch(List<FactoryOrderMng_FactoryOrderSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderMng_FactoryOrderSearch_View>, List<DTO.FactoryOrderMng.FactoryOrderSearch>>(dbItems);
        }

        public List<DTO.FactoryOrderMng.FactoryOrderDetailSearch> DB2DTO_FactoryOrderDetailSearch(List<FactoryOrderMng_FactoryOrderDetailSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderMng_FactoryOrderDetailSearch_View>, List<DTO.FactoryOrderMng.FactoryOrderDetailSearch>>(dbItems);
        }

        public DTO.FactoryOrderMng.FactoryOrder DB2DTO_FactoryOrder(FactoryOrderMng_FactoryOrder_View dbItem)
        {
            DTO.FactoryOrderMng.FactoryOrder dtoItem = AutoMapper.Mapper.Map<FactoryOrderMng_FactoryOrder_View, DTO.FactoryOrderMng.FactoryOrder>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.ConcurrencyFlag != null)
                dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dbItem.ConcurrencyFlag);

            if (dbItem.OrderDate.HasValue)
                dtoItem.OrderDateFormated = dbItem.OrderDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.LDS1.HasValue)
                dtoItem.LDS1Formated = dbItem.LDS1.Value.ToString("dd/MM/yyyy");

            if (dbItem.LDS2.HasValue)
                dtoItem.LDS2Formated = dbItem.LDS2.Value.ToString("dd/MM/yyyy");

            if (dbItem.LDS3.HasValue)
                dtoItem.LDS3Formated = dbItem.LDS3.Value.ToString("dd/MM/yyyy");

            if (dbItem.LDS4.HasValue)
                dtoItem.LDS4Formated = dbItem.LDS4.Value.ToString("dd/MM/yyyy");

            if (dbItem.PSReceivedDate.HasValue)
                dtoItem.PSReceivedDateFormated = dbItem.PSReceivedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.PIReceivedDate.HasValue)
                dtoItem.PIReceivedDateFormated = dbItem.PIReceivedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dbItem.CreatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            return dtoItem;
        }

        public void DTO2DB_FactoryOrder(DTO.FactoryOrderMng.FactoryOrder dtoItem, ref FactoryOrder dbItem)
        {
            /*
             * MAP & CHECK FactoryOrderDetail
             */
            List<FactoryOrderDetail> orderdetail_tobedeleted = new List<FactoryOrderDetail>();
            if (dtoItem.FactoryOrderDetails != null)
            {
                //CHECK
                foreach (FactoryOrderDetail dbDetail in dbItem.FactoryOrderDetail.Where(o => !dtoItem.FactoryOrderDetails.Select(s => s.FactoryOrderDetailID).Contains(o.FactoryOrderDetailID)))
                {
                    orderdetail_tobedeleted.Add(dbDetail);
                }
                foreach (FactoryOrderDetail dbDetail in orderdetail_tobedeleted)
                {
                    dbItem.FactoryOrderDetail.Remove(dbDetail);
                }
                //MAP
                foreach (DTO.FactoryOrderMng.FactoryOrderDetail dtoDetail in dtoItem.FactoryOrderDetails)
                {
                    FactoryOrderDetail dbDetail;
                    if (dtoDetail.FactoryOrderDetailID < 0)
                    {
                        dbDetail = new FactoryOrderDetail();
                        dbItem.FactoryOrderDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryOrderDetail.FirstOrDefault(o => o.FactoryOrderDetailID == dtoDetail.FactoryOrderDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryOrderMng.FactoryOrderDetail, FactoryOrderDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            /*
            * MAP & CHECK FactoryOrderSparepartDetail
            */
            List<FactoryOrderSparepartDetail> ordersparepartdetail_tobedeleted = new List<FactoryOrderSparepartDetail>();
            if (dtoItem.FactoryOrderSparepartDetails != null)
            {
                //CHECK
                foreach (var dbDetail in dbItem.FactoryOrderSparepartDetail.Where(o => !dtoItem.FactoryOrderSparepartDetails.Select(s => s.FactoryOrderSparepartDetailID).Contains(o.FactoryOrderSparepartDetailID)))
                {
                    ordersparepartdetail_tobedeleted.Add(dbDetail);
                }
                foreach (var dbDetail in ordersparepartdetail_tobedeleted)
                {
                    dbItem.FactoryOrderSparepartDetail.Remove(dbDetail);
                }
                //MAP
                foreach (var dtoDetail in dtoItem.FactoryOrderSparepartDetails)
                {
                    FactoryOrderSparepartDetail dbDetail;
                    if (dtoDetail.FactoryOrderSparepartDetailID < 0)
                    {
                        dbDetail = new FactoryOrderSparepartDetail();
                        dbItem.FactoryOrderSparepartDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryOrderSparepartDetail.FirstOrDefault(o => o.FactoryOrderSparepartDetailID == dtoDetail.FactoryOrderSparepartDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryOrderMng.FactoryOrderSparepartDetail, FactoryOrderSparepartDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            /*
            * MAP & CHECK FactoryOrderSampleDetail
            */
            List<FactoryOrderSampleDetail> ordersampedetail_tobedeleted = new List<FactoryOrderSampleDetail>();
            if (dtoItem.FactoryOrderSampleDetails != null)
            {
                //CHECK
                foreach (var dbDetail in dbItem.FactoryOrderSampleDetail.Where(o => !dtoItem.FactoryOrderSampleDetails.Select(s => s.FactoryOrderSampleDetailID).Contains(o.FactoryOrderSampleDetailID)))
                {
                    ordersampedetail_tobedeleted.Add(dbDetail);
                }
                foreach (var dbDetail in ordersampedetail_tobedeleted)
                {
                    dbItem.FactoryOrderSampleDetail.Remove(dbDetail);
                }
                //MAP
                foreach (var dtoDetail in dtoItem.FactoryOrderSampleDetails)
                {
                    FactoryOrderSampleDetail dbDetail;
                    if (dtoDetail.FactoryOrderSampleDetailID < 0)
                    {
                        dbDetail = new FactoryOrderSampleDetail();
                        dbItem.FactoryOrderSampleDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryOrderSampleDetail.FirstOrDefault(o => o.FactoryOrderSampleDetailID == dtoDetail.FactoryOrderSampleDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryOrderMng.FactoryOrderSampleDetail, FactoryOrderSampleDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            /*
             * MAP FactoryOrder History
             */
            FactoryOrderHistory dbHistory = new FactoryOrderHistory();
            FactoryOrderHistoryDetail dbHistoryDetail;
            foreach (DTO.FactoryOrderMng.FactoryOrderDetail dtoDetail in dtoItem.FactoryOrderDetails)
            {
                dbHistoryDetail = new FactoryOrderHistoryDetail();
                AutoMapper.Mapper.Map<DTO.FactoryOrderMng.FactoryOrderDetail, FactoryOrderHistoryDetail>(dtoDetail, dbHistoryDetail);
                dbHistory.FactoryOrderHistoryDetail.Add(dbHistoryDetail);
            }
            AutoMapper.Mapper.Map<DTO.FactoryOrderMng.FactoryOrder, FactoryOrderHistory>(dtoItem, dbHistory);
            dbItem.FactoryOrderHistory.Add(dbHistory);

            /*
             * SETUP FORMATED FIELD
             */
            if (!string.IsNullOrEmpty(dtoItem.OrderDateFormated))
            {
                dtoItem.OrderDate = DateTime.ParseExact(dtoItem.OrderDateFormated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }
            else
            {
                dtoItem.OrderDate = null;
            }

            if (!string.IsNullOrEmpty(dtoItem.LDS1Formated))
            {
                dtoItem.LDS1 = DateTime.ParseExact(dtoItem.LDS1Formated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }
            else
            {
                dtoItem.LDS1 = null;
            }

            if (!string.IsNullOrEmpty(dtoItem.LDS2Formated))
            {
                dtoItem.LDS2 = DateTime.ParseExact(dtoItem.LDS2Formated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }
            else
            {
                dtoItem.LDS2 = null;
            }

            if (!string.IsNullOrEmpty(dtoItem.LDS3Formated))
            {
                dtoItem.LDS3 = DateTime.ParseExact(dtoItem.LDS3Formated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }
            else
            {
                dtoItem.LDS3 = null;
            }

            if (!string.IsNullOrEmpty(dtoItem.LDS4Formated))
            {
                dtoItem.LDS4 = DateTime.ParseExact(dtoItem.LDS4Formated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }
            else
            {
                dtoItem.LDS4 = null;
            }

            if (!string.IsNullOrEmpty(dtoItem.PSReceivedDateFormated))
            {
                dtoItem.PSReceivedDate = DateTime.ParseExact(dtoItem.PSReceivedDateFormated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }
            else
            {
                dtoItem.PSReceivedDate = null;
            }

            if (!string.IsNullOrEmpty(dtoItem.PIReceivedDateFormated))
            {
                dtoItem.PIReceivedDate = DateTime.ParseExact(dtoItem.PIReceivedDateFormated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }
            else
            {
                dtoItem.PIReceivedDate = null;
            }

            AutoMapper.Mapper.Map<DTO.FactoryOrderMng.FactoryOrder, FactoryOrder>(dtoItem, dbItem);
        }

        public List<DTO.FactoryOrderMng.QCReport> DB2DTO_QCReport(List<FactoryOrderMng_QCReport_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderMng_QCReport_View>, List<DTO.FactoryOrderMng.QCReport>>(dbItems);
        }

        public List<DTO.FactoryOrderMng.ProductColli> DB2DTO_ProductColli(List<FactoryOrderMng_ProductColli_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderMng_ProductColli_View>, List<DTO.FactoryOrderMng.ProductColli>>(dbItems);
        }

        public DTO.FactoryOrderMng.FactoryOrderOverView DB2DTO_FactoryOrderOverView(FactoryOrderMng_FactoryOrder_OverView dbItem)
        {
            DTO.FactoryOrderMng.FactoryOrderOverView dtoItem = AutoMapper.Mapper.Map<FactoryOrderMng_FactoryOrder_OverView, DTO.FactoryOrderMng.FactoryOrderOverView>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.ConcurrencyFlag != null)
                dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dbItem.ConcurrencyFlag);

            if (dbItem.OrderDate.HasValue)
                dtoItem.OrderDateFormated = dbItem.OrderDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.LDS1.HasValue)
                dtoItem.LDS1Formated = dbItem.LDS1.Value.ToString("dd/MM/yyyy");

            if (dbItem.LDS2.HasValue)
                dtoItem.LDS2Formated = dbItem.LDS2.Value.ToString("dd/MM/yyyy");

            if (dbItem.LDS3.HasValue)
                dtoItem.LDS3Formated = dbItem.LDS3.Value.ToString("dd/MM/yyyy");

            if (dbItem.LDS4.HasValue)
                dtoItem.LDS4Formated = dbItem.LDS4.Value.ToString("dd/MM/yyyy");

            if (dbItem.PSReceivedDate.HasValue)
                dtoItem.PSReceivedDateFormated = dbItem.PSReceivedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.PIReceivedDate.HasValue)
                dtoItem.PIReceivedDateFormated = dbItem.PIReceivedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dbItem.CreatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            return dtoItem;
        }
        public List<DTO.FactoryOrderMng.EmailNotificationConfirmed> DB2DTO_EmailNotificationConfirmed(List<FactoryOrderMng_EmailNotificationCofirmed_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderMng_EmailNotificationCofirmed_View>, List<DTO.FactoryOrderMng.EmailNotificationConfirmed>>(dbItem);
        }

        public List<DTO.FactoryOrderMng.GeneralBreakDownDTO> DB2DTO_GeneralBreakDown(List<FactoryOrderMng_GeneralBreakDown_OverView> dbItem)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderMng_GeneralBreakDown_OverView>, List<DTO.FactoryOrderMng.GeneralBreakDownDTO>>(dbItem);
        }

        public DTO.FactoryOrderMng.ClientComplaint_View DB2DTO_ClientComplaintView(ClientComplaint_ClientComplaint_View dbItem)
        {
            return AutoMapper.Mapper.Map<ClientComplaint_ClientComplaint_View, DTO.FactoryOrderMng.ClientComplaint_View>(dbItem);
        }

        public List<DTO.FactoryOrderMng.ConstantEntry> DB2DTO_ConstantEntry(List<SupportMng_ConstantEntry_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ConstantEntry_View>, List<DTO.FactoryOrderMng.ConstantEntry>>(dbItem);
        }
    }
}
