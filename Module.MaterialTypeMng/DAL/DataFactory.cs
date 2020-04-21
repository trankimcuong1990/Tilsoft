using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.DTO;

namespace Module.MaterialTypeMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();

        private MaterialTypeMngEntities CreateContext()
        {
            return new MaterialTypeMngEntities(Library.Helper.CreateEntityConnectionString("DAL.MaterialTypeMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.MaterialTypeSearchResult>();
            totalRows = 0;

            string MaterialTypeUD = null;
            string MaterialTypeNM = null;
            if (filters.ContainsKey("MaterialTypeUD") && !string.IsNullOrEmpty(filters["MaterialTypeUD"].ToString()))
            {
                MaterialTypeUD = filters["MaterialTypeUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("MaterialTypeNM") && !string.IsNullOrEmpty(filters["MaterialTypeNM"].ToString()))
            {
                MaterialTypeNM = filters["MaterialTypeNM"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (MaterialTypeMngEntities context = CreateContext())
                {
                    totalRows = context.MaterialTypeMng_function_SearchMaterialType(MaterialTypeUD, MaterialTypeNM, orderBy, orderDirection).Count();
                    var result = context.MaterialTypeMng_function_SearchMaterialType(MaterialTypeUD, MaterialTypeNM, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_MaterialTypeSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.MaterialTypes();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (MaterialTypeMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_MaterialType(context.MaterialTypeMng_MaterialType_View.FirstOrDefault(o => o.MaterialTypeID == id));
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
                using (MaterialTypeMngEntities context = CreateContext())
                {
                    MaterialType dbItem = context.MaterialType.FirstOrDefault(o => o.MaterialTypeID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Material Type not found!";
                        return false;
                    }
                    else
                    {
                        var item = context.MaterialTypeMng_MaterialTypeCheck_View.Where(o => o.MaterialTypeID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't delete because it used in item other!");
                        }
                        context.MaterialType.Remove(dbItem);
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

        public bool UpdateData(int id, ref DTO.MaterialTypes dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int id, ref object dtoItem, int userId, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            int number;
            string indexName;

            try
            {
                using (var context = CreateContext())
                {
                    MaterialType materialType = null;

                    if (id == 0)
                    {
                        materialType = new MaterialType();

                        context.MaterialType.Add(materialType);
                        materialType.UpdatedBy = userId;
                        materialType.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        var item = context.MaterialTypeMng_MaterialTypeCheck_View.Where(o => o.MaterialTypeID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't update because it used in item other!");
                        }
                        materialType = context.MaterialType.FirstOrDefault(o => o.MaterialTypeID == id);
                        materialType.UpdatedBy = userId;
                        materialType.UpdatedDate = DateTime.Now;
                    }

                    DTO.MaterialTypes dtoMaterialType = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.MaterialTypes>();

                    if (materialType == null)
                    {
                        notification.Message = "Material Type not found!";

                        return false;
                    }
                    else
                    {
                        //hangtag image
                        Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                        // Fixed bug check dtoItem.HangTagFileHasChange has value after get value - 21 FEB 2018.
                        if (dtoMaterialType.HangTagFileHasChange.HasValue)
                        {
                            dtoMaterialType.HangTagFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoMaterialType.NewHangTagFile, dtoMaterialType.HangTagFile);
                        }
                        converter.DTO2BD_MaterialType(dtoMaterialType, ref materialType);
                        if (id <= 0)
                        {
                            // Generate code.
                            using (var trans = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM MaterialType WITH (TABLOCKX, HOLDLOCK)");

                                try
                                {
                                    var newCode = context.MaterialTypeMng_function_GenerateCode().FirstOrDefault();

                                    if (!"**".Equals(newCode))
                                    {
                                        materialType.MaterialTypeUD = newCode;

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

                        dtoItem = GetData(materialType.MaterialTypeID, out notification).Data;

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
                    if ("MaterialTypeUDUnique".Equals(indexName))
                        notification.Message = "The Material Type Code is already exists.";
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

        public bool Approve(int id, ref DTO.MaterialTypes dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int id, ref DTO.MaterialTypes dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
