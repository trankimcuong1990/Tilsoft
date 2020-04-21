using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace Module.Sale.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SaleDTO, DTO.SaleDTO>
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private SaleEntities CreateContext()
        {
            return new SaleEntities(Library.Helper.CreateEntityConnectionString("DAL.SaleModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SaleEntities context = CreateContext())
                {
                    var dbItem = context.Sale.Where(o => o.SaleID == id).FirstOrDefault();
                    //context.Sale.Remove(dbItem);
                    dbItem.Visible = false;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }
            
        }

        public override DTO.SaleDTO GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SaleDTO dtoItem = new DTO.SaleDTO();
            try
            {
                using (SaleEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        Sale dbItem;
                        dbItem = context.Sale.FirstOrDefault(o => o.SaleID == id);
                        dtoItem = converter.DB2DTO_Sale(dbItem);
                        dtoItem.CompanyID = fwFactory.GetCompanyID(dbItem.UserID.Value);
                    }
                    else
                    {
                        dtoItem = new DTO.SaleDTO();
                    }
                    return dtoItem;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return null;
            }
        }

        public override DTO.SaleDTO GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SaleDTO dtoSale = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SaleDTO>();
            try
            {
                using (SaleEntities context = CreateContext())
                {
                    Sale dbItem = null;
                    if (id > 0)
                    {
                        dbItem = context.Sale.Where(o => o.SaleID == id).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new Sale();
                        context.Sale.Add(dbItem);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_Sale(dtoSale, ref dbItem);
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.SaleID, out notification);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }
        }

        public List<DTO.SaleDTO> Search(out Library.DTO.Notification notification)
        {            
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SaleEntities context = CreateContext())
                {
                   return converter.DB2DTO_SaleSearch(context.Sale.Where(o =>o.Visible.HasValue && o.Visible.Value==true).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return new List<DTO.SaleDTO>();
            }
        }
    }
}
