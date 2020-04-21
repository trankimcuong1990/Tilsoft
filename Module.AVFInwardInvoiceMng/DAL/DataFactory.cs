using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVFInwardInvoiceMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private string _tempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }
        private AVFInwardInvoiceMngEntities CreateContext()
        {
            return new AVFInwardInvoiceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.AVFInwardInvoiceMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.AVFInwardInvoiceSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (AVFInwardInvoiceMngEntities context = CreateContext())
                {
                    string AVFSupplierNM = null;
                    string InvoiceNo = null;

                    if (filters.ContainsKey("AVFSupplierNM") && !string.IsNullOrEmpty(filters["AVFSupplierNM"].ToString()))
                    {
                        AVFSupplierNM = filters["AVFSupplierNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("InvoiceNo") && !string.IsNullOrEmpty(filters["InvoiceNo"].ToString()))
                    {
                        InvoiceNo = filters["InvoiceNo"].ToString().Replace("'", "''");
                    }

                    var dataTest = context.AVFInwardInvoiceMng_function_SearchAVFInwardInvoice(AVFSupplierNM, InvoiceNo, orderBy, orderDirection);
                    totalRows = dataTest.Count();
                    var result = context.AVFInwardInvoiceMng_function_SearchAVFInwardInvoice(AVFSupplierNM, InvoiceNo, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_AVFInwardInvoiceSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.AVFInwardInvoice();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (AVFInwardInvoiceMngEntities context = CreateContext())
                    {
                        var invoice = context.AVFInwardInvoiceMng_AVFInwardInvoice_View.FirstOrDefault(o => o.AVFPurchasingInvoiceID == id);
                        if (invoice == null)
                        {
                            throw new Exception("Can not found the invoice to edit");
                        }
                        data.Data = converter.DB2DTO_AVFInwardInvoice(invoice);
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
            throw new NotImplementedException();
        }
        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData result = new DTO.SearchFilterData();
            return result;
        }
  
    }
}
