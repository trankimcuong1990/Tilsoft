using Module.QAQCMng.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {            
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.Approve(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.Reset(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.SupportFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.GetInitData(out notification);
        }

        public List<QAQCSearchResultDTO> GetQAQCByUserID(int userID, out Library.DTO.Notification notification)
        {
            return factory.GetQAQCByUserID(userID, out notification);
        }

        public CategoryDTO GetCategory(out Library.DTO.Notification notification)
        {
            return factory.GetCategory(out notification);
        }
        public bool UpdateReportFromMobile(int userID, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateReportFromMobile(userID, dtoItem, out notification);
        }

        public List<QAQCReportStarusDTO> GetStatusQAQC(int userID, out Library.DTO.Notification notification)
        {
            return factory.GetStatusQAQC(userID, out notification);
        }

        public Int32? MakeToProcess(int userID, string managementStringID, out Library.DTO.Notification notification)
        {
            return factory.MakeToProcess(userID, managementStringID, out notification);
        }

        public DTO.ReportDataDTO SearchReportData(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            return factory.SearchReportData(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out notification);
        }

        public DTO.ReportQAQCDTO GetReportQAQC(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.GetReportQAQC(userId, id, out notification);
        }

        public bool SetStatusReport(int userId, int id, int statusId, string comment,  out Library.DTO.Notification notification) {
            return factory.SetStatusReport(userId, id, statusId, comment, out notification);
        }

        public bool SetTrackingFacoryOrDer(int userId, object dtoItem, out Library.DTO.Notification notification) {
            return factory.SetTrackingFacoryOrDer(userId, dtoItem, out notification);
        }

        public List<DTO.LogInspectorDTO> GetLogInspector(int userId, int qaqcId, out Library.DTO.Notification notification) {
            return factory.GetLogInspector(userId, qaqcId, out notification);
        }

        public DTO.LoggedInUser LoginUser(int userId, out Library.DTO.Notification notification)
        {
            return factory.LoginUser(userId, out notification);
        }
    }
}
