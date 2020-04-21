using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVFOutwardInvoiceMng.DAL
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
        private AVFOutwardInvoiceMngEntities CreateContext()
        {
            return new AVFOutwardInvoiceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.AVFOutwardInvoiceMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.AVFOutwardInvoiceSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (AVFOutwardInvoiceMngEntities context = CreateContext())
                {
                    string creditorNM = null;
                    string accountNo = null;

                    if (filters.ContainsKey("CreditorNM") && !string.IsNullOrEmpty(filters["CreditorNM"].ToString()))
                    {
                        creditorNM = filters["CreditorNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("AccountNo") && !string.IsNullOrEmpty(filters["AccountNo"].ToString()))
                    {
                        accountNo = filters["AccountNo"].ToString().Replace("'", "''");
                    }

                    var dataTest = context.AVFOutwardInvoiceMng_function_SearchAVFOutwardInvoice(creditorNM, accountNo, orderBy, orderDirection);
                    totalRows = dataTest.Count();
                    var result = context.AVFOutwardInvoiceMng_function_SearchAVFOutwardInvoice(creditorNM, accountNo, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_AVFOutwardInvoiceSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            data.Data = new DTO.AVFOutwardInvoice();
            data.Data.AVFOutwardInvoiceDetails = new List<DTO.AVFOutwardInvoiceDetail>();
            data.Seasons = new List<Module.Support.DTO.Season>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (AVFOutwardInvoiceMngEntities context = CreateContext())
                    {
                        var profile = context.AVFOutwardInvoiceMng_AVFOutwardInvoice_View.FirstOrDefault(o => o.AVFOutwardInvoiceID == id);
                        if (profile == null)
                        {
                            throw new Exception("Can not found the account to edit");
                        }
                        data.Data = converter.DB2DTO_AVFOutwardInvoice(profile);
                    }
                }
                data.Seasons = supportFactory.GetSeason().ToList();
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
                using (AVFOutwardInvoiceMngEntities context = CreateContext())
                {
                    AVFOutwardInvoice dbItem = context.AVFOutwardInvoice.FirstOrDefault(o => o.AVFOutwardInvoiceID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Profile not found!";
                        return false;
                    }
                    else
                    {
                        foreach (AVFOutwardInvoiceDetail detail in dbItem.AVFOutwardInvoiceDetail.ToArray())
                        {
                            context.AVFOutwardInvoiceDetail.Remove(detail);
                        }
                        context.AVFOutwardInvoice.Remove(dbItem);
                        context.SaveChanges();

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
        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.AVFOutwardInvoice dtoAVFOutwardInvoice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.AVFOutwardInvoice>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (AVFOutwardInvoiceMngEntities context = CreateContext())
                {
                    AVFOutwardInvoice dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new AVFOutwardInvoice();
                        context.AVFOutwardInvoice.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.AVFOutwardInvoice.FirstOrDefault(o => o.AVFOutwardInvoiceID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Invoice not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoAVFOutwardInvoice.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        //convert dto to db
                        converter.DTO2BD(dtoAVFOutwardInvoice, ref dbItem);
                        //remove orphan item
                        context.AVFOutwardInvoiceDetail.Local.Where(o => o.AVFOutwardInvoice == null).ToList().ForEach(o => context.AVFOutwardInvoiceDetail.Remove(o));

                        // processing file
                        //if (dtoAVFOutwardInvoice.PDFFileScan_HasChange)
                        //{
                        //    dbItem.PDFFileScan = fwFactory.CreateNoneImageFilePointer(this._tempFolder, dtoAVFOutwardInvoice.PDFFileScan_NewFile, dtoAVFOutwardInvoice.PDFFileScan);
                        //}

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;

                        context.SaveChanges();
                        dtoItem = GetData(dbItem.AVFOutwardInvoiceID, out notification).Data;

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
