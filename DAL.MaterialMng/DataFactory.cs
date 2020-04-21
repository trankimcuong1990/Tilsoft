using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public override DTO.MaterialMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MaterialMng.SearchFormData data = new DTO.MaterialMng.SearchFormData();
            data.Data = new List<DTO.MaterialMng.MaterialSearchResult>();
            totalRows = 0;

            string MaterialUD = null;
            string MaterialNM = null;
            string Season = null;
            bool? IsStandard = null;

            if (filters.ContainsKey("materialUd") && !string.IsNullOrEmpty(filters["materialUd"].ToString()))
            {
                MaterialUD = filters["materialUd"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("materialNm") && !string.IsNullOrEmpty(filters["materialNm"].ToString()))
            {
                MaterialNM = filters["materialNm"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                Season = filters["Season"].ToString().Replace("'", "''");
            if (filters.ContainsKey("IsStandard") && !string.IsNullOrEmpty(filters["IsStandard"].ToString()))
                IsStandard = Convert.ToBoolean(filters["IsStandard"].ToString());

            //try to get data
            try
            {
                using (MaterialMngEntities context = CreateContext())
                {
                    totalRows = context.MaterialMng_function_SearchMaterial(MaterialUD, MaterialNM, Season, IsStandard, orderBy, orderDirection).Count();
                    var result = context.MaterialMng_function_SearchMaterial(MaterialUD, MaterialNM, Season, IsStandard, orderBy, orderDirection).ToList();
                    data.Data = converter.DB2DTO_MaterialSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.MaterialMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MaterialMng.EditFormData data = new DTO.MaterialMng.EditFormData();
            data.Data = new DTO.MaterialMng.Material();

            //try to get data
            try
            {
                if (id > 0) {
                    using (MaterialMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_Material(context.MaterialMng_Material_View.FirstOrDefault(o => o.MaterialID == id));
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
                        var item = context.MaterialMng_MaterialCheck_View.Where(o => o.MaterialID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't delete because it used in item other!");
                        }
                        context.Material.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.MaterialMng.Material dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
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

                        var item = context.MaterialMng_MaterialCheck_View.Where(o => o.MaterialID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't update because it used in item other!");
                        }
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Material not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_Material(dtoItem, ref dbItem);
                        if (id <= 0)
                        {
                            // generate code
                            using (DbContextTransaction scope = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM Material WITH (TABLOCKX, HOLDLOCK)");
                                try
                                {
                                    var newCode = context.MaterialMng_function_GenerateCode().FirstOrDefault();
                                    if (newCode != "**")
                                    {
                                        dbItem.MaterialUD = newCode;
                                    }
                                    else
                                    {
                                        throw new Exception("Auto generated code exceed maximum option: [ZZ]");
                                    }
                                    context.SaveChanges();
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
                        }
                        else
                        {
                            context.SaveChanges();
                        }                        
                        
                        dtoItem = GetData(dbItem.MaterialID, out notification).Data;

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
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Approve(int id, ref DTO.MaterialMng.Material dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.MaterialMng.Material dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
