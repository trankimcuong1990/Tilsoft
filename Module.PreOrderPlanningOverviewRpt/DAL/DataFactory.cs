using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PreOrderPlanningOverviewRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private PreOrderPlanningOverviewRptEntities CreateContext()
        {
            return new PreOrderPlanningOverviewRptEntities(Library.Helper.CreateEntityConnectionString("DAL.PreOrderPlanningOverviewRptModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //DTO.SearchFormData data = new DTO.SearchFormData();
            //data.Data = new List<DTO.SampleOrderSearchResult>();
            //totalRows = 0;

            //string SampleOrderUD = null;
            //string Season = null;
            //string ClientUD = null;
            //string ClientNM = null;
            //int? PurposeID = null;
            //int? TransportTypeID = null;
            //int? SampleOrderStatusID = null;

            //if (filters.ContainsKey("SampleOrderUD") && !string.IsNullOrEmpty(filters["SampleOrderUD"].ToString()))
            //{
            //    SampleOrderUD = filters["SampleOrderUD"].ToString().Replace("'", "''");
            //}
            //if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            //{
            //    Season = filters["Season"].ToString().Replace("'", "''");
            //}
            //if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            //{
            //    ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
            //}
            //if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
            //{
            //    ClientNM = filters["ClientNM"].ToString().Replace("'", "''");
            //}
            //if (filters.ContainsKey("PurposeID") && filters["PurposeID"] != null && !string.IsNullOrEmpty(filters["PurposeID"].ToString()))
            //{
            //    PurposeID = Convert.ToInt32(filters["PurposeID"].ToString());
            //}
            //if (filters.ContainsKey("TransportTypeID") && filters["TransportTypeID"] != null && !string.IsNullOrEmpty(filters["TransportTypeID"].ToString()))
            //{
            //    TransportTypeID = Convert.ToInt32(filters["TransportTypeID"].ToString());
            //}
            //if (filters.ContainsKey("SampleOrderStatusID") && filters["SampleOrderStatusID"] != null && !string.IsNullOrEmpty(filters["SampleOrderStatusID"].ToString()))
            //{
            //    SampleOrderStatusID = Convert.ToInt32(filters["SampleOrderStatusID"].ToString());
            //}
            ////try to get data
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {

            //        totalRows = context.Sample2Mng_function_SearchSampleOrder(SampleOrderUD, Season, ClientUD, ClientNM, PurposeID, TransportTypeID, SampleOrderStatusID, orderBy, orderDirection).Count();
            //        var result = context.Sample2Mng_function_SearchSampleOrder(SampleOrderUD, Season, ClientUD, ClientNM, PurposeID, TransportTypeID, SampleOrderStatusID, orderBy, orderDirection);
            //        data.Data = converter.DB2DTO_SampleOrderSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //}

            //return data;
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //DTO.EditFormData data = new DTO.EditFormData();
            //data.Data = new DTO.SampleOrder();
            //data.Data.SampleOrderStatusID = 1;
            //data.Data.SampleOrderStatusNM = "PENDING";
            //data.Data.SampleProducts = new List<DTO.SampleProduct>();
            //data.Data.SampleMonitors = new List<DTO.SampleMonitor>();

            //data.Seasons = new List<Support.DTO.Season>();
            //data.SamplePurposes = new List<Support.DTO.SamplePurpose>();
            //data.SampleTransportTypes = new List<Support.DTO.SampleTransportType>();
            //data.Users = new List<Support.DTO.User>();
            //data.Factories = new List<Support.DTO.Factory>();
            //data.SampleRequestTypes = new List<Support.DTO.SampleRequestType>();
            //data.SampleProductStatuses = new List<Support.DTO.SampleProductStatus>();
            //data.SampleOrderStatuses = new List<Support.DTO.SampleOrderStatus>();


            ////try to get data
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        if (id > 0)
            //        {
            //            data.Data = converter.DB2DTO_SampleOrder(context.Sample2Mng_SampleOrder_View
            //                .Include("Sample2Mng_SampleProduct_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleItemLocation_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleReferenceImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View.Sample2Mng_SampleProductMinuteImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View.Sample2Mng_SampleRemarkImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View.Sample2Mng_SampleQARemarkImage_View")
            //                .Include("Sample2Mng_SampleMonitor_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductSubFactory_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View.Sample2Mng_SampleProgressImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View.Sample2Mng_SampleTechnicalDrawingFile_View")
            //                .FirstOrDefault(o => o.SampleOrderID == id));
            //        }

            //        data.Seasons = supportFactory.GetSeason().ToList();
            //        data.SamplePurposes = supportFactory.GetSamplePurpose().ToList();
            //        data.SampleTransportTypes = supportFactory.GetSampleTransportType().ToList();
            //        data.Users = supportFactory.GetUsers().ToList();
            //        data.Factories = supportFactory.GetFactory().ToList();
            //        data.SampleRequestTypes = supportFactory.GetSampleRequestType().ToList();
            //        data.SampleProductStatuses = supportFactory.GetSampleProductStatus().ToList();
            //        data.SampleProductStatuses.Remove(data.SampleProductStatuses.FirstOrDefault(o => o.SampleProductStatusID == 4)); // remove finished status
            //        data.SampleOrderStatuses = supportFactory.GetSampleOrderStatus().ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //}

            //return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            context.SampleTechnicalDrawing.Remove(dbItem);
            //            // also remove all child item if needed
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //DTO.SampleOrder dtoOrder = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleOrder>();
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleOrder dbItem = null;
            //        if (id == 0)
            //        {
            //            dbItem = new SampleOrder();
            //            context.SampleOrder.Add(dbItem);
            //            dbItem.CreatedBy = userId;
            //            dbItem.CreatedDate = DateTime.Now;
            //        }
            //        else
            //        {
            //            dbItem = context.SampleOrder.FirstOrDefault(o => o.SampleOrderID == id);
            //        }

            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Order not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.UpdatedBy = userId;
            //            dbItem.UpdatedDate = DateTime.Now;
            //            converter.DTO2DB_SampleOrder(dtoOrder, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
            //            context.SampleProductMinuteImage.Local.Where(o => o.SampleProductMinute == null).ToList().ForEach(o => context.SampleProductMinuteImage.Remove(o));
            //            context.SampleProductMinute.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProductMinute.Remove(o));
            //            context.SampleRemarkImage.Local.Where(o => o.SampleRemark == null).ToList().ForEach(o => context.SampleRemarkImage.Remove(o));
            //            context.SampleRemark.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleRemark.Remove(o));
            //            context.SampleTechnicalDrawingFile.Local.Where(o => o.SampleTechnicalDrawing == null).ToList().ForEach(o => context.SampleTechnicalDrawingFile.Remove(o));
            //            context.SampleTechnicalDrawing.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleTechnicalDrawing.Remove(o));
            //            context.SampleProgressImage.Local.Where(o => o.SampleProgress == null).ToList().ForEach(o => context.SampleProgressImage.Remove(o));
            //            context.SampleProgress.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProgress.Remove(o));
            //            context.SampleReferenceImage.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleReferenceImage.Remove(o));
            //            context.SampleProduct.Local.Where(o => o.SampleOrder == null).ToList().ForEach(o => context.SampleProduct.Remove(o));
            //            context.SampleMonitor.Local.Where(o => o.SampleOrder == null).ToList().ForEach(o => context.SampleMonitor.Remove(o));
            //            context.SaveChanges();

            //            // generate order number
            //            if (id <= 0)
            //            {
            //                using (DbContextTransaction scope = context.Database.BeginTransaction())
            //                {
            //                    context.Database.ExecuteSqlCommand("SELECT * FROM SampleOrder WITH (TABLOCKX, HOLDLOCK)");

            //                    try
            //                    {
            //                        context.SaveChanges();
            //                        dbItem.SampleOrderUD = Library.Common.Helper.formatIndex(dbItem.SampleOrderID.ToString(), 8, "0");
            //                        context.SaveChanges();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        throw ex;
            //                    }
            //                    finally
            //                    {
            //                        scope.Commit();
            //                    }
            //                }
            //            }
            //            dtoItem = GetData(dbItem.SampleOrderID, out notification).Data;
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = true;
            //            dbItem.ConfirmedBy = userId;
            //            dbItem.ConfirmedDate = DateTime.Now;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = false;
            //            dbItem.ConfirmedBy = null;
            //            dbItem.ConfirmedDate = null;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        //
        // CUSTOM FUNCTION HERE
        //

        public DTO.SupportFormData GetInitData(out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            //DTO.SupportFormData data = new DTO.SupportFormData();
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = Library.Helper.GetInnerException(ex).Message;
            //}
            //return data;
        }

        public DTO.ReportFormData GetHTMLReport(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };

            string season = "2018/2019";
            DateTime tetDate = new DateTime(2019, 2, 5);
            DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            while ((int)fromDate.DayOfWeek != 1)
            {
                fromDate = fromDate.AddDays(-1);
            }

            DateTime toDate = new DateTime(2019, 5, 20);
            List<int> wIDs = new List<int>();

            DTO.ReportFormData data = new DTO.ReportFormData();            
            data.WeekInfoDTOs = new List<DTO.WeekInfoDTO>();            
            data.ReportDetailDTOs = new List<DTO.ReportDetailDTO>();
            data.SumDataRow = new DTO.ReportDetailDTO {
                CapacityB = 0,
                CapacityA = 0,
                OrderB = 0,
                OrderA = 0,
                PreOrderB = 0,
                PreOrderA = 0,
                PreProducedB = 0,
                PreProducedA = 0
            };
            try
            {
                using (PreOrderPlanningOverviewRptEntities context = CreateContext())
                {
                    //context.Database.CommandTimeout = 3 * 60;
                    data.WeekInfoDTOs = converter.DB2DTO_WeeklyInfo(context.WeekInfo.Where(o => o.WeekStart <= toDate && o.WeekStart >= fromDate).OrderBy(o=>o.WeekStart).ToList());
                    data.TetWeekInfoID = context.WeekInfo.FirstOrDefault(o => o.WeekStart <= tetDate && o.WeekEnd >= tetDate).WeekInfoID;
                    var dbItems = context.PreOrderPlanningOverviewRpt_function_GetWeeklyOverview(fromDate, toDate).ToList();
                    data.TotalPreOrder = context.FactoryPlanningSetting.FirstOrDefault(o => o.Season == season).TotalPreOrder.Value;

                    //
                    // processing manually - bad practice
                    //
                    foreach (var wInfo in data.WeekInfoDTOs)
                    {
                        decimal capacity, order, preorder, preproduced;
                        capacity = order = preorder = preproduced = 0;
                        foreach (var detail in dbItems.Where(o=>o.WeekInfoID == wInfo.WeekInfoID))
                        {
                            if (detail.WeekStart >= tetDate)
                            {
                                data.SumDataRow.CapacityA += detail.Capacity.Value;
                                data.SumDataRow.OrderA += detail.TotalOrderQnt.Value;
                                data.SumDataRow.PreOrderA += detail.PreOrderQnt.Value;
                                data.SumDataRow.PreProducedA += detail.PreProducedQnt.Value;
                            }
                            else
                            {
                                data.SumDataRow.CapacityB += detail.Capacity.Value;
                                data.SumDataRow.OrderB += detail.TotalOrderQnt.Value;
                                data.SumDataRow.PreOrderB += detail.PreOrderQnt.Value;
                                data.SumDataRow.PreProducedB += detail.PreProducedQnt.Value;
                            }
                            capacity += detail.Capacity.Value;
                            order += detail.TotalOrderQnt.Value;
                            preorder += detail.PreOrderQnt.Value;
                            preproduced += detail.PreProducedQnt.Value;
                        }

                        data.SumDataRow.ReportWeekDetailDTOs.Add(new DTO.ReportWeekDetailDTO {
                            WeekInfoID = wInfo.WeekInfoID,
                            Color = "#000",
                            Value = capacity
                        });
                        data.SumDataRow.ReportWeekDetailDTOs.Add(new DTO.ReportWeekDetailDTO
                        {
                            WeekInfoID = wInfo.WeekInfoID,
                            Color = "#000",
                            Value = order
                        });
                        data.SumDataRow.ReportWeekDetailDTOs.Add(new DTO.ReportWeekDetailDTO
                        {
                            WeekInfoID = wInfo.WeekInfoID,
                            Color = "#000",
                            Value = preorder
                        });
                        data.SumDataRow.ReportWeekDetailDTOs.Add(new DTO.ReportWeekDetailDTO
                        {
                            WeekInfoID = wInfo.WeekInfoID,
                            Color = "#000",
                            Value = preproduced
                        });
                        data.SumDataRow.ReportWeekDetailDTOs.Add(new DTO.ReportWeekDetailDTO
                        {
                            WeekInfoID = wInfo.WeekInfoID,
                            Color = (capacity + preproduced - (order + preorder) >= 0) ? "#000" : "#ff0000",
                            Value = capacity + preproduced - (order + preorder)
                        });
                    }

                    foreach (var factory in dbItems.Select(o => new { FactoryUD = o.FactoryUD, FactoryNM = o.FactoryName }).Distinct())
                    {
                        DTO.ReportDetailDTO detailRow = new DTO.ReportDetailDTO {
                            FactoryUD = factory.FactoryUD,
                            FactoryNM =factory.FactoryNM,
                            CapacityB = 0,
                            CapacityA = 0,
                            OrderB = 0,
                            OrderA = 0,
                            PreOrderB = 0,
                            PreOrderA = 0,
                            PreProducedB = 0,
                            PreProducedA = 0
                        };
                        foreach (var detail in dbItems.Where(o => o.FactoryUD == factory.FactoryUD).OrderBy(o => o.WeekStart))
                        {
                            if (detail.WeekStart >= tetDate)
                            {
                                detailRow.CapacityA += detail.Capacity.Value;
                                detailRow.OrderA += detail.TotalOrderQnt.Value;
                                detailRow.PreOrderA += detail.PreOrderQnt.Value;
                                detailRow.PreProducedA += detail.PreProducedQnt.Value;

                                data.SumDataRow.CapacityA += detail.Capacity.Value;
                                data.SumDataRow.OrderA += detail.TotalOrderQnt.Value;
                                data.SumDataRow.PreOrderA += detail.PreOrderQnt.Value;
                                data.SumDataRow.PreProducedA += detail.PreProducedQnt.Value;
                            }
                            else
                            {
                                detailRow.CapacityB += detail.Capacity.Value;
                                detailRow.OrderB += detail.TotalOrderQnt.Value;
                                detailRow.PreOrderB += detail.PreOrderQnt.Value;
                                detailRow.PreProducedB += detail.PreProducedQnt.Value;

                                data.SumDataRow.CapacityB += detail.Capacity.Value;
                                data.SumDataRow.OrderB += detail.TotalOrderQnt.Value;
                                data.SumDataRow.PreOrderB += detail.PreOrderQnt.Value;
                                data.SumDataRow.PreProducedB += detail.PreProducedQnt.Value;
                            }

                            detailRow.ReportWeekDetailDTOs.Add(new DTO.ReportWeekDetailDTO {
                                WeekInfoID = detail.WeekInfoID,
                                Color = "#000",
                                Value = detail.Capacity.Value
                            });
                            detailRow.ReportWeekDetailDTOs.Add(new DTO.ReportWeekDetailDTO
                            {
                                WeekInfoID = detail.WeekInfoID,
                                Color = "#000",
                                Value = detail.TotalOrderQnt.Value
                            });
                            detailRow.ReportWeekDetailDTOs.Add(new DTO.ReportWeekDetailDTO
                            {
                                WeekInfoID = detail.WeekInfoID,
                                Color = "#000",
                                Value = detail.PreOrderQnt.Value
                            });
                            detailRow.ReportWeekDetailDTOs.Add(new DTO.ReportWeekDetailDTO
                            {
                                WeekInfoID = detail.WeekInfoID,
                                Color = "#000",
                                Value = detail.PreProducedQnt.Value
                            });
                            detailRow.ReportWeekDetailDTOs.Add(new DTO.ReportWeekDetailDTO
                            {
                                WeekInfoID = detail.WeekInfoID,
                                Color = (detail.Capacity.Value + detail.PreProducedQnt.Value - (detail.TotalOrderQnt.Value + detail.PreOrderQnt.Value) >= 0) ? "#000" : "#ff0000",
                                Value = detail.Capacity.Value + detail.PreProducedQnt.Value - (detail.TotalOrderQnt.Value + detail.PreOrderQnt.Value)
                            });
                        }

                        data.ReportDetailDTOs.Add(detailRow);
                    }

                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            //data.ReportDetailDTOs = data.ReportDetailDTOs.OrderByDescending(o => o.OrderB).ToList();
            data.ReportDetailDTOs = data.ReportDetailDTOs.OrderBy(o => o.FactoryUD).ToList();
            return data;
        }
    }
}
