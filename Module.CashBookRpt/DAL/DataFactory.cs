using Library.DTO;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Module.CashBookRpt.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private CashBookRptEntities CreateContext()
        {
            return new CashBookRptEntities(Library.Helper.CreateEntityConnectionString("DAL.CashBookRptModel"));
        }

        public DTO.SearchFormData GetDataWithFilters(int userID, int? paymentType, int? bankAccount, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.SearchFormData data = new DTO.SearchFormData();

            try
            {
                using (var context = CreateContext())
                {
                    var result = context.CashBookRpt_Function_SearchResult(paymentType, bankAccount, startDate, endDate).ToList();
                    data.CashBookRpt_SearchResultDtos = converter.DB2DTO_SearchResults(result);
                    data.Beginning = context.CashBookRpt_Function_GetBeginning(paymentType, bankAccount, startDate).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }     
        public DTO.SupportFormDto GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SupportFormDto data = new DTO.SupportFormDto();
            try
            {
                using (var context = CreateContext())
                {
                    data.CashBookRptPaymentTypeDtos = converter.DB2DTO_PaymentType(context.CashBookRpt_SupportPaymentType_View.ToList());
                    data.CashBookRptBankAccountDtos = converter.DB2DTO_BankAccount(context.CashBookRpt_BankAccount_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public string ExportExcel(int? paymentType, int? bankAccount, string startDate, string endDate, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            CashBookRptData ds = new CashBookRptData();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("CashBookRpt_Function_SearchResultExport", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Type", paymentType);
                adap.SelectCommand.Parameters.AddWithValue("@BankAccount", bankAccount);
                adap.SelectCommand.Parameters.AddWithValue("@StartDate", startDate);
                adap.SelectCommand.Parameters.AddWithValue("@EndDate", endDate);
                adap.TableMappings.Add("Table", "CashBook");
                adap.TableMappings.Add("Table1", "Condition");
                adap.TableMappings.Add("Table2", "Sum");
                adap.Fill(ds);
                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "CashBookReport");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }
    }
}
