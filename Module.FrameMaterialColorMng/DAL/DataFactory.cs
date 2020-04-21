using Library;
using Library.DTO;
using Module.FrameMaterialColorMng.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FrameMaterialColorMng.DAL
{
   public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        public DataFactory(string tempFolder)
        {

        }

        public DataFactory()
        {
        }

        private FrameMaterialColorMngEntities CreateContext()
        {
            return new FrameMaterialColorMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FrameMaterialColorMngModel"));
        }
        public override SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            SearchFormData data = new SearchFormData();
            data.Data = new List<FrameMaterialColorSearchResult>();
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

        public override EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            EditFormData data = new EditFormData();
            data.Data = new FrameMaterialColorDTO();
            //try to get data
            try
            {
                if (id > 0)
                {
                    using (FrameMaterialColorMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_FrameMaterialColor(context.FrameMaterialColorMng_FrameMaterialColor_View.FirstOrDefault(o => o.FrameMaterialColorID == id));
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
                        var item = context.FrameMaterialColorMng_FrameMaterialColorCheck_View.Where(o => o.FrameMaterialColorID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't delete because it used in item other!");
                        }
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

        public bool UpdateDataFrameMaterialColor(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            FrameMaterialColorDTO dtoFrameMaterialColorDTO  = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<FrameMaterialColorDTO>();
            notification = new Notification();
            notification.Type = NotificationType.Success;
            int number;
            string indexName;                     
            
            try
            {
                // Vailidation
                ValidationContext validationDTO  = new ValidationContext(dtoFrameMaterialColorDTO, null, null);
                List<ValidationResult> results = new List<ValidationResult>();
                bool valid = Validator.TryValidateObject(dtoFrameMaterialColorDTO, validationDTO, results, true);
                if (!valid)
                {
                    string error = "";
                    List<string> detailError = new List<string>();
                    foreach (ValidationResult vr in results)
                    {
                        notification.Type = NotificationType.Error;
                        error = vr.ErrorMessage;
                        detailError.Add(error);                        
                    }
                    notification.Type = NotificationType.Error;
                    notification.DetailMessage.AddRange(detailError);
                    return false;
                }

                using (var context = CreateContext())
                {
                    FrameMaterialColor frameMaterialColor = null;

                    if (id == 0)
                    {
                        frameMaterialColor = new FrameMaterialColor();
                        context.FrameMaterialColor.Add(frameMaterialColor);
                        frameMaterialColor.UpdatedBy = userId;
                        frameMaterialColor.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        var item = context.FrameMaterialColorMng_FrameMaterialColorCheck_View.Where(o => o.FrameMaterialColorID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't update because it used in item other!");
                        }
                        frameMaterialColor = context.FrameMaterialColor.FirstOrDefault(o => o.FrameMaterialColorID == id);
                        frameMaterialColor.UpdatedBy = userId;
                        frameMaterialColor.UpdatedDate = DateTime.Now;
                    }

                    if (frameMaterialColor == null)
                    {
                        notification.Message = "Frame Material Color not found!";

                        return false;
                    }
                    else
                    {
                        converter.DTO2BD(dtoFrameMaterialColorDTO, ref frameMaterialColor);

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
