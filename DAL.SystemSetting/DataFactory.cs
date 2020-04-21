using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MaterialMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.MaterialMng.SearchFormData, DTO.MaterialMng.EditFormData, DTO.MaterialMng.Material>
    {
        private DataConverter converter = new DataConverter();
        private MaterialMngEntities CreateContext()
        {
            return new MaterialMngEntities(DALBase.Helper.CreateEntityConnectionString("MaterialMngModel"));
        }

        public override DTO.MaterialMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out DTO.Framework.Notification notification)
        {
            notification = new DTO.Framework.Notification() { Type = DTO.Framework.NotificationType.Success };
            DTO.MaterialMng.SearchFormData data = new DTO.MaterialMng.SearchFormData();
            data.Data = new List<DTO.MaterialMng.MaterialSearchResult>();
            totalRows = 0;

            string MaterialUD = null;
            string MaterialNM = null;
            if (filters.ContainsKey("MaterialUD") && !string.IsNullOrEmpty(filters["MaterialUD"].ToString()))
            {
                MaterialUD = filters["MaterialUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("MaterialNM") && !string.IsNullOrEmpty(filters["MaterialNM"].ToString()))
            {
                MaterialNM = filters["MaterialNM"].ToString().Replace("'", "''");
            }


            //try to get data
            try
            {
                using (MaterialMngEntities context = CreateContext())
                {
                    totalRows = context.MaterialMng_function_SearchMaterial(MaterialUD, MaterialNM, orderBy, orderDirection).Count();
                    var result = context.MaterialMng_function_SearchMaterial(MaterialUD, MaterialNM, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_MaterialSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = DTO.Framework.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.MaterialMng.EditFormData GetData(int id, out DTO.Framework.Notification notification)
        {
            notification = new DTO.Framework.Notification() { Type = DTO.Framework.NotificationType.Success };
            DTO.MaterialMng.EditFormData data = new DTO.MaterialMng.EditFormData();
            data.Data = new DTO.MaterialMng.Material();

            //try to get data
            try
            {
                using (MaterialMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_Material(context.MaterialMng_Material_View.FirstOrDefault(o => o.MaterialID == id));
                }
            }
            catch (Exception ex)
            {
                notification.Type = DTO.Framework.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out DTO.Framework.Notification notification)
        {
            notification = new DTO.Framework.Notification() { Type = DTO.Framework.NotificationType.Success };

            try
            {
                using (MaterialMngEntities context = CreateContext())
                {
                    Material dbItem = context.Material.FirstOrDefault(o => o.MaterialID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Material not found!";
                        return false;
                    }
                    else
                    {
                        context.Material.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = DTO.Framework.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.MaterialMng.Material dtoItem, out DTO.Framework.Notification notification)
        {
            notification = new DTO.Framework.Notification() { Type = DTO.Framework.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                using (MaterialMngEntities context = CreateContext())
                {
                    Material dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Material();
                        context.Material.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Material.FirstOrDefault(o => o.MaterialID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Material not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_Material(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.MaterialID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (System.Data.DataException dEx)
            {
                notification.Type = DTO.Framework.NotificationType.Error;
                Library.ErrorHelper.DataExceptionParser(dEx, out number, out indexName);
                if (number == 2601 && !string.IsNullOrEmpty(indexName))
                {
                    if (indexName == "MaterialUDUnique")
                    {
                        notification.Message = "The Material Code is already exists";
                    }
                }
                else
                {
                    notification.Message = dEx.Message;
                }

                return false;
            }
            catch (Exception ex)
            {
                notification.Type = DTO.Framework.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Approve(int id, ref DTO.MaterialMng.Material dtoItem, out DTO.Framework.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.MaterialMng.Material dtoItem, out DTO.Framework.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
