using Library;
using Library.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FrameMaterialMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.FrameMaterialMng.SearchFormData, DTO.FrameMaterialMng.EditFormData, DTO.FrameMaterialMng.FrameMaterial>
    {
        private DataConverter converter = new DataConverter();
        private FrameMaterialMngEntities CreateContext()
        {
            return new FrameMaterialMngEntities(DALBase.Helper.CreateEntityConnectionString("FrameMaterialMngModel"));
        }

        public override DTO.FrameMaterialMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FrameMaterialMng.SearchFormData data = new DTO.FrameMaterialMng.SearchFormData();
            data.Data = new List<DTO.FrameMaterialMng.FrameMaterialSearchResult>();
            totalRows = 0;

            string FrameMaterialUD = null;
            string FrameMaterialNM = null;
            if (filters.ContainsKey("FrameMaterialUD") && !string.IsNullOrEmpty(filters["FrameMaterialUD"].ToString()))
            {
                FrameMaterialUD = filters["FrameMaterialUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FrameMaterialNM") && !string.IsNullOrEmpty(filters["FrameMaterialNM"].ToString()))
            {
                FrameMaterialNM = filters["FrameMaterialNM"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (FrameMaterialMngEntities context = CreateContext())
                {
                    totalRows = context.FrameMaterialMng_function_SearchFrameMaterial(FrameMaterialUD, FrameMaterialNM, orderBy, orderDirection).Count();
                    var result = context.FrameMaterialMng_function_SearchFrameMaterial(FrameMaterialUD, FrameMaterialNM, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_FrameMaterialSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.FrameMaterialMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FrameMaterialMng.EditFormData data = new DTO.FrameMaterialMng.EditFormData();
            data.Data = new DTO.FrameMaterialMng.FrameMaterial();

            //try to get data
            try
            {
                using (FrameMaterialMngEntities context = CreateContext())
                {
                    data.Data =  converter.DB2DTO_FrameMaterial(context.FrameMaterialMng_FrameMaterial_View.FirstOrDefault(o => o.FrameMaterialID == id));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FrameMaterialMngEntities context = CreateContext())
                {
                    FrameMaterial dbItem = context.FrameMaterial.FirstOrDefault(o => o.FrameMaterialID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Frame material not found!";
                        return false;
                    }
                    else
                    {
                        context.FrameMaterial.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.FrameMaterialMng.FrameMaterial dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            int number;
            string indexName;

            try
            {
                using (var context = CreateContext())
                {
                    FrameMaterial frameMaterial = null;

                    if (id == 0)
                    {
                        frameMaterial = new FrameMaterial();

                        context.FrameMaterial.Add(frameMaterial);
                    }
                    else
                    {
                        frameMaterial = context.FrameMaterial.FirstOrDefault(o => o.FrameMaterialID == id);
                    }

                    if (frameMaterial == null)
                    {
                        notification.Message = "Frame Material not found!";

                        return false;
                    }
                    else
                    {
                        converter.DTO2BD(dtoItem, ref frameMaterial);

                        if (id <= 0)
                        {
                            // Generate code.
                            using (var trans = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM FrameMaterial WITH (TABLOCKX, HOLDLOCK)");

                                try
                                {
                                    var newCode = context.FrameMaterialMng_function_GenerateCode().FirstOrDefault();

                                    if (!"**".Equals(newCode))
                                    {
                                        frameMaterial.FrameMaterialUD = newCode;

                                        context.SaveChanges();
                                    }
                                    else
                                    {
                                        notification.Type = NotificationType.Error;
                                        notification.Message = "Auto generated code exceed maximum option: [ZZ]";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    throw ex;
                                }
                                finally
                                {
                                    trans.Commit();
                                }
                            }
                        }
                        else
                        {
                            context.SaveChanges();
                        }

                        dtoItem = GetData(frameMaterial.FrameMaterialID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (DataException exData)
            {
                notification.Type = NotificationType.Error;
                ErrorHelper.DataExceptionParser(exData, out number, out indexName);

                if (number == 2601 && !string.IsNullOrEmpty(indexName))
                {
                    if ("FrameMaterialUDUnique".Equals(indexName))
                        notification.Message = "The Frame Material Code is already exists.";
                }
                else
                {
                    notification.Message = exData.Message;
                }

                return false;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        public override bool Approve(int id, ref DTO.FrameMaterialMng.FrameMaterial dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.FrameMaterialMng.FrameMaterial dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
