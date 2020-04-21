using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ForwarderInvoiceMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.ForwarderInvoiceMng.SearchFormData, DTO.ForwarderInvoiceMng.EditFormData, DTO.ForwarderInvoiceMng.ForwarderInvoice>
    {
        private DataConverter converter = new DataConverter();
        private Support.DataFactory supportFactory = new Support.DataFactory();
        private string _TempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private ForwarderInvoiceMngEntities CreateContext()
        {
            return new ForwarderInvoiceMngEntities(DALBase.Helper.CreateEntityConnectionString("ForwarderInvoiceMngModel"));
        }

        public override DTO.ForwarderInvoiceMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ForwarderInvoiceMng.SearchFormData data = new DTO.ForwarderInvoiceMng.SearchFormData();
            data.Data = new List<DTO.ForwarderInvoiceMng.ForwarderInvoiceSearchResult>();
            totalRows = 0;

            string BookingID = null;
            string InvoiceNo = null;
            string AccountNo = null;
            if (filters.ContainsKey("BookingID") && !string.IsNullOrEmpty(filters["BookingID"].ToString()))
            {
                BookingID = filters["BookingID"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("InvoiceNo") && !string.IsNullOrEmpty(filters["InvoiceNo"].ToString()))
            {
                InvoiceNo = filters["InvoiceNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("AccountNo") && !string.IsNullOrEmpty(filters["AccountNo"].ToString()))
            {
                AccountNo = filters["AccountNo"].ToString().Replace("'", "''");
            }
        
            //try to get data
            try
            {
                using (ForwarderInvoiceMngEntities context = CreateContext())
                {
                    totalRows = context.ForwarderInvoiceMng_function_SearchForwarderInvoice(BookingID, InvoiceNo, AccountNo, orderBy, orderDirection).Count();
                    var result = context.ForwarderInvoiceMng_function_SearchForwarderInvoice(BookingID, InvoiceNo, AccountNo, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ForwarderInvoiceSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.ForwarderInvoiceMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ForwarderInvoiceMng.EditFormData data = new DTO.ForwarderInvoiceMng.EditFormData();

            data.Data = new DTO.ForwarderInvoiceMng.ForwarderInvoice();
            data.Data.ForwarderInvoiceDetails = new List<DTO.ForwarderInvoiceMng.ForwarderInvoiceDetail>();
            data.FeeTypes = new List<DTO.Support.FeeType>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (ForwarderInvoiceMngEntities context = CreateContext())
                    {
                        var invoice = context.ForwarderInvoiceMng_ForwarderInvoice_View.FirstOrDefault(o => o.ForwarderInvoiceID == id);
                        if (invoice == null)
                        {
                            throw new Exception("Can not found the invoice to edit");
                        }
                        data.Data = converter.DB2DTO_ForwarderInvoice(invoice);
                    }
                }
                data.FeeTypes = supportFactory.GetFeeType().ToList();
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
                using (ForwarderInvoiceMngEntities context = CreateContext())
                {
                    ForwarderInvoice dbItem = context.ForwarderInvoices.FirstOrDefault(o => o.ForwarderInvoiceID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Forwarder Invoice not found!";
                        return false;
                    }
                    else
                    {
                        context.ForwarderInvoices.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.ForwarderInvoiceMng.ForwarderInvoice dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try {
                using (ForwarderInvoiceMngEntities context = CreateContext())
                {
                    ForwarderInvoice dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ForwarderInvoice();
                        context.ForwarderInvoices.Add(dbItem);
                    }
                    else 
                    {
                        dbItem = context.ForwarderInvoices.FirstOrDefault(o => o.ForwarderInvoiceID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Forwarder Invoice not found";
                        return false;
                    }
                    else 
                    {
                        converter.DTO2BD(dtoItem, ref dbItem);
                        context.SaveChanges();
                        
                        // processing file
                        if (dtoItem.PDFFileScan_HasChange)
                        {
                            dbItem.PDFFileScan = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoItem.PDFFileScan_NewFile, dtoItem.PDFFileScan);
                        }
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.ForwarderInvoiceID, out notification).Data;

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

        public override bool Approve(int id, ref DTO.ForwarderInvoiceMng.ForwarderInvoice dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.ForwarderInvoiceMng.ForwarderInvoice dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM MODULES
        //
        public DTO.ForwarderInvoiceMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.ForwarderInvoiceMng.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
