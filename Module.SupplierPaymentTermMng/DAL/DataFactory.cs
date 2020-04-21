using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.SupplierPaymentTermMng.DTO;

namespace Module.SupplierPaymentTermMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchForm, DTO.EditForm>
    {
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            try
            {
                using (var context = CreatContex())
                {
                    var dbItem = context.SupplierPaymentTerm.Where(o => o.SupplierPaymentTermID == id).FirstOrDefault();
                    context.SupplierPaymentTerm.Remove(dbItem);
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

        public override EditForm GetData(int id, out Notification notification)
        {
            DTO.EditForm data = new DTO.EditForm();
            data.Data = new DTO.SupplierPaymentTermDto();

            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using (var context = CreatContex())
                {
                    if (id > 0)
                    {
                        SupplierPaymentTermMng_SupplierPaymentTerm_View dbItem;
                        dbItem = context.SupplierPaymentTermMng_SupplierPaymentTerm_View.FirstOrDefault(o => o.SupplierPaymentTermID == id);
                        data.Data = converter.DB2DTO_SupplierPaymentTerm(dbItem);
                    }
                }
                return data;
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
                return data;
            }
        }

        public override SearchForm GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification() { Type = NotificationType.Success };

            SearchForm data = new SearchForm();
            data.Data = new List<SearchSupplierPaymentTermDto>();
            try
            {
                using (var context = CreatContex())
                {
                    data.Data = converter.DB2DTO_SeachSupplierPaymentTerm(context.SupplierPaymentTermMng_SupplierPaymentTerm_SearchView.ToList());
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
            }
            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.SupplierPaymentTermDto dtoItems = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SupplierPaymentTermDto>();
            try
            {
                using (var context = CreatContex())
                {
                    SupplierPaymentTerm dbItem;
                    if (id == 0)
                    {
                        dbItem = new SupplierPaymentTerm();
                        context.SupplierPaymentTerm.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.SupplierPaymentTerm.Where(o => o.SupplierPaymentTermID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "Data Not found !";
                        return false;
                    }
                    else
                    {
                        var SupplierData = converter.DB2DTO_SeachSupplierPaymentTerm(context.SupplierPaymentTermMng_SupplierPaymentTerm_SearchView.Where(o => o.SupplierPaymentTermID != id).ToList());
                        string upperSupplierPaymentTermNM = dtoItems.SupplierPaymentTermNM.ToUpper().Trim();
                        while(upperSupplierPaymentTermNM.IndexOf(" ") >= 0)
                        {
                            upperSupplierPaymentTermNM = upperSupplierPaymentTermNM.Replace(" ", "");
                        }
                        for (int i = 0; i < SupplierData.Count; i++)
                        {
                            string upperSupplierPaymentTermNMList = SupplierData[i].SupplierPaymentTermNM.ToUpper().Trim();
                            while (upperSupplierPaymentTermNMList.IndexOf(" ") >= 0)
                            {
                                upperSupplierPaymentTermNMList = upperSupplierPaymentTermNMList.Replace(" ", "");
                            }
                            if (upperSupplierPaymentTermNM  == upperSupplierPaymentTermNMList)
                            {
                                throw new Exception("Supplier Payment Term Name already exists");
                            }
                        }
                        converter.DTO2DB_SupplierPaymentTerm(dtoItems, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.SupplierPaymentTermID, out notification).Data;
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

        private SupplierPaymentTermMngEntities CreatContex()
        {
            return new SupplierPaymentTermMngEntities(Library.Helper.CreateEntityConnectionString("DAL.SupplierPaymentTermMngModel"));
        }

    }
}
