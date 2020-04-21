using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Module.FactoryBreakdownMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private FactoryBreakdownMngEntities CreateContext()
        {
            return new FactoryBreakdownMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryBreakdownMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FactoryBreakdownSearchResultDTO>();
            totalRows = 0;

            string ClientUD = null;
            string SampleOrderUD = null;
            string SampleProductUD = null;
            string ArticleDescription = null;
            int UserID = 0;
            bool? IsConfirmed = null;
            string ModelUD = null;
            string ModelNM = null;
            string Season = null;
            int FactoryID = 0;
            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("SampleOrderUD") && !string.IsNullOrEmpty(filters["SampleOrderUD"].ToString()))
            {
                SampleOrderUD = filters["SampleOrderUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ArticleDescription") && !string.IsNullOrEmpty(filters["ArticleDescription"].ToString()))
            {
                ArticleDescription = filters["ArticleDescription"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("UserID") && filters["UserID"] != null && !string.IsNullOrEmpty(filters["UserID"].ToString()))
            {
                UserID = Convert.ToInt32(filters["UserID"].ToString());
            }
            if (filters.ContainsKey("IsConfirmed") && filters["IsConfirmed"] != null && !string.IsNullOrEmpty(filters["IsConfirmed"].ToString()))
            {
                IsConfirmed = Convert.ToBoolean(filters["IsConfirmed"].ToString());
            }
            if (filters.ContainsKey("SampleProductUD") && !string.IsNullOrEmpty(filters["SampleProductUD"].ToString()))
            {
                SampleProductUD = filters["SampleProductUD"].ToString().Replace("'", "''");
                ModelUD = SampleProductUD;
            }
            if (filters.ContainsKey("ModelNM") && !string.IsNullOrEmpty(filters["ModelNM"].ToString()))
            {
                ModelNM = filters["ModelNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryID") && filters["FactoryID"] != null && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
            {
                FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
            }
            //try to get data
            try
            {
                using (FactoryBreakdownMngEntities context = CreateContext())
                {
                    var resultSet = context.FactoryBreakdownMng_function_SearchFactoryBreakdown(IsConfirmed, ClientUD, SampleOrderUD, ArticleDescription, ModelUD, ModelNM, Season, UserID, SampleProductUD, FactoryID, orderBy, orderDirection).ToList();
                    totalRows = resultSet.Count();

                    var sampleProduct = context.Breakdown.Where(o => o.SampleProductID != null).Select(o => o.SampleProductID).ToList();

                    var statistic = resultSet.Where(o => o.IsConfirmed != true && !sampleProduct.Contains(o.SampleProductID)).GroupBy(o => o.FactoryID).Select(group => new DTO.FactoryBreakdownStatistic { FactoryID = group.Key, CountFactory = group.Count() }).ToList();
                    data.Statistic = statistic;
                    data.Data = converter.DB2DTO_FactoryBreakdownSearchResultDTO(resultSet.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
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
            DTO.FactoryBreakdownDTO dtoBreakdown = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryBreakdownDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // check data access permission
                if (dtoBreakdown.SampleProductID.HasValue)
                {
                    if (CheckSamplePermission(userId, dtoBreakdown.SampleProductID.Value) <= 0)
                    {
                        throw new Exception("Data access not authorized!");
                    }
                }
                //else
                //{
                //    throw new Exception("Invalid sample product data! (id null)");
                //}

                using (FactoryBreakdownMngEntities context = CreateContext())
                {
                    FactoryBreakdown dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryBreakdown();
                        context.FactoryBreakdown.Add(dbItem);
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        dbItem = context.FactoryBreakdown.FirstOrDefault(o => o.FactoryBreakdownID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Factory breakdown not found!";
                        return false;
                    }
                    else
                    {
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        converter.DTO2DB_FactoryBreakdown(dtoBreakdown, ref dbItem, userId);

                        context.FactoryBreakdownDetail.Local.Where(o => o.FactoryBreakdown == null).ToList().ForEach(o => context.FactoryBreakdownDetail.Remove(o));
                        context.FactoryBreakdownModel.Local.Where(o => o.FactoryBreakdown == null).ToList().ForEach(o => context.FactoryBreakdownModel.Remove(o));

                        context.SaveChanges();
                        dtoItem = GetData(userId, dbItem.FactoryBreakdownID, dbItem.SampleProductID, dbItem.ModelID, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryBreakdownMngEntities context = CreateContext())
                {
                    FactoryBreakdown dbItem = context.FactoryBreakdown.FirstOrDefault(o => o.FactoryBreakdownID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Factory breakdown not found!";
                        return false;
                    }
                    else
                    {
                        // check data access permission
                        if (dbItem.SampleProductID.HasValue)
                        {
                            if (CheckSamplePermission(userId, dbItem.SampleProductID.Value) <= 0)
                            {
                                throw new Exception("Data access not authorized!");
                            }
                        }
                        //else
                        //{
                        //    throw new Exception("Invalid sample product data! (id null)");
                        //}

                        // validate data
                        if (!dbItem.IndicatedPrice.HasValue || dbItem.IndicatedPrice <= 0)
                        {
                            throw new Exception("Invalid indicated price!");
                        }
                        FactoryBreakdownDetail dbDetail = dbItem.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownCategoryID == 11); // load ability
                        if (dbDetail == null || !dbDetail.Quantity.HasValue || dbDetail.Quantity <= 0)
                        {
                            throw new Exception("Invalid load ability!");
                        }

                        dbItem.IsConfirmed = true;
                        dbItem.ConfirmedBy = userId;
                        dbItem.ConfirmedDate = DateTime.Now;
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryBreakdownMngEntities context = CreateContext())
                {
                    FactoryBreakdown dbItem = context.FactoryBreakdown.FirstOrDefault(o => o.FactoryBreakdownID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Factory breakdown not found!";
                        return false;
                    }
                    else
                    {
                        // check data access permission
                        if (dbItem.SampleProductID.HasValue)
                        {
                            if (CheckSamplePermission(userId, dbItem.SampleProductID.Value) <= 0)
                            {
                                throw new Exception("Data access not authorized!");
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid sample product data! (id null)");
                        }

                        dbItem.IsConfirmed = false;
                        dbItem.ConfirmedBy = null;
                        dbItem.ConfirmedDate = null;
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        //
        // CUSTOM FUNCTION HERE
        //

        public DTO.EditFormData GetData(int userId, int id, int? sampleProductID, int? modelID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.FactoryBreakdownDTO();
            data.Data.FactoryBreakdownDetailDTOs = new List<DTO.FactoryBreakdownDetailDTO>();

            //try to get data
            try
            {
                using (FactoryBreakdownMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_FactoryBreakdownDTO(context.FactoryBreakdownMng_FactoryBreakdown_View
                            .Include("FactoryBreakdownMng_FactoryBreakdownDetail_View")
                            .FirstOrDefault(o => o.FactoryBreakdownID == id));
                    }
                    else
                    {
                        if (sampleProductID.HasValue)
                        {
                            // Auto create new Factory Breakdown.
                            context.FactoryBreakdownMng_function_AddNewSampleProduct();

                            // Load Factory Breakdown with SampleProductID.
                            data.Data = converter.DB2DTO_FactoryBreakdownDTO(context.FactoryBreakdownMng_FactoryBreakdown_View
                                .Include("FactoryBreakdownMng_FactoryBreakdownDetail_View")
                                .FirstOrDefault(o => o.SampleProductID == sampleProductID));
                        }
                        else
                        {
                            throw new Exception("Invalid data flow, data should be auto generated! (not <= 0)");
                        }
                    }
                    if (sampleProductID > 0)
                    {
                        // check data access permission
                        if (data.Data.SampleProductID.HasValue)
                        {
                            if (CheckSamplePermission(userId, data.Data.SampleProductID.Value) <= 0)
                            {
                                throw new Exception("Data access not authorized!");
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid sample product data! (id null)");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        private int CheckSamplePermission(int userId, int sampleProductId)
        {
            using (FactoryBreakdownMngEntities context = CreateContext())
            {
                try
                {
                    return context.FactoryBreakdownMng_function_CheckSamplePermission(userId, sampleProductId).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public string GetFactoryBreakdownExportToExcelList(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            FactoryBreakdownDataObject ds = new FactoryBreakdownDataObject();

            //try to get data
            try
            {
                string ClientUD = null;
                string SampleOrderUD = null;
                string SampleProductUD = null;
                string ArticleDescription = null;
                int UserID = 0;
                bool? IsConfirmed = null;
                if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                {
                    ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("SampleOrderUD") && !string.IsNullOrEmpty(filters["SampleOrderUD"].ToString()))
                {
                    SampleOrderUD = filters["SampleOrderUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ArticleDescription") && !string.IsNullOrEmpty(filters["ArticleDescription"].ToString()))
                {
                    ArticleDescription = filters["ArticleDescription"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("UserID") && filters["UserID"] != null && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                {
                    UserID = Convert.ToInt32(filters["UserID"].ToString());
                }
                if (filters.ContainsKey("IsConfirmed") && filters["IsConfirmed"] != null && !string.IsNullOrEmpty(filters["IsConfirmed"].ToString()))
                {
                    IsConfirmed = Convert.ToBoolean(filters["IsConfirmed"].ToString());
                }
                if (filters.ContainsKey("SampleProductUD") && !string.IsNullOrEmpty(filters["SampleProductUD"].ToString()))
                {
                    SampleProductUD = filters["SampleProductUD"].ToString().Replace("'", "''");
                }
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryBreakdownMng_function_ReportFactoryBreakdown", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@IsConfirmed", IsConfirmed);
                adap.SelectCommand.Parameters.AddWithValue("@ClientUD", ClientUD);
                adap.SelectCommand.Parameters.AddWithValue("@SampleOrderUD", SampleOrderUD);
                adap.SelectCommand.Parameters.AddWithValue("@ArticleDescription", ArticleDescription);
                adap.SelectCommand.Parameters.AddWithValue("@SampleProductUD", SampleProductUD);
                adap.SelectCommand.Parameters.AddWithValue("@UserID", UserID);
                adap.SelectCommand.Parameters.AddWithValue("@SortedBy", String.Empty);
                adap.SelectCommand.Parameters.AddWithValue("@SortedDirection", String.Empty);
                adap.TableMappings.Add("Table", "FactoryBreakdownReportView");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "ListFactoryBreakdown");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return string.Empty;
            }
        }

        //public object GetDataWithSampleProduct(int userID, int id, int sampleProductID, out Library.DTO.Notification notification)
        //{
        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
        //    DTO.EditFormData data = new DTO.EditFormData();
        //    data.Data = new DTO.FactoryBreakdownDTO();
        //    data.Data.FactoryBreakdownDetailDTOs = new List<DTO.FactoryBreakdownDetailDTO>();

        //    try
        //    {
        //        using (var context = CreateContext())
        //        {
        //            if (id == 0)
        //            {
        //                // Auto create new Factory Breakdown.
        //                context.FactoryBreakdownMng_function_AddNewSampleProduct();

        //                // Load Factory Breakdown with SampleProductID.
        //                data.Data = converter.DB2DTO_FactoryBreakdownDTO(context.FactoryBreakdownMng_FactoryBreakdown_View
        //                    .Include("FactoryBreakdownMng_FactoryBreakdownDetail_View")
        //                    .FirstOrDefault(o => o.SampleProductID == sampleProductID));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = Library.Helper.GetInnerException(ex).Message;
        //    }

        //    return data;
        //}

        public object ExportExcelFactoryBreakdownDetail(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            string fileName = string.Empty;
            FactoryBreakdownDetailObject ds = new FactoryBreakdownDetailObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryBreakdownMng_function_ExportFactoryBreakdown", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@FactoryBreakdownID", id);
                adap.TableMappings.Add("Table", "FactoryBreakdown");
                adap.TableMappings.Add("Table1", "FactoryBreakdownDetail");
                adap.Fill(ds);

                foreach (var item in ds.FactoryBreakdown)
                {
                    item.ThumbnailLocation = FrameworkSetting.Setting.MediaThumbnailUrl + (!item.IsThumbnailLocationNull() ? item.ThumbnailLocation : "no-image.jpg");
                }

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "FactoryBreakdownDetail");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return fileName;
        }

        /// <summary>
        /// Get data open Breakdown
        /// </summary>
        /// <param name="sampleProductID"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public object GetDataForBreakdown(int sampleProductID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.FactoryBreakdownDTO data = new DTO.FactoryBreakdownDTO();

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.FactoryBreakdownMng_FactoryBreakdown_View.FirstOrDefault(o => o.SampleProductID == sampleProductID);

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can't find Factory Breakdown!";

                        return data;
                    }

                    data = converter.DB2DTO_FactoryBreakdownDTO(dbItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Seasons = new List<Support.DTO.Season>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
                data.Factories = supportFactory.GetFactory().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool ImportExcelData(int userId, ref object dtoItem, out Library.DTO.Notification notification)
        {
            List<DTO.ImportFactoryBreakdown> dtoFactoryBreakdowns = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.ImportFactoryBreakdown>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryBreakdownMngEntities context = CreateContext())
                {
                    foreach (DTO.ImportFactoryBreakdown dtoFactoryBreakdown in dtoFactoryBreakdowns.ToList())
                    {
                        FactoryBreakdown dbFactoryBreakdown;
                        dbFactoryBreakdown = context.FactoryBreakdown.FirstOrDefault(o => o.FactoryBreakdownID == dtoFactoryBreakdown.FactoryBreakdownID);
                        converter.DTO2DB_ImportFactoryBreakdown(dtoFactoryBreakdown, ref dbFactoryBreakdown, userId);
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }
    }
}
