using DTO.Support;
using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.PaymentTermMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        //private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private string _TempFolder;

        public DataFactory() { }

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private PaymentTermMngEntities CreateContext()
        {
            return new PaymentTermMngEntities(Library.Helper.CreateEntityConnectionString("DAL.PaymentTermMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.PaymentTermSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (PaymentTermMngEntities context = CreateContext())
                {
                    string paymentTermNM = null;
                    bool? isActive = null;
                    string paymentMethod = null;
                    int? paymentTypeID = null;

                    if (filters.ContainsKey("PaymentTermNM") && !string.IsNullOrEmpty(filters["PaymentTermNM"].ToString()))
                    {
                        paymentTermNM = filters["PaymentTermNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("IsActive") && !string.IsNullOrEmpty(filters["IsActive"].ToString()))
                    {
                        isActive = Convert.ToBoolean(filters["IsActive"].ToString());
                    }
                    if (filters.ContainsKey("PaymentMethod") && !string.IsNullOrEmpty(filters["PaymentMethod"].ToString()))
                    {
                        paymentMethod = filters["PaymentMethod"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("PaymentTypeID") && !string.IsNullOrEmpty(filters["PaymentTypeID"].ToString()))
                    {
                        paymentTypeID = Convert.ToInt32(filters["PaymentTypeID"].ToString());
                    }

                    totalRows = context.PaymentTermMng_function_SearchPaymentTerm(paymentTermNM, paymentTypeID, isActive, paymentMethod, orderBy, orderDirection).Count();
                    var result = context.PaymentTermMng_function_SearchPaymentTerm(paymentTermNM, paymentTypeID, isActive, paymentMethod, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_PaymentTermSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            data.Data = new DTO.PaymentTerm();
            data.PaymentTypes = new List<PaymentType>();
            data.PaymentMethods = new List<PaymentMethod>();

            //try to get data
            try
            {
                using (PaymentTermMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_PaymentTerm(context.PaymentTermMng_PaymentTerm_View.FirstOrDefault(o => o.PaymentTermID == id));
                    data.PaymentTypes = GetPaymentType().ToList();
                    data.PaymentMethods = GetPaymentMethod().ToList();
                    data.ActiveStatuses = GetYesNo().ToList();
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
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Delete Success" };

            try
            {
                using (PaymentTermMngEntities context = CreateContext())
                {
                    PaymentTerm dbItem = context.PaymentTerm.FirstOrDefault(o => o.PaymentTermID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Payment Term not found!";
                        return false;
                    }
                    else
                    {
                        context.PaymentTerm.Remove(dbItem);
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

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.PaymentTerm dtoPaymentTerm = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PaymentTerm>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // cuong.tran: #2090 - Update Payment term module (view detail) - start
                // If Type = 4 then Method = "LC"
                if (dtoPaymentTerm.PaymentTypeID.HasValue && dtoPaymentTerm.PaymentTypeID.Value == 4)
                {
                    dtoPaymentTerm.PaymentMethod = "LC";
                }
                // Check data validation
                // Case DP_Percent
                if ("DP-PERCENT".Equals(dtoPaymentTerm.PaymentMethod))
                {
                    // Warning: Check value of DownValue
                    if (!dtoPaymentTerm.DownValue.HasValue)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Please input value DownValue!";
                        return false;
                    }

                    // Warning: Check value DownValue larger than 100
                    if (dtoPaymentTerm.DownValue.Value > 100)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Please input value DownValue smaller than 100!";
                        return false;
                    }
                }
                // cuong.tran: #2090 - Update Payment term module (view detail) - e n d


                using (PaymentTermMngEntities context = CreateContext())
                {
                    PaymentTerm dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new PaymentTerm();
                        context.PaymentTerm.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.PaymentTerm.FirstOrDefault(o => o.PaymentTermID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Payment Term not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_PaymentTerm(dtoPaymentTerm, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.PaymentTermID, out notification).Data;

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

        public DTO.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //int i = 0;
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.YesNoValues = new List<YesNo>();
            data.PaymentMethods = new List<PaymentMethod>();
            data.PaymentTypes = new List<PaymentType>();

            //try to get data
            try
            {
                data.YesNoValues = GetYesNo().ToList();
                data.PaymentMethods = GetPaymentMethod().ToList();
                data.PaymentTypes = GetPaymentType().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<YesNo> GetYesNo()
        {
            List<YesNo> result = new List<YesNo>();
            result.Add(new YesNo() { YesNoText = "Yes", YesNoValue = "true" });
            result.Add(new YesNo() { YesNoText = "No", YesNoValue = "false" });

            return result;
        }

        public IEnumerable<PaymentMethod> GetPaymentMethod()
        {
            //try to get data
            try
            {
                using (PaymentTermMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_PaymentMethod(context.List_PaymentMethod.ToList());
                }
            }
            catch (Exception)
            {
                return new List<PaymentMethod>();
            }
        }

        public IEnumerable<PaymentType> GetPaymentType()
        {
            //try to get data
            try
            {
                using (PaymentTermMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_PaymentType(context.List_PaymentType.ToList());
                }
            }
            catch (Exception)
            {
                return new List<PaymentType>();
            }
        }
    }
}
