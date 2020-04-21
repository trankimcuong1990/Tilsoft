using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.IO;
namespace DAL.CreditNoteMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.CreditNoteMng.CreditNote, DTO.CreditNoteMng.CreditNote>
    {
        private DataConverter converter = new DataConverter();

        private CreditNoteMngEntities CreateContext()
        {
            return new CreditNoteMngEntities(DALBase.Helper.CreateEntityConnectionString("CreditNoteMngModel"));
        }

        public override IEnumerable<DTO.CreditNoteMng.CreditNote> GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.CreditNoteMng.CreditNote GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try to get data
            try
            {
                using (CreditNoteMngEntities context = CreateContext())
                {
                    DTO.CreditNoteMng.CreditNote dtoItem = new DTO.CreditNoteMng.CreditNote();

                    CreditNoteMng_CreditNote_View dbItem;
                    dbItem = context.CreditNoteMng_CreditNote_View
                        .Include("CreditNoteMng_CreditNoteDetail_View")
                        .Include("CreditNoteMng_CreditNoteProductDetail_View")
                        .Include("CreditNoteMng_CreditNoteSparepartDetail_View")
                        .FirstOrDefault(o => o.CreditNoteID == id);

                    dtoItem = converter.DB2DTO_CreditNote(dbItem);
                    return dtoItem;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.CreditNoteMng.CreditNote();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success , Message = "Delete success"};
            try
            {
                using (CreditNoteMngEntities context = CreateContext())
                {
                    CreditNote dbItem = context.CreditNote.FirstOrDefault(o => o.CreditNoteID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Credit note not found!");
                    }
                    if (dbItem.IsConfirmed != null && (bool)dbItem.IsConfirmed)
                    {
                        throw new Exception("Credit note was confirmed. You can not delete");
                    }
                    context.CreditNote.Remove(dbItem);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.CreditNoteMng.CreditNote dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (CreditNoteMngEntities context = CreateContext())
                {
                    CreditNote dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new CreditNote();
                        context.CreditNote.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.CreditNote.FirstOrDefault(o => o.CreditNoteID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "invoice not found!";
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
                        converter.DTO2DB_CreditNote(dtoItem, ref dbItem);
                        context.SaveChanges();

                        //Update InvoiceNo
                        if (id == 0)
                        {
                            context.CreditNoteMng_function_GenerateInvoiceNo(dbItem.CreditNoteID);
                        }
                        //Get return data
                        dtoItem = GetData(dbItem.CreditNoteID, out notification);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public DTO.CreditNoteMng.CreditNote GetEditData(int id,int eCommercialInvoiceID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try to get data
            try
            {
                using (CreditNoteMngEntities context = CreateContext())
                {
                    DTO.CreditNoteMng.CreditNote dtoItem = new DTO.CreditNoteMng.CreditNote();

                    if (id > 0)
                    {
                        CreditNoteMng_CreditNote_View dbItem;
                        dbItem = context.CreditNoteMng_CreditNote_View
                            .Include("CreditNoteMng_CreditNoteDetail_View")
                            .Include("CreditNoteMng_CreditNoteProductDetail_View")
                            .Include("CreditNoteMng_CreditNoteSparepartDetail_View")
                            .FirstOrDefault(o => o.CreditNoteID == id);

                        dtoItem = converter.DB2DTO_CreditNote(dbItem);
                    }
                    else
                    {
                        ECommercialInvoiceMng_ECommercialInvoice_View dbEInvoice = context.ECommercialInvoiceMng_ECommercialInvoice_View
                                                                            .Include("ECommercialInvoiceMng_ECommercialInvoiceDetail_View")
                                                                            .Include("ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View")
                                                                            .FirstOrDefault(o => o.ECommercialInvoiceID == eCommercialInvoiceID);
                        //CreditNote
                        dtoItem.ECommercialInvoiceID = eCommercialInvoiceID;
                        dtoItem.InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy");
                        
                        dtoItem.ClientUD = dbEInvoice.ClientUD;
                        dtoItem.ClientNM = dbEInvoice.ClientNM;
                        dtoItem.VATNo = dbEInvoice.VATNo;
                        dtoItem.TelePhone = dbEInvoice.Telephone;
                        dtoItem.Fax = dbEInvoice.Fax;
                        dtoItem.ClientAddress = dbEInvoice.ClientAddress;

                        dtoItem.InvoiceNo = dbEInvoice.InvoiceNo;
                        dtoItem.RefNo = dbEInvoice.RefNo;
                        dtoItem.Conditions = dbEInvoice.Conditions;
                        dtoItem.LCRefNo = dbEInvoice.LCRefNo;
                        dtoItem.Currency = dbEInvoice.Currency;
                        dtoItem.DeliveryTerm = dbEInvoice.DeliveryTerm;
                        dtoItem.PaymentTerm = dbEInvoice.PaymentTerm;
                        

                        //Purchasing Detail Info
                        dtoItem.CreditNoteDetails = new List<DTO.CreditNoteMng.CreditNoteDetail>();
                        dtoItem.CreditNoteProductDetails = new List<DTO.CreditNoteMng.CreditNoteProductDetail>();
                        dtoItem.CreditNoteSparepartDetails = new List<DTO.CreditNoteMng.CreditNoteSparepartDetail>();
                        
                        DTO.CreditNoteMng.CreditNoteProductDetail dtoProduct;
                        DTO.CreditNoteMng.CreditNoteSparepartDetail dtoSparepart;
                        int i = -1;
                        foreach (var item in dbEInvoice.ECommercialInvoiceMng_ECommercialInvoiceDetail_View)
                        {
                            dtoProduct = new DTO.CreditNoteMng.CreditNoteProductDetail();
                            dtoProduct.CreditNoteProductDetailID = i;
                            dtoProduct.UnitPrice = item.UnitPrice;
                            dtoProduct.ECommercialInvoiceDetailID = item.ECommercialInvoiceDetailID;

                            dtoProduct.ProformaInvoiceNo = item.ProformaInvoiceNo;
                            dtoProduct.ContainerNo = item.ContainerNo;
                            dtoProduct.SealNo = item.SealNo;
                            dtoProduct.ContainerTypeNM = item.ContainerType;
                            dtoProduct.ArticleCode = item.ArticleCode;
                            dtoProduct.Description = item.Description;

                            dtoItem.CreditNoteProductDetails.Add(dtoProduct);
                            i--;
                        }

                        i = -1;
                        foreach (var item in dbEInvoice.ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View)
                        {
                            dtoSparepart = new DTO.CreditNoteMng.CreditNoteSparepartDetail();
                            dtoSparepart.CreditNoteSparepartDetailID = i;
                            dtoSparepart.UnitPrice = item.UnitPrice;
                            dtoSparepart.ECommercialInvoiceSparepartDetailID = item.ECommercialInvoiceSparepartDetailID;

                            dtoSparepart.ProformaInvoiceNo = item.ProformaInvoiceNo;
                            dtoSparepart.ContainerNo = item.ContainerNo;
                            dtoSparepart.SealNo = item.SealNo;
                            dtoSparepart.ContainerTypeNM = item.ContainerType;
                            dtoSparepart.ArticleCode = item.ArticleCode;
                            dtoSparepart.Description = item.Description;

                            dtoItem.CreditNoteSparepartDetails.Add(dtoSparepart);
                            i--;
                        }
                    }
                    return dtoItem;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.CreditNoteMng.CreditNote();
            }
        }

        public DTO.ECommercialInvoiceMng.InvoiceContainerPrintout GetCreditNotePrintout(int creditNoteID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (CreditNoteMngEntities context = CreateContext())
                {
                    CreditNoteMng_CreditNote_ReportView dbItem;
                    dbItem = context.CreditNoteMng_CreditNote_ReportView
                        .Include("CreditNoteMng_CreditNoteDetail_ReportView")
                        .FirstOrDefault(o => o.CreditNoteID == creditNoteID);

                    return converter.DB2DTO_Printout(dbItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ECommercialInvoiceMng.InvoiceContainerPrintout();
            }
        }

        public bool Confirm(int id, int confirmedBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Credit note has been confirmed success" };
            try
            {
                using (CreditNoteMngEntities context = CreateContext())
                {
                    CreditNote dbItem = context.CreditNote.Where(o => o.CreditNoteID == id).FirstOrDefault();
                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedBy = confirmedBy;
                    dbItem.ConfirmedDate = DateTime.Now;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }
    
    }  
}
