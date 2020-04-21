using Library;
using Library.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MaterialTypeMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.MaterialTypeMng.SearchFormData, DTO.MaterialTypeMng.EditFormData, DTO.MaterialTypeMng.MaterialType>
    {
        private DataConverter converter = new DataConverter();
        private Support.DataFactory supportFactory = new Support.DataFactory();

        private MaterialTypeMngEntities CreateContext()
        {
            return new MaterialTypeMngEntities(DALBase.Helper.CreateEntityConnectionString("MaterialTypeMngModel"));
        }

        public override DTO.MaterialTypeMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MaterialTypeMng.SearchFormData data = new DTO.MaterialTypeMng.SearchFormData();
            data.Data = new List<DTO.MaterialTypeMng.MaterialTypeSearchResult>();
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

        public override DTO.MaterialTypeMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MaterialTypeMng.EditFormData data = new DTO.MaterialTypeMng.EditFormData();
            data.Data = new DTO.MaterialTypeMng.MaterialType();

            //try to get data
            try
            {
                using (MaterialTypeMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_MaterialType(context.MaterialTypeMng_MaterialType_View.FirstOrDefault(o => o.MaterialTypeID == id));
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

        public override bool UpdateData(int id, ref DTO.MaterialTypeMng.MaterialType dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public  bool UpdateData(int id, ref DTO.MaterialTypeMng.MaterialType dtoItem,int userId, out Library.DTO.Notification notification)
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
                    }
                    else
                    {
                        materialType = context.MaterialType.FirstOrDefault(o => o.MaterialTypeID == id);
                    }

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
                        if (dtoItem.HangTagFileHasChange.HasValue)
                        {
                            dtoItem.HangTagFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoItem.NewHangTagFile, dtoItem.HangTagFile);
                        }
                        converter.DTO2BD_MaterialType(dtoItem, ref materialType);
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

        public override bool Approve(int id, ref DTO.MaterialTypeMng.MaterialType dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.MaterialTypeMng.MaterialType dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}