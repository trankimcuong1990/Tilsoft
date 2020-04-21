using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
namespace Module.FactoryMaterial.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private string _tempFolder;

        public DataFactory() {}
        
        public DataFactory(string tempFolder) {
            this._tempFolder = tempFolder + @"\";
        }

        private FactoryMaterialEntities CreateContext()
        {
            return new FactoryMaterialEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryMaterialModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryMaterialEntities context = CreateContext())
                {
                    var dbItem = context.FactoryMaterial.Where(o => o.FactoryMaterialID == id).FirstOrDefault();
                    foreach (var item in dbItem.FactoryMaterialImage.ToArray())
                    {
                        context.FactoryMaterialImage.Remove(item);
                    }
                    context.FactoryMaterial.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }
            
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
            try
            {
                using (FactoryMaterialEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        FactoryMaterialMng_FactoryMaterial_View dbItem;
                        dbItem = context.FactoryMaterialMng_FactoryMaterial_View.FirstOrDefault(o => o.FactoryMaterialID == id);
                        editFormData.Data = converter.DB2DTO_FactoryMaterial(dbItem);
                    }
                    else
                    {
                        editFormData.Data = new DTO.FactoryMaterial();
                        editFormData.Data.FactoryMaterialImages = new List<DTO.FactoryMaterialImage>();
                    }

                    //get support list
                    editFormData.Units = support_factory.GetUnit(1);
                    editFormData.FactoryMaterialGroups = support_factory.GetFactoryMaterialGroup();
                    editFormData.FactoryMaterialTypes = support_factory.GetFactoryMaterialType();
                    editFormData.FactoryMaterialColors = support_factory.GetFactoryMaterialColor();
                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return editFormData;
            }
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
            totalRows = 0;

            string factoryMaterialUD = string.Empty;
            string factoryMaterialNM = string.Empty;
            string factoryMaterialEnglishName = string.Empty;
            int? factoryMaterialGroupID = null;
            if (filters.ContainsKey("factoryMaterialUD")) factoryMaterialUD = filters["factoryMaterialUD"].ToString();
            if (filters.ContainsKey("factoryMaterialNM")) factoryMaterialNM = filters["factoryMaterialNM"].ToString();
            if (filters.ContainsKey("factoryMaterialEnglishName")) factoryMaterialEnglishName = filters["factoryMaterialEnglishName"].ToString();
            if (filters.ContainsKey("factoryMaterialGroupID") && filters["factoryMaterialGroupID"]!= null) factoryMaterialGroupID = Convert.ToInt32(filters["factoryMaterialGroupID"]);

            try
            {
                using (FactoryMaterialEntities context = CreateContext())
                {
                    totalRows = context.FactoryMaterialMng_function_SearchFactoryMaterial(orderBy, orderDirection, factoryMaterialUD, factoryMaterialNM, factoryMaterialEnglishName, factoryMaterialGroupID).Count();
                    var result = context.FactoryMaterialMng_function_SearchFactoryMaterial(orderBy, orderDirection, factoryMaterialUD, factoryMaterialNM, factoryMaterialEnglishName, factoryMaterialGroupID);
                    searchFormData.Data = converter.DB2DTO_FactoryMaterialSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    foreach (var item in searchFormData.Data)
                    {
                        item.FactoryMaterialImages = converter.DB2DTO_FactoryMaterialImage(context.FactoryMaterialMng_FactoryMaterialImage_View.Where(o => o.FactoryMaterialID == item.FactoryMaterialID).ToList());
                    }
                }
                searchFormData.FactoryMaterialGroups = support_factory.GetFactoryMaterialGroup();
                return searchFormData;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return searchFormData;
            }
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryMaterial dtoFactoryMaterial = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryMaterial>();
            try
            {
                using (FactoryMaterialEntities context = CreateContext())
                {
                    FactoryMaterial dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryMaterial();
                        context.FactoryMaterial.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryMaterial.Where(o => o.FactoryMaterialID == id).FirstOrDefault();
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_FactoryMaterial(dtoFactoryMaterial, ref dbItem, this._tempFolder);
                        //remove orphan item
                        context.FactoryMaterialImage.Local.Where(o => o.FactoryMaterial == null).ToList().ForEach(o => context.FactoryMaterialImage.Remove(o));
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.FactoryMaterialID, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }
        }

        public string PrintFactoryMaterialStock(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryMaterialMng_function_GetStock", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.Fill(ds.FactoryMaterialMng_Stock_View);

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "FactoryMaterialStock");

                // generate xml file
                return Library.Helper.CreateReportFiles(ds, "FactoryMaterialStock");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return string.Empty;
            }
        }
    }
}
