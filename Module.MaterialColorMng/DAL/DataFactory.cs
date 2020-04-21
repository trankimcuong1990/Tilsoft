using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.DTO;
using Module.MaterialColorMng.DTO;

namespace Module.MaterialColorMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private MaterialColorMngEntities CreateContext()
        {
            return new MaterialColorMngEntities(Library.Helper.CreateEntityConnectionString("DAL.MaterialColorMngModel"));
        }

        public DTO.SearchFormData GetDataWithFilters(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.MaterialColorSearchResultData>();
            totalRows = 0;

            string MaterialColorUD = null;
            string MaterialColorNM = null;
            if (filters.ContainsKey("MaterialColorUD") && !string.IsNullOrEmpty(filters["MaterialColorUD"].ToString()))
            {
                MaterialColorUD = filters["MaterialColorUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("MaterialColorNM") && !string.IsNullOrEmpty(filters["MaterialColorNM"].ToString()))
            {
                MaterialColorNM = filters["MaterialColorNM"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (MaterialColorMngEntities context = CreateContext())
                {
                    totalRows = context.MaterialColorMng_function_SearchMaterialColor(MaterialColorUD, MaterialColorNM, orderBy, orderDirection).Count();
                    var result = context.MaterialColorMng_function_SearchMaterialColor(MaterialColorUD, MaterialColorNM, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_MaterialColorSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetEditData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.MaterialColorData();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (MaterialColorMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_MaterialColor(context.MaterialColorMng_MaterialColor_View.FirstOrDefault(o => o.MaterialColorID == id));
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

        public bool Delete(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (MaterialColorMngEntities context = CreateContext())
                {
                    MaterialColor dbItem = context.MaterialColor.FirstOrDefault(o => o.MaterialColorID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "MaterialColor not found!";
                        return false;
                    }
                    else
                    {
                        var item = context.MaterialColorMng_MaterialColorCheck_View.Where(o => o.MaterialColorID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't delete because it used in item other!");
                        }
                        context.MaterialColor.Remove(dbItem);
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

        public bool Update(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;
            DTO.MaterialColorData dtoItems = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.MaterialColorData>();

            int number;
            string indexName;

            try
            {
                using (var context = CreateContext())
                {
                    MaterialColor materialColor = null;

                    if (id == 0)
                    {
                        materialColor = new MaterialColor();

                        context.MaterialColor.Add(materialColor);
                        materialColor.UpdatedBy = userId;
                        materialColor.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        var item = context.MaterialColorMng_MaterialColorCheck_View.Where(o => o.MaterialColorID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't update because it used in item other!");
                        }
                        materialColor = context.MaterialColor.FirstOrDefault(o => o.MaterialColorID == id);
                        materialColor.UpdatedBy = userId;
                        materialColor.UpdatedDate = DateTime.Now;
                    }

                    if (materialColor == null)
                    {
                        notification.Message = "Material Color not found!";

                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_MaterialColor(dtoItems, ref materialColor);

                        if (id <= 0)
                        {
                            // Generate code.
                            using (var trans = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM MaterialColor WITH (TABLOCKX, HOLDLOCK)");

                                try
                                {
                                    var newCode = context.MaterialColorMng_function_GenerateCode().FirstOrDefault();

                                    if (!"***".Equals(newCode))
                                    {
                                        materialColor.MaterialColorUD = newCode;

                                        context.SaveChanges();
                                    }
                                    else
                                    {
                                        notification.Type = NotificationType.Error;
                                        notification.Message = "Auto generated code exceed maximum option: [ZZZ]";
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

                        dtoItem = GetEditData(materialColor.MaterialColorID, out notification).Data;

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
                    if ("MaterialColorUDUnique".Equals(indexName))
                        notification.Message = "The Material Color Code is already exists.";
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

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
