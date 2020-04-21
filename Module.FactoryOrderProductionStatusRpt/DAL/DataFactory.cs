using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Library.DTO;
using Library;

namespace Module.FactoryOrderProductionStatusRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.ReportData, DTO.ReportData>
    {
        private DataConverter converter = new DataConverter();
        public DataFactory() { }

        private FactoryOrderProductionStatusRptEntities CreateContext()
        {
            return new FactoryOrderProductionStatusRptEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryOrderProductionStatusRptModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override DTO.ReportData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override DTO.ReportData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.ReportData GetFactoryOrderProductionStatusRpt(int userId, Hashtable filters, out Notification notification)
        {
            DTO.ReportData data = new DTO.ReportData();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                string workOrderUD = null;
                string proformaInvoiceNo = null; ;
                string clientUD = null;
                DateTime? fromLDS = null;
                DateTime? toLDS = null;

                if (filters["workOrderUD"] != null) workOrderUD = filters["workOrderUD"].ToString();
                if (filters["clientUD"] != null) clientUD = filters["clientUD"].ToString();
                if (filters["proformaInvoiceNo"] != null) proformaInvoiceNo = filters["proformaInvoiceNo"].ToString();
                if (filters["fromLDS"] != null && filters["fromLDS"].ToString() !="") fromLDS = filters["fromLDS"].ToString().ConvertStringToDateTime();
                if (filters["toLDS"] != null && filters["toLDS"].ToString()!="") toLDS = filters["toLDS"].ToString().ConvertStringToDateTime();

                Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                using (FactoryOrderProductionStatusRptEntities context = CreateContext())
                {
                    var x = context.FactoryOrderProductionStatusRpt_FactoryOrderProductionStatus_View.Where(o => o.WorkOrderUD.Contains(workOrderUD == null || workOrderUD == "" ? o.WorkOrderUD : workOrderUD)
                                                                                                && o.ClientUD.Contains(clientUD == null || clientUD == "" ? o.ClientUD : clientUD)
                                                                                                && o.ProformaInvoiceNo.Contains(proformaInvoiceNo == null || proformaInvoiceNo == "" ? o.ProformaInvoiceNo : proformaInvoiceNo)
                                                                                                && o.LDS >= (fromLDS.HasValue ? fromLDS.Value : o.LDS)
                                                                                                && o.LDS <= (toLDS.HasValue ? toLDS.Value : o.LDS)
                                                                                                );
                    data.FactoryOrderProductionStatuses = converter.DB2DTO_FactoryOrderProductionStatus(x.ToList()); 
                    return data;
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
                return new DTO.ReportData();
            }
        }

        

    }
}
