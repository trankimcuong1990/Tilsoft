using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL.DDCMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.DDCMng.SearchFormData, DTO.DDCMng.EditFormData, DTO.DDCMng.DDC>
    {
        private DataConverter converter = new DataConverter();
        private Support.DataFactory supportFactory = new Support.DataFactory();

        private DDCMngEntities CreateContext()
        {
            return new DDCMngEntities(DALBase.Helper.CreateEntityConnectionString("DDCMngModel"));
        }

        public override DTO.DDCMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.DDCMng.SearchFormData data = new DTO.DDCMng.SearchFormData();
            data.Data = new List<DTO.DDCMng.DDCSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (DDCMngEntities context = CreateContext())
                {
                    totalRows = context.DDCMng_function_SearchDDC(orderBy, orderDirection).Count();
                    var result = context.DDCMng_function_SearchDDC(orderBy, orderDirection);
                    data.Data = converter.DB2DTO_DDCSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.DDCMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DDCMngEntities context = CreateContext())
                {
                    DDC dbItem = context.DDC.FirstOrDefault(o => o.DDCID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "DDC not found!";
                        return false;
                    }
                    else
                    {
                        context.DDC.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.DDCMng.DDC dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DDCMngEntities context = CreateContext())
                {
                    DDC dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new DDC();
                        context.DDC.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.DDC.FirstOrDefault(o => o.DDCID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "DDC not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD(dtoItem, ref dbItem);
                        context.DDCDetail.Local.Where(o => o.DDC == null).ToList().ForEach(o => context.DDCDetail.Remove(o));
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.DDCID, string.Empty, out notification).Data;

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

        public override bool Approve(int id, ref DTO.DDCMng.DDC dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.DDCMng.DDC dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.DDCMng.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.DDCMng.InitFormData data = new DTO.DDCMng.InitFormData();
            data.Seasons = new List<DTO.Support.Season>();
            try
            {
                using (DDCMngEntities context = CreateContext())
                {
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

        public DTO.DDCMng.EditFormData GetData(int id, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.DDCMng.EditFormData data = new DTO.DDCMng.EditFormData();
            data.Data = new DTO.DDCMng.DDC();
            data.Data.Details = new List<DTO.DDCMng.DDCDetail>();

            //try to get data
            try
            {
                using (DDCMngEntities context = CreateContext())
                {
                    if (id <= 0)
                    {
                        data.Data.Season = season;
                        foreach (DTO.Support.Client dtoClient in supportFactory.GetClient())
                        {
                            data.Data.Details.Add(new DTO.DDCMng.DDCDetail() { ClientID = dtoClient.ClientID, ClientUD = dtoClient.ClientUD, ClientNM = dtoClient.ClientNM});
                        }
                    }
                    else 
                    {
                        data.Data = converter.DB2DTO_DDC(context.DDCMng_DDC_View.FirstOrDefault(o => o.DDCID == id));
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
    }
}
