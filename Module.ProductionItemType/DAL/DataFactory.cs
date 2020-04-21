using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Module.ProductionItemType.DAL
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
        private ProductionItemTypeEntities CreateContext()
        {
            return new ProductionItemTypeEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductionItemTypeModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.ProductionItemTypeSearchDTO>();
            totalRows = 0;

            //try to get data
            try
            {
                using (ProductionItemTypeEntities context = CreateContext())
                {
                    string ProductionItemTypeUD = null;
                    string ProductionItemTypeNM = null;
                    int? ProductionItemMaterialTypeID = null;

                    if (filters.ContainsKey("ProductionItemTypeUD") && !string.IsNullOrEmpty(filters["ProductionItemTypeUD"].ToString()))
                    {
                        ProductionItemTypeUD = filters["ProductionItemTypeUD"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ProductionItemTypeNM") && !string.IsNullOrEmpty(filters["ProductionItemTypeNM"].ToString()))
                    {
                        ProductionItemTypeNM = filters["ProductionItemTypeNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ProductionItemMaterialTypeID") && !string.IsNullOrEmpty(filters["ProductionItemMaterialTypeID"].ToString()))
                    {
                        ProductionItemMaterialTypeID = Convert.ToInt32(filters["ProductionItemMaterialTypeID"]);
                    }

                    totalRows = context.ProductionItemMng_function_SearchProductionItemType(ProductionItemTypeUD, ProductionItemTypeNM, ProductionItemMaterialTypeID, orderBy, orderDirection).Count();
                    var result = context.ProductionItemMng_function_SearchProductionItemType(ProductionItemTypeUD, ProductionItemTypeNM, ProductionItemMaterialTypeID, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ProductionItemTypeSearchList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            DTO.ProductionItemTypeDTO dtoProductionItemType = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ProductionItemTypeDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;
            try
            {
                using (ProductionItemTypeEntities context = CreateContext())
                {
                    ProductionItemType dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ProductionItemType();
                        context.ProductionItemType.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ProductionItemType.FirstOrDefault(o => o.ProductionItemTypeID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Production Item Type not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2BD(dtoProductionItemType, ref dbItem);

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;

                        context.SaveChanges();

                        dtoItem = GetData(userId, dbItem.ProductionItemTypeID, out notification).Data;

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
                        case "IX_ProductionItemTypeUDUnique":
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
            data.Data = new DTO.ProductionItemTypeDTO();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (ProductionItemTypeEntities context = CreateContext())
                    {
                        var ProductionItemType = context.ProductionItemMng_ProductionItemType_View.FirstOrDefault(o => o.ProductionItemTypeID == id);
                        if (ProductionItemType == null)
                        {
                            throw new Exception("Can not found the Production Item Type to edit");
                        }

                        data.Data = converter.DB2DTO_ProductionItemType(ProductionItemType);
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
