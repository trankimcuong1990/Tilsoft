using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Module.ProductionItemSpec.DAL
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
        private ProductionItemSpecEntities CreateContext()
        {
            return new ProductionItemSpecEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductionItemSpecModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.ProductionItemSpecSearchDTO>();
            totalRows = 0;

            //try to get data
            try
            {
                using (ProductionItemSpecEntities context = CreateContext())
                {
                    string ProductionItemSpecUD = null;
                    string ProductionItemSpecNM = null;
                    int? ProductionItemSpecTypeID = null;

                    if (filters.ContainsKey("ProductionItemSpecUD") && !string.IsNullOrEmpty(filters["ProductionItemSpecUD"].ToString()))
                    {
                        ProductionItemSpecUD = filters["ProductionItemSpecUD"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ProductionItemSpecNM") && !string.IsNullOrEmpty(filters["ProductionItemSpecNM"].ToString()))
                    {
                        ProductionItemSpecNM = filters["ProductionItemSpecNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ProductionItemSpecTypeID") && !string.IsNullOrEmpty(filters["ProductionItemSpecTypeID"].ToString()))
                    {
                        ProductionItemSpecTypeID = Convert.ToInt32(filters["ProductionItemSpecTypeID"]);
                    }

                    totalRows = context.ProductionItemMng_function_SearchProductionItemSpec(ProductionItemSpecUD, ProductionItemSpecNM, ProductionItemSpecTypeID, orderBy, orderDirection).Count();
                    var result = context.ProductionItemMng_function_SearchProductionItemSpec(ProductionItemSpecUD, ProductionItemSpecNM, ProductionItemSpecTypeID, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ProductionItemSpecSearchList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            DTO.ProductionItemSpecDTO dtoProductionItemSpec = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ProductionItemSpecDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;
            try
            {
                using (ProductionItemSpecEntities context = CreateContext())
                {
                    ProductionItemSpec dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ProductionItemSpec();
                        context.ProductionItemSpec.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ProductionItemSpec.FirstOrDefault(o => o.ProductionItemSpecID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Production Item Spec not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2BD(dtoProductionItemSpec, ref dbItem);

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;

                        context.SaveChanges();

                        dtoItem = GetData(userId, dbItem.ProductionItemSpecID, out notification).Data;

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
                        case "IX_ProductionItemSpecUnique":
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
            data.Data = new DTO.ProductionItemSpecDTO();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (ProductionItemSpecEntities context = CreateContext())
                    {
                        var ProductionItemSpec = context.ProductionItemMng_ProductionItemSpec_View.FirstOrDefault(o => o.ProductionItemSpecID == id);
                        if (ProductionItemSpec == null)
                        {
                            throw new Exception("Can not found the Production Item Spec to edit");
                        }

                        data.Data = converter.DB2DTO_ProductionItemSpec(ProductionItemSpec);
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
