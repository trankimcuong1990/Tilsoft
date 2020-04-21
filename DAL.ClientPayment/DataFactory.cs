using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.IO;
namespace DAL.ClientPaymentMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.ClientPaymentMng.ClientPaymentSearch, DTO.ClientPaymentMng.ClientPayment>
    {
        private DataConverter converter = new DataConverter();
        private string _tempFolder;
        private ClientPaymentMngEntities CreateContext()
        {
            return new ClientPaymentMngEntities(DALBase.Helper.CreateEntityConnectionString("ClientPaymentMngModel"));
        }

        public DataFactory(){}
        
        public DataFactory(string tempFolder)
        {
            _tempFolder = tempFolder + @"\";
        }

        public override IEnumerable<DTO.ClientPaymentMng.ClientPaymentSearch> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out DTO.Framework.Notification notification)
        {
            notification = new DTO.Framework.Notification() { Type = DTO.Framework.NotificationType.Success };
            totalRows = 0;

            string Season = string.Empty;
            string ProformaInvoiceNo = string.Empty;              
            string ClientUD = string.Empty;
            string ClientNM = string.Empty;
            int SaleID = 0;

            if (filters.ContainsKey("Season")) Season = filters["Season"].ToString();
            if (filters.ContainsKey("ProformaInvoiceNo")) ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString();
            if (filters.ContainsKey("ClientUD"))  ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ClientNM")) ClientNM = filters["ClientNM"].ToString();
            if (filters.ContainsKey("SaleID") && filters["SaleID"]!=null) SaleID = Convert.ToInt32(filters["SaleID"].ToString());
           

            //try to get data
            try
            {
                using (ClientPaymentMngEntities context = CreateContext())
                {
                    totalRows = context.ClientPaymentMng_function_SearchClientPayment(orderBy, orderDirection, Season, ProformaInvoiceNo, ClientUD, ClientNM, SaleID).Count();
                    var result = context.ClientPaymentMng_function_SearchClientPayment(orderBy, orderDirection, Season, ProformaInvoiceNo, ClientUD, ClientNM, SaleID);
                    return converter.DB2DTO_ClientPaymentSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = DTO.Framework.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new List<DTO.ClientPaymentMng.ClientPaymentSearch>();
            }
        }

        public override DTO.ClientPaymentMng.ClientPayment GetData(int id, out DTO.Framework.Notification notification)
        {
            notification = new DTO.Framework.Notification() { Type = DTO.Framework.NotificationType.Success };
            //try to get data
            try
            {
                using (ClientPaymentMngEntities context = CreateContext())
                {
                    DTO.ClientPaymentMng.ClientPayment dtoItem = new DTO.ClientPaymentMng.ClientPayment();

                    if (id > 0)
                    {
                        ClientPaymentMng_ClientPayment_View dbItem;
                        dbItem = context.ClientPaymentMng_ClientPayment_View
                            .Include("ClientPaymentMng_ClientPaymentDetail_View")
                            .FirstOrDefault(o => o.ClientPaymentID == id);

                        dtoItem = converter.DB2DTO_ClientPayment(dbItem);
                    }
                    else
                    {
                        dtoItem.ClientPaymentDetails = new List<DTO.ClientPaymentMng.ClientPaymentDetail>();
                    }
                    return dtoItem;
                }
            }
            catch (Exception ex)
            {
                notification.Type = DTO.Framework.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ClientPaymentMng.ClientPayment();
            }
        }

        public override bool DeleteData(int id, out DTO.Framework.Notification notification)
        {
            notification = new DTO.Framework.Notification() { Type = DTO.Framework.NotificationType.Success };
            try
            {
                using (ClientPaymentMngEntities context = CreateContext())
                {
                    ClientPayment dbItem = context.ClientPayment.FirstOrDefault(o => o.ClientPaymentID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "payment not found!";
                        return false;
                    }
                    else
                    {
                        context.ClientPayment.Remove(dbItem);
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = DTO.Framework.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.ClientPaymentMng.ClientPayment dtoItem, out DTO.Framework.Notification notification)
        {
            notification = new DTO.Framework.Notification() { Type = DTO.Framework.NotificationType.Success };
            try
            {
                using (ClientPaymentMngEntities context = CreateContext())
                {
                    ClientPayment dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ClientPayment();
                        context.ClientPayment.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ClientPayment.FirstOrDefault(o => o.ClientPaymentID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "payment not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        //convert dto to db
                        converter.DTO2DB_ClientPayment(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.ClientPaymentID, out notification);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = DTO.Framework.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public DTO.ClientPaymentMng.SearchSupportList GetSearchSupportData()
        {
            DAL.Support.DataFactory factory = new Support.DataFactory();  
            DTO.ClientPaymentMng.SearchSupportList dtoSupport = new DTO.ClientPaymentMng.SearchSupportList();
            dtoSupport.Seasons = factory.GetSeason().ToList();
            dtoSupport.Salers = factory.GetSaler().ToList();
            return dtoSupport;
        }

        public DTO.ClientPaymentMng.EditSupportList GetEditSupportData()
        {
            DAL.Support.DataFactory factory = new Support.DataFactory();
            DTO.ClientPaymentMng.EditSupportList dtoSupport = new DTO.ClientPaymentMng.EditSupportList();
            dtoSupport.Users = factory.GetUser().ToList();
            return dtoSupport;
        }

    }
}
