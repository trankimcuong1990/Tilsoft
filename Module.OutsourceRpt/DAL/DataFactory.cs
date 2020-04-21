namespace Module.OutsourceRpt.DAL
{
    using Library.DTO;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal class DataFactory
    {
        private readonly DataConverter converter = new DataConverter();

        public OutsourceRptEntities CreateContext()
        {
            return new OutsourceRptEntities(Library.Helper.CreateEntityConnectionString("DAL.OutsourceRptModel"));
        }

        public DTO.SearchFormDTO GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            totalRows = 0;

            DTO.SearchFormDTO data = new DTO.SearchFormDTO();

            int? productionTeamID = null;
            string clientUD = null;
            string proformaInvoiceNo = null;
            string modelUD = null;
            string modelNM = null;
            string workOrderUD = null;
            int? workOrderStatusID = null;
            string productionItemTypeIDs = null;

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                if (filters.ContainsKey("productionTeamID") && filters["productionTeamID"] != null)
                {
                    productionTeamID = Convert.ToInt32(filters["productionTeamID"]);
                }

                if (filters.ContainsKey("clientUD") && filters["clientUD"] != null)
                {
                    clientUD = filters["clientUD"].ToString();
                }

                if (filters.ContainsKey("proformaInvoiceNo") && filters["proformaInvoiceNo"] != null)
                {
                    proformaInvoiceNo = filters["proformaInvoiceNo"].ToString();
                }

                if (filters.ContainsKey("modelUD") && filters["modelUD"] != null)
                {
                    modelUD = filters["modelUD"].ToString();
                }
                
                if (filters.ContainsKey("modelNM") && filters["modelNM"] != null)
                {
                    modelNM = filters["modelNM"].ToString();
                }

                if (filters.ContainsKey("workOrderUD") && filters["workOrderUD"] != null)
                {
                    workOrderUD = filters["workOrderUD"].ToString();
                }

                if (filters.ContainsKey("workOrderStatusID") && filters["workOrderStatusID"] != null)
                {
                    workOrderStatusID = Convert.ToInt32(filters["workOrderStatusID"]);
                }

                if (filters.ContainsKey("productionItemTypeIDs") && filters["productionItemTypeIDs"] != null)
                {
                    productionItemTypeIDs = filters["productionItemTypeIDs"].ToString();
                }

                using (var context = CreateContext())
                {
                    totalRows = context.OutsourceRpt_function_GetWorkOrder(productionTeamID, clientUD, proformaInvoiceNo, modelUD, modelNM, workOrderUD, workOrderStatusID, productionItemTypeIDs, companyID).Count();
                    data.OutsourceWorkOrder = AutoMapper.Mapper.Map<List<OutsourceRpt_WorkOrder_View>, List<DTO.OutsourceWorkOrderDTO>>(context.OutsourceRpt_function_GetWorkOrder(productionTeamID, clientUD, proformaInvoiceNo, modelUD, modelNM, workOrderUD, workOrderStatusID, productionItemTypeIDs, companyID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.InitFormDTO GetInitData(int userID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.InitFormDTO data = new DTO.InitFormDTO();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                using (var context = CreateContext())
                {
                    data.ProductionTeam = AutoMapper.Mapper.Map<List<OutsourceRpt_ProductionTeam_View>, List<DTO.OutsourceProductionTeamDTO>>(context.OutsourceRpt_ProductionTeam_View.Where(o => o.CompanyID == companyID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<DTO.OutsourceProductionItemDTO> GetProductionItemWithWorkOrder(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.OutsourceProductionItemDTO> data = new List<DTO.OutsourceProductionItemDTO>();

            int? productionTeamID = null;
            string clientUD = null;
            string proformaInvoiceNo = null;
            string modelUD = null;
            string modelNM = null;
            string workOrderUD = null;
            int? workOrderStatusID = null;
            string productionItemTypeIDs = null;

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                if (filters.ContainsKey("productionTeamID") && filters["productionTeamID"] != null)
                {
                    productionTeamID = Convert.ToInt32(filters["productionTeamID"]);
                }

                if (filters.ContainsKey("clientUD") && filters["clientUD"] != null)
                {
                    clientUD = filters["clientUD"].ToString();
                }

                if (filters.ContainsKey("proformaInvoiceNo") && filters["proformaInvoiceNo"] != null)
                {
                    proformaInvoiceNo = filters["proformaInvoiceNo"].ToString();
                }

                if (filters.ContainsKey("modelUD") && filters["modelUD"] != null)
                {
                    modelUD = filters["modelUD"].ToString();
                }

                if (filters.ContainsKey("modelNM") && filters["modelNM"] != null)
                {
                    modelNM = filters["modelNM"].ToString();
                }

                if (filters.ContainsKey("workOrderUD") && filters["workOrderUD"] != null)
                {
                    workOrderUD = filters["workOrderUD"].ToString();
                }

                if (filters.ContainsKey("workOrderStatusID") && filters["workOrderStatusID"] != null)
                {
                    workOrderStatusID = Convert.ToInt32(filters["workOrderStatusID"]);
                }

                if (filters.ContainsKey("productionItemTypeIDs") && filters["productionItemTypeIDs"] != null)
                {
                    productionItemTypeIDs = filters["productionItemTypeIDs"].ToString();
                }

                using (var context = CreateContext())
                {
                    data = AutoMapper.Mapper.Map<List<OutsourceRpt_ProductionItem_View>, List<DTO.OutsourceProductionItemDTO>>(context.OutsourceRpt_function_GetProductionItem(productionTeamID, clientUD, proformaInvoiceNo, modelUD, modelNM, workOrderUD, workOrderStatusID, productionItemTypeIDs, companyID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<DTO.OutsourceDocumentNoteDTO> GetDocumentNoteWithProductionItem(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.OutsourceDocumentNoteDTO> data = new List<DTO.OutsourceDocumentNoteDTO>();

            int? productionTeamID = null;
            string clientUD = null;
            string proformaInvoiceNo = null;
            string modelUD = null;
            string modelNM = null;
            string workOrderUD = null;
            int? workOrderStatusID = null;
            string productionItemTypeIDs = null;
            int? productionItemID = null;

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                if (filters.ContainsKey("productionTeamID") && filters["productionTeamID"] != null)
                {
                    productionTeamID = Convert.ToInt32(filters["productionTeamID"]);
                }

                if (filters.ContainsKey("clientUD") && filters["clientUD"] != null)
                {
                    clientUD = filters["clientUD"].ToString();
                }

                if (filters.ContainsKey("proformaInvoiceNo") && filters["proformaInvoiceNo"] != null)
                {
                    proformaInvoiceNo = filters["proformaInvoiceNo"].ToString();
                }

                if (filters.ContainsKey("modelUD") && filters["modelUD"] != null)
                {
                    modelUD = filters["modelUD"].ToString();
                }

                if (filters.ContainsKey("modelNM") && filters["modelNM"] != null)
                {
                    modelNM = filters["modelNM"].ToString();
                }

                if (filters.ContainsKey("workOrderUD") && filters["workOrderUD"] != null)
                {
                    workOrderUD = filters["workOrderUD"].ToString();
                }

                if (filters.ContainsKey("workOrderStatusID") && filters["workOrderStatusID"] != null)
                {
                    workOrderStatusID = Convert.ToInt32(filters["workOrderStatusID"]);
                }

                if (filters.ContainsKey("productionItemTypeIDs") && filters["productionItemTypeIDs"] != null)
                {
                    productionItemTypeIDs = filters["productionItemTypeIDs"].ToString();
                }

                if (filters.ContainsKey("productionItemID") && filters["productionItemID"] != null)
                {
                    productionItemID = Convert.ToInt32(filters["productionItemID"]);
                }

                using (var context = CreateContext())
                {
                    data = AutoMapper.Mapper.Map<List<OutsourceRpt_DocumentNote_View>, List<DTO.OutsourceDocumentNoteDTO>>(context.OutsourceRpt_function_GetDocumentNote(productionTeamID, clientUD, proformaInvoiceNo, modelUD, modelNM, workOrderUD, workOrderStatusID, productionItemTypeIDs, companyID, productionItemID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
    }
}
