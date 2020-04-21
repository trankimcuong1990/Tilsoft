using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Module.ProductionItemGroup.DTO;

namespace Module.ProductionItemGroup.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private string _tempFolder;
        private DataConverter converter = new DataConverter();
        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }
        private ProductionItemGroupEntities CreateContext()
        {
            return new ProductionItemGroupEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductionItemGroupModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.ProductionItemGroupSearchDTO>();
            totalRows = 0;

            //try to get data
            try
            {
                using (ProductionItemGroupEntities context = CreateContext())
                {
                    string ProductionItemGroupUD = null;
                    string ProductionItemGroupNM = null;
                    //int? ProductionItemGroupID = null;

                    if (filters.ContainsKey("ProductionItemGroupUD") && !string.IsNullOrEmpty(filters["ProductionItemGroupUD"].ToString()))
                    {
                        ProductionItemGroupUD = filters["ProductionItemGroupUD"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ProductionItemGroupNM") && !string.IsNullOrEmpty(filters["ProductionItemGroupNM"].ToString()))
                    {
                        ProductionItemGroupNM = filters["ProductionItemGroupNM"].ToString().Replace("'", "''");
                    }

                    //if (filters.ContainsKey("ProductionItemGroupID") && !string.IsNullOrEmpty(filters["ProductionItemGroupID"].ToString()))
                    //{
                    //    ProductionItemGroupID = Convert.ToInt32(filters["ProductionItemGroupID"]);
                    //}

                    totalRows = context.ProductionItemMng_function_SearchProductionItemGroup(ProductionItemGroupUD, ProductionItemGroupNM, orderBy, orderDirection).Count();
                    var result = context.ProductionItemMng_function_SearchProductionItemGroup(ProductionItemGroupUD, ProductionItemGroupNM, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ProductionItemGroupSearchList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.ProductionItemGroupDTO dtoProductionItemGroup = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ProductionItemGroupDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;
            try
            {
                using (ProductionItemGroupEntities context = CreateContext())
                {
                    ProductionItemGroup dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ProductionItemGroup();
                        context.ProductionItemGroup.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ProductionItemGroup.FirstOrDefault(o => o.ProductionItemGroupID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Production Item Group not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2BD(dtoProductionItemGroup, ref dbItem);

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;
                        context.ProductionItemGroupNotification.Local.Where(o => o.ProductionItemGroup == null).ToList().ForEach(o => context.ProductionItemGroupNotification.Remove(o));
                        context.ProductionItemGroupStockNotification.Local.Where(o => o.ProductionItemGroup == null).ToList().ForEach(o => context.ProductionItemGroupStockNotification.Remove(o));
                        context.SaveChanges();
                        dtoItem = GetData(userId, dbItem.ProductionItemGroupID, out notification).Data;

                        return true;
                    }
                }
            }

            catch (System.Data.DataException dEx)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Library.ErrorHelper.DataExceptionParser(dEx, out number, out indexName);
                if (number == 2601 && !string.IsNullOrEmpty(indexName))
                {
                    switch (indexName)
                    {
                        case "IX_ProductionItemGroupUDUnique":
                            notification.Message = "Code must be unique! Please select other code!";
                            break;
                    }
                }
                else
                {
                    notification.Message = dEx.Message;
                }
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.EditFormData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.ProductionItemGroupDTO();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (ProductionItemGroupEntities context = CreateContext())
                    {

                        var ProductionItemGroup = context.ProductionItemMng_ProductionItemGroup_View.FirstOrDefault(o => o.ProductionItemGroupID == id);
                        if (ProductionItemGroup == null)
                        {
                            throw new Exception("Can not found the Production Item group to edit");

                        }

                        data.Data = converter.DB2DTO_ProductionItemGroup(ProductionItemGroup);
                        //data.ListUser = converter.DB2DTO_UserOnProductionItemGroup(context.SupportMng_User2_View.ToList());

                        //var ProductionItemGroup = context.ProductionItemMng_ProductionItemGroup_View.FirstOrDefault(o => o.ProductionItemGroupID == id);
                        //if (ProductionItemGroup == null)
                        //{
                        //    throw new Exception("Can not found the Production Item group to edit");
                        //}

                        //data.Data = converter.DB2DTO_ProductionItemGroup(ProductionItemGroup);

                    }
                }
                else
                {
                    data.Data.ListProductionItemGroupNotification = new List<ProductionItemGroupNotificationDTO>();
                    data.Data.ListProductionItemGroupStockNotification = new List<ProductionItemGroupStockNotificationDTO>();
                }

                using (var context = CreateContext())
                {
                    data.ListUser = converter.DB2DTO_UserOnProductionItemGroup(context.ProductionItemGroupMng_User2_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();

            try
            {
                data.YesNoValues = supportFactory.GetYesNo().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
    }
}
