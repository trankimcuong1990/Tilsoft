using System.Collections;
using Library.DTO;
using Module.OPSequence.DTO;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Module.OPSequence.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchData, DTO.EditData>
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        #region Generate functions and methods

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public override EditData GetData(int id, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public override SearchData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private DataConverter converter = new DataConverter();

        public OPSequenceEntities CreateContext()
        {
            return new OPSequenceEntities(Library.Helper.CreateEntityConnectionString("DAL.OPSequenceModel"));
        }

        public DTO.SearchData Search(int userId, Hashtable filter, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification() { Type = NotificationType.Success };
            DTO.SearchData data = new SearchData() { Data = new List<OPSequenceSearchResultDto>() };

            try
            {
                using (var context = this.CreateContext())
                {
                    string opSequenceNm = filter["OPSequenceNM"]?.ToString().Replace("'", "''");
                    string companyID = fwFactory.GetCompanyID(userId).HasValue ? fwFactory.GetCompanyID(userId).Value.ToString() : null;
                    //var result = context.EmployeeMng_Employee_View.FirstOrDefault(o => o.UserID == userId);
                    //if (result != null)
                    //    companyID = result.CompanyID.ToString();

                    totalRows = context.OPSequence_function_OPSequenceSearchResult(opSequenceNm, companyID, orderBy, orderDirection).Count();
                    data.Data = converter.DB2DTO_Search(context.OPSequence_function_OPSequenceSearchResult(opSequenceNm, companyID, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return data;
            }

            return data;
        }

        public DTO.EditData Get(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.EditData data = new EditData() { Data = new OPSequenceDto() };

            try
            {
                if (id > 0)
                {
                    using (OPSequenceEntities context = CreateContext())
                    {
                        OPSequence_OPSequence_View dbItem = context.OPSequence_OPSequence_View.FirstOrDefault(s => s.OPSequenceID == id);

                        if (dbItem == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data." };
                            return data;
                        }

                        data.Data = converter.DB2DTO_Get(dbItem);
                    }
                }
                else
                {
                    data.Data.OPSequenceDetails = new List<OPSequenceDetailDto>();
                }

                Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                data.WorkCenters = support_factory.GetWorkCenter();
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return data;
            }

            return data;
        }

        public bool Update(int userId, int id, ref object dto, out Notification notification)
        {
            DTO.OPSequenceDto dtoItem = ((Newtonsoft.Json.Linq.JObject)dto).ToObject<DTO.OPSequenceDto>();
            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using (OPSequenceEntities context = CreateContext())
                {
                    OPSequence dbItem;

                    if (id > 0)
                    {
                        if (this.HasBOM(id))
                        {
                            throw new Exception("There are some BOM which are using OP Sequence, so you can not change !!!");
                        }

                        dbItem = context.OPSequence.FirstOrDefault(s => s.OPSequenceID == id);

                        if (dbItem == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data" };
                            return false;
                        }
                    }
                    else
                    {
                        dbItem = new OPSequence();
                        context.OPSequence.Add(dbItem);
                    }
                    converter.DTO2DB_Update(dtoItem, ref dbItem);
                    dbItem.CompanyID = fwFactory.GetCompanyID(userId);
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;
                    context.OPSequenceDetail.Local.Where(o => o.OPSequence == null).ToList().ForEach(o => context.OPSequenceDetail.Remove(o));
                    context.SaveChanges();
                    dto = Get(dbItem.OPSequenceID, out notification).Data;
                }

                return true;
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }
        }

        public bool Delete(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using (OPSequenceEntities context = CreateContext())
                {
                    OPSequence dbItem = context.OPSequence.FirstOrDefault(s => s.OPSequenceID == id);

                    if (dbItem == null)
                    {
                        notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data." };
                        return false;
                    }

                    context.OPSequence.Remove(dbItem);
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }
        }

        private bool HasBOM(int opSequenceID)
        {
            try
            {
                using (OPSequenceEntities context = CreateContext())
                {
                    return context.OPSequence_function_HasBOM(opSequenceID).FirstOrDefault().Value > 0;
                }
            }
            catch { }
            return false;
            
        }
    }
}
