using Library;
using Library.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MaterialColorMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.MaterialColorMng.SearchFormData, DTO.MaterialColorMng.EditFormData, DTO.MaterialColorMng.MaterialColor>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private MaterialColorMngEntities CreateContext()
        {
            return new MaterialColorMngEntities(DALBase.Helper.CreateEntityConnectionString("MaterialColorMngModel"));
        }


        public override DTO.MaterialColorMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MaterialColorMng.SearchFormData data = new DTO.MaterialColorMng.SearchFormData();
            data.Data = new List<DTO.MaterialColorMng.MaterialColorSearchResult>();
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

        public override DTO.MaterialColorMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MaterialColorMng.EditFormData data = new DTO.MaterialColorMng.EditFormData();
            data.Data = new DTO.MaterialColorMng.MaterialColor();

            //try to get data
            try
            {
                using (MaterialColorMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_MaterialColor(context.MaterialColorMng_MaterialColor_View.FirstOrDefault(o => o.MaterialColorID == id));
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

        public override bool UpdateData(int id, ref DTO.MaterialColorMng.MaterialColor dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

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
                    }
                    else
                    {
                        materialColor = context.MaterialColor.FirstOrDefault(o => o.MaterialColorID == id);
                    }

                    if (materialColor == null)
                    {
                        notification.Message = "Material Color not found!";

                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_MaterialColor(dtoItem, ref materialColor);

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

                        dtoItem = GetData(materialColor.MaterialColorID, out notification).Data;

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

        public override bool Approve(int id, ref DTO.MaterialColorMng.MaterialColor dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.MaterialColorMng.MaterialColor dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}