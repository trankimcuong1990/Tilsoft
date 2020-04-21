using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchasingCreditNote
{
    internal class BLL
    {
        DAL.DataFactory factory = new DAL.DataFactory();
        DAL.ReportFactory reportFactory = new DAL.ReportFactory();
        DAL.DataConverter converter = new DAL.DataConverter();
        private Framework.BLL fwBLL = new Framework.BLL();
        
        public object GetPurchasingCreditNoteSearchResult(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetPurchasingCreditNoteSearchResult2(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetEditData(int userId, int purchasingCreditNoteID,int? creditNoteType, int? purchasingInvoiceID, int? supplierID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();

            DTO.PurchasingCreditNote dtoCreditNote = new DTO.PurchasingCreditNote();
            dtoCreditNote.PurchasingCreditNoteDetails = new List<DTO.PurchasingCreditNoteDetail>();
            dtoCreditNote.PurchasingCreditNoteSparepartDetails = new List<DTO.PurchasingCreditNoteSparepartDetail>();
            dtoCreditNote.PurchasingCreditNoteExtendDetails = new List<DTO.PurchasingCreditNoteExtendDetail>();
            
            try
            {
                if (purchasingCreditNoteID > 0)
                {
                    dtoCreditNote = factory.GetPurchasingCreditNote(userId, purchasingCreditNoteID, out notification);
                }
                else
                {
                    if (creditNoteType == 1) //FOB - CREDIT NOTE
                    {
                        DTO.PurchasingInvoice dtoInvoice = factory.GetPurchasingInvoice(userId, purchasingInvoiceID, out notification);
                        dtoCreditNote = converter.DTO2DTO_PurchasingCreditNote(dtoInvoice);
                        dtoCreditNote.CreditNoteType = creditNoteType;
                        dtoCreditNote.PurchasingCreditNoteDetails = new List<DTO.PurchasingCreditNoteDetail>();
                        dtoCreditNote.PurchasingCreditNoteSparepartDetails = new List<DTO.PurchasingCreditNoteSparepartDetail>();
                        dtoCreditNote.PurchasingCreditNoteExtendDetails = new List<DTO.PurchasingCreditNoteExtendDetail>();
                    }
                    else if (creditNoteType == 2) // OTHER - CREDIT NOTE
                    {
                        dtoCreditNote.CreditNoteType = creditNoteType;
                        dtoCreditNote.SupplierID = supplierID;
                        dtoCreditNote.SupplierNM = support_factory.GetSupplier(userId).Where(o => o.SupplierID == supplierID).FirstOrDefault().SupplierNM;
                        dtoCreditNote.Season = Library.Helper.GetCurrentSeason();
                    }                                        
                }
                dtoCreditNote.Seasons = support_factory.GetSeason();
                return dtoCreditNote;
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error };
                notification.Message = ex.Message;
                return dtoCreditNote;
            }
        }
        
        public bool UpdatePurchasingCreditNote(int userID ,int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdatePurchasingCreditNote(userID, id, ref dtoItem, out notification);
        }

        public object GetPurchasingInvoice(int userId, int? id, out Library.DTO.Notification notification)
        {
            return factory.GetPurchasingInvoice(userId, id, out notification);
        }

        public string GetPrintoutCreditNote(int userId, int id, out Library.DTO.Notification notification)
        {
            return reportFactory.GetReportData(userId, id, out notification);
        }

        public object GetCreditNoteType(out Library.DTO.Notification notification)
        {
            return factory.GetCreditNoteType(out notification);
        }
        
        public object GetPurchasingInvoiceSearchResult(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetPurchasingInvoiceSearchResult(userId, filters, out notification);
        }

        public object GetSupplier(int userId)
        {
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
            return support_factory.GetSupplier(userId);
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "delete purchasing credit note:" + id.ToString());
            return factory.DeleteData(userId, id, out notification);
        }

        public bool SetStatus(int id, int statusBy, bool status, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(statusBy, 0, "set credit note status:" + id.ToString());
            return factory.SetStatus(id, statusBy, status, out notification);
        }
    }
}
