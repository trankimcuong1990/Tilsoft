using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.QAQCRpt.DTO;
using Library;

namespace Module.QAQCRpt.DAL
{
    public class DataFactory : Library.Base.DataFactory2<DTO.SearchDTO, DTO.EditDTO>
    {
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        private QAQCRptEntities CreateContext()
        {
            return new QAQCRptEntities(Library.Helper.CreateEntityConnectionString("DAL.QAQCRptModel"));
        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditDTO GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override SearchDTO GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override object GetInitData(int userId, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.SupportDTO support = new SupportDTO();

            try
            {
                using (var context = CreateContext())
                {
                    support.factories = AutoMapper.Mapper.Map<List<QAQCRpt_SupportFactory_View>, List<DTO.SupportFactory>>(context.QAQCRpt_SupportFactory_View.ToList()).ToList();
                    support.clients = AutoMapper.Mapper.Map<List<QAQCRpt_SupportClient_View>, List<DTO.SupportClient>>(context.QAQCRpt_SupportClient_View.ToList()).ToList();
                    support.productGroups = AutoMapper.Mapper.Map<List<SupportMng_ProductGroup_View>, List<DTO.SupportProductGroup>>(context.SupportMng_ProductGroup_View.ToList()).ToList();
                }
                return support;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return support;
            }
        }

        public override object GetSearchFilter(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public List<DTO.InspectionProductivityDTO> GetInspectionProductivity(int userId, Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            List<DTO.InspectionProductivityDTO> data = new List<InspectionProductivityDTO>();

            int? clientID = null;
            int? factoryID = null;
            int? productGroupID = null;
            string fromDate = null;
            string toDate = null;

            if (filters.ContainsKey("clientID") && filters["clientID"] != null && !string.IsNullOrEmpty(filters["clientID"].ToString()))
            {
                clientID = Convert.ToInt32(filters["clientID"].ToString());
            }
            if (filters.ContainsKey("factoryID") && filters["factoryID"] != null && !string.IsNullOrEmpty(filters["factoryID"].ToString()))
            {
                factoryID = Convert.ToInt32(filters["factoryID"].ToString());
            }
            if (filters.ContainsKey("productGroupID") && filters["productGroupID"] != null && !string.IsNullOrEmpty(filters["productGroupID"].ToString()))
            {
                productGroupID = Convert.ToInt32(filters["productGroupID"].ToString());
            }
            if (filters.ContainsKey("fromDate") && !string.IsNullOrEmpty(filters["fromDate"].ToString()))
            {
                fromDate = filters["fromDate"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("toDate") && !string.IsNullOrEmpty(filters["toDate"].ToString()))
            {
                toDate = filters["toDate"].ToString().Replace("'", "''");
            }

            try
            {
                using (var context = CreateContext())
                {
                    var result = context.QAQCRpt_InspectionProductivity_Function(fromDate.ConvertStringToDateTime(), toDate.ConvertStringToDateTime(), factoryID, clientID, productGroupID).ToList();
                    data = converter.DB2DTO_InspectionProductivity(result);
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return data;
            }
        }

        public List<DTO.TotalInspectionForFactoryMasterDTO> GetTotalInspectionForFactoryMaster(int userId, Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            List<DTO.TotalInspectionForFactoryMasterDTO> data = new List<TotalInspectionForFactoryMasterDTO>();

            int? clientID = null;
            int? factoryID = null;
            string fromDate = null;
            string toDate = null;

            if (filters.ContainsKey("clientID") && filters["clientID"] != null && !string.IsNullOrEmpty(filters["clientID"].ToString()))
            {
                clientID = Convert.ToInt32(filters["clientID"].ToString());
            }
            if (filters.ContainsKey("factoryID") && filters["factoryID"] != null && !string.IsNullOrEmpty(filters["factoryID"].ToString()))
            {
                factoryID = Convert.ToInt32(filters["factoryID"].ToString());
            }
            if (filters.ContainsKey("fromDate") && !string.IsNullOrEmpty(filters["fromDate"].ToString()))
            {
                fromDate = filters["fromDate"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("toDate") && !string.IsNullOrEmpty(filters["toDate"].ToString()))
            {
                toDate = filters["toDate"].ToString().Replace("'", "''");
            }

            try
            {
                using (var context = CreateContext())
                {
                    var result = context.QAQCRpt_TotalInspectionForFactoryMaster_Function(fromDate.ConvertStringToDateTime(), toDate.ConvertStringToDateTime(), factoryID, clientID).ToList();
                    data = converter.DB2DTO_InspectionForFactoryMaster(result).ToList();

                    foreach (var item in data)
                    {
                        var resultDetail = context.QAQCRpt_TotalInspectionForFactoryDetail_Function(fromDate.ConvertStringToDateTime(), toDate.ConvertStringToDateTime(), item.FactoryID, item.ClientID).ToList();
                        item.detail = converter.DB2DTO_InspectionForFactoryDetail(resultDetail).ToList();
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return data;
            }
        }


    }
}
