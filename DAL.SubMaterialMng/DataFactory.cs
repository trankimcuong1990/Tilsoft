using Library;
using Library.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SubMaterialMng
{
    public class DataFactory:DALBase.FactoryBase2<DTO.SubMaterialMng.SearchFormData, DTO.SubMaterialMng.EditFormData, DTO.SubMaterialMng.SubMaterial>
    {
        private DataConverter converter = new DataConverter();
        private SubMaterialMngEntities CreateContext()
        {
            return new SubMaterialMngEntities(DALBase.Helper.CreateEntityConnectionString("SubMaterialMngModel"));
        }

        public override DTO.SubMaterialMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SubMaterialMng.SearchFormData data = new DTO.SubMaterialMng.SearchFormData();
            data.Data = new List<DTO.SubMaterialMng.SubMaterialSearchResult>();
            totalRows = 0;

            string SubMaterialUD = null;
            string SubMaterialNM = null;
            if (filters.ContainsKey("SubMaterialUD") && !string.IsNullOrEmpty(filters["SubMaterialUD"].ToString()))
            {
                SubMaterialUD = filters["SubMaterialUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("SubMaterialNM") && !string.IsNullOrEmpty(filters["SubMaterialNM"].ToString()))
            {
                SubMaterialNM = filters["SubMaterialNM"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (SubMaterialMngEntities context = CreateContext())
                {
                    totalRows = context.SubMaterialMng_function_SearchSubMaterial(SubMaterialUD, SubMaterialNM, orderBy, orderDirection).Count();
                    var result = context.SubMaterialMng_function_SearchSubMaterial(SubMaterialUD, SubMaterialNM, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SubMaterialSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.SubMaterialMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SubMaterialMng.EditFormData data = new DTO.SubMaterialMng.EditFormData();
            data.Data = new DTO.SubMaterialMng.SubMaterial();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (SubMaterialMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_SubMaterial(context.SubMaterialMng_SubMaterial_View.FirstOrDefault(o => o.SubMaterialID == id));
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

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (SubMaterialMngEntities context = CreateContext())
                {
                    SubMaterial dbItem = context.SubMaterial.FirstOrDefault(o => o.SubMaterialID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sub material not found!";
                        return false;
                    }
                    else
                    {
                        var item = context.SubMaterialMng_SubMaterialCheck_View.Where(o => o.SubMaterialID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't delete because it used in item other!");
                        }
                        context.SubMaterial.Remove(dbItem);
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
        public override bool UpdateData(int id, ref DTO.SubMaterialMng.SubMaterial dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
        public bool UpdateDataSub(int id, ref DTO.SubMaterialMng.SubMaterial dtoItem,int iRequesterID, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            int number;
            string indexName;

            try
            {
                using (var context = CreateContext())
                {
                    SubMaterial subMaterial = null;

                    if (id == 0)
                    {
                        subMaterial = new SubMaterial();

                        context.SubMaterial.Add(subMaterial);
                        subMaterial.UpdatedBy = iRequesterID;
                        subMaterial.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        var item = context.SubMaterialMng_SubMaterialCheck_View.Where(o => o.SubMaterialID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't update because it used in item other!");
                        }
                        subMaterial = context.SubMaterial.FirstOrDefault(o => o.SubMaterialID == id);
                        subMaterial.UpdatedBy = iRequesterID;
                        subMaterial.UpdatedDate = DateTime.Now;
                    }

                    if (subMaterial == null)
                    {
                        notification.Message = "Sub Material not found!";

                        return false;
                    }
                    else
                    {
                        converter.DTO2BD(dtoItem, ref subMaterial);

                        if (id <= 0)
                        {
                            // Generate code.
                            using (var trans = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM SubMaterial WITH (TABLOCKX, HOLDLOCK)");

                                try
                                {
                                    var newCode = context.SubMaterialMng_function_GenerateCode().FirstOrDefault();

                                    if (!"**".Equals(newCode))
                                    {
                                        subMaterial.SubMaterialUD = newCode;

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

                        dtoItem = GetData(subMaterial.SubMaterialID, out notification).Data;

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
                    if ("SubMaterialUDUnique".Equals(indexName))
                        notification.Message = "The Sub Material Code is already exists.";
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

        public override bool Approve(int id, ref DTO.SubMaterialMng.SubMaterial dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.SubMaterialMng.SubMaterial dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
