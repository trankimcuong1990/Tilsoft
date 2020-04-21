using Library;
using Library.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FrameMaterialColorMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.FrameMaterialColorMng.SearchFormData, DTO.FrameMaterialColorMng.EditFormData, DTO.FrameMaterialColorMng.FrameMaterialColor>
    {
        private DataConverter converter = new DataConverter();
        private FrameMaterialColorMngEntities CreateContext()
        {
            return new FrameMaterialColorMngEntities(DALBase.Helper.CreateEntityConnectionString("FrameMaterialColorMngModel"));
        }

        public override DTO.FrameMaterialColorMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FrameMaterialColorMng.SearchFormData data = new DTO.FrameMaterialColorMng.SearchFormData();
            data.Data = new List<DTO.FrameMaterialColorMng.FrameMaterialColorSearchResult>();
            totalRows = 0;

            string FrameMaterialColorUD = null;
            string FrameMaterialColorNM = null;
            if (filters.ContainsKey("FrameMaterialColorUD") && !string.IsNullOrEmpty(filters["FrameMaterialColorUD"].ToString()))
            {
                FrameMaterialColorUD = filters["FrameMaterialColorUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FrameMaterialColorNM") && !string.IsNullOrEmpty(filters["FrameMaterialColorNM"].ToString()))
            {
                FrameMaterialColorNM = filters["FrameMaterialColorNM"].ToString().Replace("'", "''");
            }


            //try to get data
            try
            {
                using (FrameMaterialColorMngEntities context = CreateContext())
                {
                    totalRows = context.FrameMaterialColorMng_function_SearchFrameMaterialColor(FrameMaterialColorUD, FrameMaterialColorNM, orderBy, orderDirection).Count();
                    var result = context.FrameMaterialColorMng_function_SearchFrameMaterialColor(FrameMaterialColorUD, FrameMaterialColorNM, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_FrameMaterialColorSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.FrameMaterialColorMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FrameMaterialColorMng.EditFormData data = new DTO.FrameMaterialColorMng.EditFormData();
            data.Data = new DTO.FrameMaterialColorMng.FrameMaterialColor();

            //try to get data
            try
            {
                using (FrameMaterialColorMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_FrameMaterialColor(context.FrameMaterialColorMng_FrameMaterialColor_View.FirstOrDefault(o => o.FrameMaterialColorID == id));
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
                using (FrameMaterialColorMngEntities context = CreateContext())
                {
                    FrameMaterialColor dbItem = context.FrameMaterialColor.FirstOrDefault(o => o.FrameMaterialColorID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Frame material color not found!";
                        return false;
                    }
                    else
                    {
                        context.FrameMaterialColor.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.FrameMaterialColorMng.FrameMaterialColor dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            int number;
            string indexName;

            try
            {
                using (var context = CreateContext())
                {
                    FrameMaterialColor frameMaterialColor = null;

                    if (id == 0)
                    {
                        frameMaterialColor = new FrameMaterialColor();

                        context.FrameMaterialColor.Add(frameMaterialColor);
                    }
                    else
                    {
                        frameMaterialColor = context.FrameMaterialColor.FirstOrDefault(o => o.FrameMaterialColorID == id);
                    }

                    if (frameMaterialColor == null)
                    {
                        notification.Message = "Frame Material Color not found!";

                        return false;
                    }
                    else
                    {
                        converter.DTO2BD(dtoItem, ref frameMaterialColor);

                        if (id <= 0)
                        {
                            // Generate code.
                            using (var trans = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM FrameMaterialColor WITH (TABLOCKX, HOLDLOCK)");

                                try
                                {
                                    var newCode = context.FrameMaterialColorMng_function_GenerateCode().FirstOrDefault();

                                    if (!"**".Equals(newCode))
                                    {
                                        frameMaterialColor.FrameMaterialColorUD = newCode;

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

                        dtoItem = GetData(frameMaterialColor.FrameMaterialColorID, out notification).Data;

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
                    if ("FrameMaterialColorUDUnique".Equals(indexName))
                        notification.Message = "The Frame Material Color Code is already exists.";
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
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (FrameMaterialColorMngEntities context = CreateContext())
            //    {
            //        FrameMaterialColor dbItem = null;
            //        if (id == 0)
            //        {
            //            dbItem = new FrameMaterialColor();
            //            context.FrameMaterialColor.Add(dbItem);
            //        }
            //        else
            //        {
            //            dbItem = context.FrameMaterialColor.FirstOrDefault(o => o.FrameMaterialColorID == id);
            //        }

            //        if (dbItem == null)
            //        {
            //            notification.Message = "Frame material color not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            converter.DTO2BD(dtoItem, ref dbItem);
            //            context.SaveChanges();

            //            dtoItem = GetData(dbItem.FrameMaterialColorID, out notification).Data;

            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    return false;
            //}
        }

        public override bool Approve(int id, ref DTO.FrameMaterialColorMng.FrameMaterialColor dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.FrameMaterialColorMng.FrameMaterialColor dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
