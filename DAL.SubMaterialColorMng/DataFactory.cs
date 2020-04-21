using Library;
using Library.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SubMaterialColorMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.SubMaterialColorMng.SearchFormData, DTO.SubMaterialColorMng.EditFormData, DTO.SubMaterialColorMng.SubMaterialColor>
    {
        private DataConverter converter = new DataConverter();
        private SubMaterialColorMngEntities CreateContext()
        {
            return new SubMaterialColorMngEntities(DALBase.Helper.CreateEntityConnectionString("SubMaterialColorMngModel"));
        }

        public override DTO.SubMaterialColorMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SubMaterialColorMng.SearchFormData data = new DTO.SubMaterialColorMng.SearchFormData();
            data.Data = new List<DTO.SubMaterialColorMng.SubMaterialColorSearchResult>();
            totalRows = 0;

            string SubMaterialColorUD = null;
            string SubMaterialColorNM = null;
            if (filters.ContainsKey("SubMaterialColorUD") && !string.IsNullOrEmpty(filters["SubMaterialColorUD"].ToString()))
            {
                SubMaterialColorUD = filters["SubMaterialColorUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("SubMaterialColorNM") && !string.IsNullOrEmpty(filters["SubMaterialColorNM"].ToString()))
            {
                SubMaterialColorNM = filters["SubMaterialColorNM"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (SubMaterialColorMngEntities context = CreateContext())
                {
                    totalRows = context.SubMaterialColorMng_function_SearchSubMaterialColor(SubMaterialColorUD, SubMaterialColorNM, orderBy, orderDirection).Count();
                    var result = context.SubMaterialColorMng_function_SearchSubMaterialColor(SubMaterialColorUD, SubMaterialColorNM, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SubMaterialColorSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.SubMaterialColorMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SubMaterialColorMng.EditFormData data = new DTO.SubMaterialColorMng.EditFormData();
            data.Data = new DTO.SubMaterialColorMng.SubMaterialColor();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (SubMaterialColorMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_SubMaterialColor(context.SubMaterialColorMng_SubMaterialColor_View.FirstOrDefault(o => o.SubMaterialColorID == id));
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
                using (SubMaterialColorMngEntities context = CreateContext())
                {
                    SubMaterialColor dbItem = context.SubMaterialColor.FirstOrDefault(o => o.SubMaterialColorID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sub material not found!";
                        return false;
                    }
                    else
                    {
                        var item = context.SubMaterialColorMng_SubMaterialColorCheck_View.Where(o => o.SubMaterialColorID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't delete because it used in item other!");
                        }
                        context.SubMaterialColor.Remove(dbItem);
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
        public override bool UpdateData(int id, ref DTO.SubMaterialColorMng.SubMaterialColor dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
        public bool UpdateDataSub(int id, ref DTO.SubMaterialColorMng.SubMaterialColor dtoItem,int iRequesterID, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            int number;
            string indexName;

            try
            {
                using (var context = CreateContext())
                {
                    SubMaterialColor subMaterialColor = null;

                    if (id == 0)
                    {
                        subMaterialColor = new SubMaterialColor();

                        context.SubMaterialColor.Add(subMaterialColor);
                        subMaterialColor.UpdatedBy = iRequesterID;
                        subMaterialColor.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        var item = context.SubMaterialColorMng_SubMaterialColorCheck_View.Where(o => o.SubMaterialColorID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't update because it used in item other!");
                        }
                        subMaterialColor = context.SubMaterialColor.FirstOrDefault(o => o.SubMaterialColorID == id);
                        subMaterialColor.UpdatedBy = iRequesterID;
                        subMaterialColor.UpdatedDate = DateTime.Now;
                    }

                    if (subMaterialColor == null)
                    {
                        notification.Message = "Sub Material Color not found!";

                        return false;
                    }
                    else
                    {
                        converter.DTO2BD(dtoItem, ref subMaterialColor);

                        if (id <= 0)
                        {
                            // Generate code.
                            using (var trans = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM SubMaterialColor WITH (TABLOCKX, HOLDLOCK)");

                                try
                                {
                                    var newCode = context.SubMaterialColorMng_function_GenerateCode().FirstOrDefault();

                                    if (!"**".Equals(newCode))
                                    {
                                        subMaterialColor.SubMaterialColorUD = newCode;

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

                        dtoItem = GetData(subMaterialColor.SubMaterialColorID, out notification).Data;

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
                    if ("SubMaterialColorUDUnique".Equals(indexName))
                        notification.Message = "The Sub Material Color Code is already exists.";
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

        public override bool Approve(int id, ref DTO.SubMaterialColorMng.SubMaterialColor dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.SubMaterialColorMng.SubMaterialColor dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
