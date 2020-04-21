using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CushionMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.CushionMng.SearchFormData, DTO.CushionMng.EditFormData, DTO.CushionMng.Cushion>
    {
        private DataConverter converter = new DataConverter();
        private Support.DataFactory supportFactory = new Support.DataFactory();
        private string _TempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }
        private CushionMngEntities CreateContext()
        {
            return new CushionMngEntities(DALBase.Helper.CreateEntityConnectionString("CushionMngModel"));
        }

        public override DTO.CushionMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.CushionMng.SearchFormData data = new DTO.CushionMng.SearchFormData();
            data.Data = new List<DTO.CushionMng.CushionSearchResult>();
            totalRows = 0;

            string CushionUD = null;
            string CushionNM = null;
            string Season = null;
            bool? IsStandard = null;
            if (filters.ContainsKey("CushionUD") && !string.IsNullOrEmpty(filters["CushionUD"].ToString()))
            {
                CushionUD = filters["CushionUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("CushionNM") && !string.IsNullOrEmpty(filters["CushionNM"].ToString()))
            {
                CushionNM = filters["CushionNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("IsStandard") && filters["IsStandard"] != null && !string.IsNullOrEmpty(filters["IsStandard"].ToString()))
            {
                IsStandard = (filters["IsStandard"].ToString() == "true") ? true : false;
            }

            //try to get data
            try
            {
                using (CushionMngEntities context = CreateContext())
                {
                    totalRows = context.CushionMng_function_SearchCushion(CushionUD, CushionNM, Season, IsStandard, orderBy, orderDirection).Count();
                    var result = context.CushionMng_function_SearchCushion(CushionUD, CushionNM, Season, IsStandard, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_CushionSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.CushionMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.CushionMng.EditFormData data = new DTO.CushionMng.EditFormData();
            data.Data = new DTO.CushionMng.Cushion();
            data.Seasons = new List<DTO.Support.Season>();

            //try to get data
            try
            {
                using (CushionMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_Cushion(context.CushionMng_Cushion_View.FirstOrDefault(o => o.CushionID == id));
                    data.Seasons = supportFactory.GetSeason().ToList();
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
                using (CushionMngEntities context = CreateContext())
                {
                    Cushion dbItem = context.Cushion.FirstOrDefault(o => o.CushionID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Cushion not found!";
                        return false;
                    }
                    else
                    {
                        context.Cushion.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.CushionMng.Cushion dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (CushionMngEntities context = CreateContext())
                {
                    Cushion dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Cushion();
                        context.Cushion.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Cushion.FirstOrDefault(o => o.CushionID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Cushion not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2BD(dtoItem, ref dbItem);
                        context.SaveChanges();

                        // processing image
                        if (dtoItem.ImageFile_HasChange)
                        {
                            dbItem.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ImageFile_NewFile, dtoItem.ImageFile);
                        }
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.CushionID, out notification).Data;

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

        public override bool Approve(int id, ref DTO.CushionMng.Cushion dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.CushionMng.Cushion dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.CushionMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.CushionMng.SearchFilterData data = new DTO.CushionMng.SearchFilterData();
            data.Seasons = new List<DTO.Support.Season>();
            data.YesNoValues = new List<DTO.Support.YesNo>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
                data.YesNoValues = supportFactory.GetYesNo().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
    }
}
