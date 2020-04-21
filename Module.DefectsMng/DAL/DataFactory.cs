using Library.DTO;
using Module.DefectsMng.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DefectsMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private string _TempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        
        private DefectsEntities CreateContext()
        {
            return new DefectsEntities(Library.Helper.CreateEntityConnectionString("DAL.DefectsModel"));
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;

            notification = new Notification() { Type = NotificationType.Success };

            SearchFormData data = new SearchFormData() { Data = new List<DefectsDTO>() };

            try
            {
                using (var context = CreateContext())
                {                   
                    string defectNM = null;
                    string defectUD = null;
                    string defectName = null;
                    string typeOfDefectNameA = null;
                    string typeOfDefectNameB = null;
                    string typeOfDefectNameC = null;
                    int? defectA = null;
                    int? defectB = null;
                    int? defectC = null;
                    int? defectGroupID = null;
                    
                    if (filters.ContainsKey("DefectNM") && !string.IsNullOrEmpty(filters["DefectNM"].ToString()))
                    {
                        defectNM = filters["DefectNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("DefectUD") && !string.IsNullOrEmpty(filters["DefectUD"].ToString()))
                    {
                        defectUD = filters["DefectUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("DefectName") && !string.IsNullOrEmpty(filters["DefectName"].ToString()))
                    {
                        defectName = filters["DefectName"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("TypeOfDefectNameA") && !string.IsNullOrEmpty(filters["TypeOfDefectNameA"].ToString()))
                    {
                        typeOfDefectNameA = filters["TypeOfDefectNameA"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("TypeOfDefectNameB") && !string.IsNullOrEmpty(filters["TypeOfDefectNameB"].ToString()))
                    {
                        typeOfDefectNameB = filters["TypeOfDefectNameB"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("TypeOfDefectNameC") && !string.IsNullOrEmpty(filters["TypeOfDefectNameC"].ToString()))
                    {
                        typeOfDefectNameC = filters["TypeOfDefectNameC"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("DefectA") && filters["DefectA"] != null)
                    {
                        defectA = Convert.ToInt32(filters["DefectA"]);
                    }
                    if (filters.ContainsKey("DefectB") && filters["DefectB"] != null)
                    {
                        defectB = Convert.ToInt32(filters["DefectB"]);
                    }
                    if (filters.ContainsKey("DefectC") && filters["DefectC"] != null)
                    {
                        defectC = Convert.ToInt32(filters["DefectC"]);
                    }
                    if (filters.ContainsKey("DefectGroupID") && filters["DefectGroupID"] != null)
                    {
                        defectGroupID = Convert.ToInt32(filters["DefectGroupID"]);
                    }

                    totalRows = context.DefectsMng_function_DefectsSearchResult(defectUD, defectNM,defectName,typeOfDefectNameA,typeOfDefectNameB, typeOfDefectNameC, defectA, defectB, defectC, defectGroupID, orderBy, orderDirection).Count();
                    data.Data = this.converter.BD2DTO_DefectsMngSearchResult(context.DefectsMng_function_DefectsSearchResult(defectUD, defectNM, defectName, typeOfDefectNameA, typeOfDefectNameB, typeOfDefectNameC, defectA, defectB, defectC, defectGroupID, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Notification()
                {
                    Type = NotificationType.Error,
                    Message = ex.Message

                };
            }
            return data;
        }
        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            EditFormData data = new EditFormData
            {
                DefectsDTO = new DefectsDTO()
            };

            try
            {
                if (id > 0)
                {
                    using (var context = CreateContext())
                    {
                        var item = context.DefectsMng_Defects_View.Include("DefectsMng_DefectsImage_View").FirstOrDefault(o => o.DefectID == id);
                        if (item == null)
                        {
                            notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                        }
                        else
                        {
                            data.DefectsDTO = this.converter.DB2DTO_DefectsDTO(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
            }
            return data;

        }
        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using (var Context = CreateContext())
                {
                    Defects unit = Context.Defects.FirstOrDefault(o => o.DefectID == id);

                    if (unit == null)
                    {
                        notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                        return false;
                    }

                    Context.Defects.Remove(unit);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }
        }
        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            DTO.DefectsDTO defetcsDTO = ((JObject)dtoItem).ToObject<DTO.DefectsDTO>();

            notification = new Notification { Type = NotificationType.Success };
            _TempFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";

            try
            {
                using (var context = CreateContext())
                {
                    Defects defects = null;

                    if (id == 0)
                    {
                        defects = new Defects();
                        context.Defects.Add(defects);
                    }

                    if (id > 0)
                    {
                        defects = context.Defects.FirstOrDefault(o => o.DefectID == id);

                        
                    }
                    if (defects == null)
                    {
                        notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                        return false;
                    }
                    else
                    {
                        //process tdfile
                        foreach (DTO.DefectsImageDTO image in defetcsDTO.defectsImageDTOs.Where(o => o.ScanHasChange))
                        {
                            image.FileUD = fwFactory.CreateFilePointer(this._TempFolder, image.ScanNewFile, image.FileUD);
                        }

                        this.converter.DTO2DB_Defects(defetcsDTO, ref defects);

                        //remove tdfile
                        foreach (DefectsImage dbDFImage in context.DefectsImage.Local.Where(o => o.Defects == null).ToList())
                        {
                            if (!string.IsNullOrEmpty(dbDFImage.FileUD))
                            {
                                fwFactory.RemoveImageFile(dbDFImage.FileUD);
                            }
                        }

                        //remove orphan
                        context.DefectsImage.Local.Where(o => o.Defects == null).ToList().ForEach(o => context.DefectsImage.Remove(o));

                        context.SaveChanges();

                        dtoItem = this.GetData(defects.DefectID, out notification);
                        return true;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }

        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = true;
            //            dbItem.ConfirmedBy = userId;
            //            dbItem.ConfirmedDate = DateTime.Now;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = false;
            //            dbItem.ConfirmedBy = null;
            //            dbItem.ConfirmedDate = null;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        //
        // CUSTOM FUNCTION HERE
        //

        public DTO.SupportFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SupportFormData data = new DTO.SupportFormData();
            try
            {
                using (DefectsEntities context = CreateContext())
                {
                    data.DefectsGroups = converter.DB2DTO_Defects(context.DefectsGroupMng_DefectsGroup_View.ToList());
                }
                using (DefectsEntities context = CreateContext())
                {
                    data.TypeOfDefectDTO = converter.DB2DTO_TypeOfDefects(context.Support_TypeOfDefects_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }
    }
}
