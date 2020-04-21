using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.DTO;
using Module.ProductTestingMng.DTO;

namespace Module.ProductTestingMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchForm, DTO.EditForm>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        //private string _TempFolder;

        private ProductTestingMngEntities CreatContex()
        {
            return new ProductTestingMngEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductTestingMngModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            try
            {
             using(var context = CreatContex())
                {
                    var dbItem = context.ProductTest.Where(o => o.ProductTestID == id).FirstOrDefault();
                    context.ProductTest.Remove(dbItem);
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

        public override EditForm GetData(int id, out Notification notification)
        {
            DTO.EditForm editForm = new EditForm();
            editForm.Data = new ProductTestingDTO();
            editForm.Data.productTestFileDTOs = new List<ProductTestFileDTO>();
            editForm.Data.productTestStandardDTOs = new List<ProductTestStandardDTO>();
            editForm.supportProductTestStandards = new List<SupportProductTestStandard>();

            notification = new Notification() { Type = NotificationType.Success };

            try
            {  using(var context = CreatContex())
                {
                    if (id > 0)
                    {
                        ProductTestingMng_ProductTesting_View dbItem;
                        dbItem = context.ProductTestingMng_ProductTesting_View.Include("ProductTestingMng_ProductTestFile_View").FirstOrDefault(o => o.ProductTestID == id);
                        editForm.Data = converter.DB2DTO_ProductTesting(dbItem);
                        
                    }
                    editForm.supportProductTestStandards = converter.DB2DTO_spListTestStandard(context.SupportMng_ProductTestStandard_View.ToList());
                }
                return editForm;
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
                return editForm;
            }
        }

        public override SearchForm GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            
            totalRows = 0;
            notification = new Notification() { Type = NotificationType.Success };

            SearchForm data = new SearchForm();
            data.Data = new List<ProductTestingSearchResultDTO>();


            string productTestUD = null;
            string modelUD = null;
            string modelNM = null;
            string clientUD = null;
            string friendlyName = null;
            string testStandardNM = null;
            string testDate = null;


            if (filters.ContainsKey("ProductTestUD") && !string.IsNullOrEmpty(filters["ProductTestUD"].ToString()))
            {
                productTestUD = filters["ProductTestUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ModelUD") && !string.IsNullOrEmpty(filters["ModelUD"].ToString()))
            {
                modelUD = filters["ModelUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ModelNM") && !string.IsNullOrEmpty(filters["ModelNM"].ToString()))
            {
                modelNM = filters["ModelNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                clientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FriendlyName") && !string.IsNullOrEmpty(filters["FriendlyName"].ToString()))
            {
                friendlyName = filters["FriendlyName"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("TestStandardNM") && !string.IsNullOrEmpty(filters["TestStandardNM"].ToString()))
            {
                testStandardNM = filters["TestStandardNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("TestDate") && !string.IsNullOrEmpty(filters["TestDate"].ToString()))
            {
                testDate = filters["TestDate"].ToString().Replace("'", "''");
            }

            DateTime? productTestDate = testDate.ConvertStringToDateTime();
            try
            {
              using(var context = CreatContex())
                {
                    totalRows = context.ProductTestingMng_Function_SearchResult(productTestUD, modelUD, modelNM, clientUD, friendlyName, testStandardNM, productTestDate, orderBy, orderDirection).Count();
                    var result = context.ProductTestingMng_Function_SearchResult(productTestUD, modelUD, modelNM, clientUD, friendlyName, testStandardNM, productTestDate, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            }
            return data; 
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.ProductTestingDTO dtoItems = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ProductTestingDTO>();
            try
            {
                using(var context = CreatContex())
                {
                    ProductTest dbItem = null;
                    using (DbContextTransaction scope = context.Database.BeginTransaction())
                    {
                        context.Database.ExecuteSqlCommand("SELECT * FROM ProductTest WITH (TABLOCKX, HOLDLOCK)");
                        try
                        {                         
                            if (id == 0)
                            {
                                dbItem = new ProductTest();
                                context.ProductTest.Add(dbItem);
                            }
                            else
                            {
                                dbItem = context.ProductTest.Where(o => o.ProductTestID == id).FirstOrDefault();
                            }
                            if (dbItem == null)
                            {
                                notification.Message = "Data Not found !";
                                return false;
                            }
                            else
                            {
                                converter.DTO2DB_ProductTesting(dtoItems, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);

                                //remove
                                context.ProductTestFile.Local.Where(o => o.ProductTest == null).ToList().ForEach(o => context.ProductTestFile.Remove(o));
                                context.ProductTestUsingTestStandard.Local.Where(o => o.ProductTest == null).ToList().ForEach(o => context.ProductTestUsingTestStandard.Remove(o));

                                dbItem.UpdatedBy = userId;
                                dbItem.UpdatedDate = DateTime.Now;

                                context.SaveChanges();

                                //Create code of id
                                dbItem.ProductTestUD = "P" + dbItem.ProductTestID.ToString("D9");
                                context.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            scope.Commit();
                        }
                    }
                    dtoItem = GetData(dbItem.ProductTestID, out notification).Data;
                }
                return true;
            }
            catch (Exception ex )
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
    }
}
