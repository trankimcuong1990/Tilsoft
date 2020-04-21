using System;
using System.Collections.Generic;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/ECommercialInvoice")]
    public class ECommercialInvoiceController : ApiController
    {

        private string moduleCode = "ECommercialInvoiceMng";

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.ECommercialInvoiceMng.SearchFormData data = bll.SearchECommercialInvoice(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new Library.DTO.ReturnData<DTO.ECommercialInvoiceMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, int? typOfInvoice,int? internalCompanyID, int? clientID, int? parentID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;
            DTO.ECommercialInvoiceMng.DataContainer ECommercialInvoice = bll.GetDataContainer(id, typOfInvoice, internalCompanyID, clientID, parentID, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.ECommercialInvoiceMng.DataContainer>() { Data = ECommercialInvoice, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.ECommercialInvoiceMng.ECommercialInvoice dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (dtoItem.IsConfirmed.HasValue)
            {
                if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
                {
                    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                }
                if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanReset))
                {
                    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                }
            }

            // validation            
            if (!Helper.CommonHelper.ValidateDTO<DTO.ECommercialInvoiceMng.ECommercialInvoice>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.ECommercialInvoiceMng.ECommercialInvoice>() { Data = dtoItem, Message = notification });
            }

            // continue processing
             BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);

            
            return Ok(new Library.DTO.ReturnData<DTO.ECommercialInvoiceMng.ECommercialInvoice>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("purchasinginvoices")]
        public IHttpActionResult GetPurchasingInvoice(int clientID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;

            int totalRows = 0;
            IEnumerable<DTO.ECommercialInvoiceMng.PurchasingInvoice> data = bll.GetPurchasingInvoice(clientID, ControllerContext.GetAuthUserId(), out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.ECommercialInvoiceMng.PurchasingInvoice>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("printinvoice")]
        public IHttpActionResult PrintInvoice(int id, string formatFile, string reportName, int invoiceType)
        {
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            //CREATE PRINTOUT
            string printoutFileName = string.Empty;
            int? companyID = fwBll.GetCompanyID(ControllerContext.GetAuthUserId());
            /*
             * get report file base on format of report
             * Excel format is special, we will use one function to get report base on excel
             * Another format we will use microsoft report tool to create report
             */
            if (formatFile == "Excel")
            {
                //switch (companyID)
                //{
                //    case 13:
                //        printoutFileName = bll.GetOrangePiePrintout(id, out notification);
                //        break;
                //    default:
                        
                //        break;
                //}

                printoutFileName = bll.GetInvoicePrintoutInExcel(id, reportName, invoiceType, out notification);
            }
            else
            {
                DTO.ECommercialInvoiceMng.InvoiceContainerPrintout dtoPrintout = bll.GetInvoicePrintoutData(id, ControllerContext.GetAuthUserId(), out notification);
                if (dtoPrintout != null && dtoPrintout.Invoices != null && dtoPrintout.InvoiceDetails != null)
                {
                    reportName = reportName + ".rdlc";
                    //switch (companyID)
                    //{
                    //    case 13:
                    //        reportName = reportName + "_OrangePine.rdlc";
                    //        break;
                    //    default:
                    //        reportName = reportName + ".rdlc";
                    //        break;
                    //}
                    Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
                    lr.ReportPath = FrameworkSetting.Setting.AbsoluteReportFolder + reportName;

                    Microsoft.Reporting.WebForms.ReportDataSource rsInvoice = new Microsoft.Reporting.WebForms.ReportDataSource();
                    rsInvoice.Name = "Invoice";
                    rsInvoice.Value = dtoPrintout.Invoices;
                    lr.DataSources.Add(rsInvoice);

                    Microsoft.Reporting.WebForms.ReportDataSource rsInvoiceDetail = new Microsoft.Reporting.WebForms.ReportDataSource();
                    rsInvoiceDetail.Name = "InvoiceDetail";
                    rsInvoiceDetail.Value = dtoPrintout.InvoiceDetails;
                    lr.DataSources.Add(rsInvoiceDetail);
                    printoutFileName = PrintoutHelper.BuildPrintoutFile(lr, formatFile);
                }
            }
            return Ok(new Library.DTO.ReturnData<string>() { Data = printoutFileName, Message = notification });
        }

        [HttpPost]
        [Route("printpackinglist")]
        public IHttpActionResult PrintPackingList(int id, string formatType, string reportName)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            string printoutFileName = string.Empty;
            if (reportName == "PackingListPrint")
            {
                if (formatType == "PDF")
                {
                    DTO.ECommercialInvoiceMng.PackingListContainerPrintout dtoPrintout = bll.GetPackingListPrintoutData(id, ControllerContext.GetAuthUserId(), out notification);

                    reportName = reportName + ".rdlc";
                    Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
                    lr.ReportPath = FrameworkSetting.Setting.AbsoluteReportFolder + reportName;

                    Microsoft.Reporting.WebForms.ReportDataSource rsInvoice = new Microsoft.Reporting.WebForms.ReportDataSource();
                    rsInvoice.Name = "PackingList";
                    rsInvoice.Value = dtoPrintout.PackingListPrintouts;
                    lr.DataSources.Add(rsInvoice);

                    Microsoft.Reporting.WebForms.ReportDataSource rsInvoiceDetail = new Microsoft.Reporting.WebForms.ReportDataSource();
                    rsInvoiceDetail.Name = "PackingListDetail";
                    rsInvoiceDetail.Value = dtoPrintout.PackingListDetailPrintouts;
                    lr.DataSources.Add(rsInvoiceDetail);

                    printoutFileName = PrintoutHelper.BuildPrintoutFile(lr, "PDF");
                }
                else
                {
                    printoutFileName = bll.GetPEUackingListPrintoutData(id, reportName, out notification);
                }
            }
            else
            {
                if (reportName == "PackingListPrint_PerCont")
                {
                    if (formatType == "PDF")
                    {
                        List<DTO.ECommercialInvoiceMng.PackingListContainerPrintout> listDtoPrintout = bll.GetPackingListPrintoutData_PerCont(id, ControllerContext.GetAuthUserId(), out notification);
                        foreach(var dtoPrintout in listDtoPrintout)
                        {
                            //DTO.ECommercialInvoiceMng.PackingListContainerPrintout dtoPrintout = bll.GetPackingListPrintoutData(id, ControllerContext.GetAuthUserId(), out notification);

                            reportName = reportName + ".rdlc";
                            Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
                            lr.ReportPath = FrameworkSetting.Setting.AbsoluteReportFolder + reportName;

                            Microsoft.Reporting.WebForms.ReportDataSource rsInvoice = new Microsoft.Reporting.WebForms.ReportDataSource();
                            rsInvoice.Name = "PackingList";
                            rsInvoice.Value = dtoPrintout.PackingListPrintouts;
                            lr.DataSources.Add(rsInvoice);

                            Microsoft.Reporting.WebForms.ReportDataSource rsInvoiceDetail = new Microsoft.Reporting.WebForms.ReportDataSource();
                            rsInvoiceDetail.Name = "PackingListDetail";
                            rsInvoiceDetail.Value = dtoPrintout.PackingListDetailPrintouts;
                            lr.DataSources.Add(rsInvoiceDetail);

                            if(printoutFileName != string.Empty)
                            {
                                printoutFileName = printoutFileName + "," + PrintoutHelper.BuildPrintoutFile(lr, "PDF");
                            }
                            else
                            {
                                printoutFileName = PrintoutHelper.BuildPrintoutFile(lr, "PDF");
                            }
                            reportName = "PackingListPrint_PerCont";
                        }
                    }
                    else
                    {
                        printoutFileName = bll.GetEUPackingListPrintoutData_PerCont(id, reportName, out notification);
                    }
                }
                else
                {
                    printoutFileName = bll.GetOrangePiePrintout(id, out notification);
                }
            }

            return Ok(new Library.DTO.ReturnData<string>() { Data = printoutFileName, Message = notification });
        }

        [HttpPost]
        [Route("printpackinglistextend")]
        public IHttpActionResult PrintPackingListExtend(int id, int template)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            //GET DATA
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            DTO.ECommercialInvoiceMng.PackingListContainerPrintout dtoPrintout = bll.GetPackingListPrintoutData(id, ControllerContext.GetAuthUserId(), out notification);

            //CREATE PRINTOUT
            string printoutFileName = string.Empty;
            if (dtoPrintout != null && dtoPrintout.PackingListPrintouts != null && dtoPrintout.PackingListDetailPrintouts != null)
            {
                string reportName = "";
                //int? companyID = fwBll.GetCompanyID(ControllerContext.GetAuthUserId());
                switch (template)
                {
                    case 1:
                        reportName = "PackingListPrint_OrangePine.rdlc";
                        break;
                    default:
                        reportName = "PackingListPrint.rdlc";
                        break;
                }

                Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
                lr.ReportPath = FrameworkSetting.Setting.AbsoluteReportFolder + reportName;

                Microsoft.Reporting.WebForms.ReportDataSource rsInvoice = new Microsoft.Reporting.WebForms.ReportDataSource();
                rsInvoice.Name = "PackingList";
                rsInvoice.Value = dtoPrintout.PackingListPrintouts;
                lr.DataSources.Add(rsInvoice);

                Microsoft.Reporting.WebForms.ReportDataSource rsInvoiceDetail = new Microsoft.Reporting.WebForms.ReportDataSource();
                rsInvoiceDetail.Name = "PackingListDetail";
                rsInvoiceDetail.Value = dtoPrintout.PackingListDetailPrintouts;
                lr.DataSources.Add(rsInvoiceDetail);

                printoutFileName = PrintoutHelper.BuildPrintoutFile(lr, "PDF");
            }
            return Ok(new Library.DTO.ReturnData<string>() { Data = printoutFileName, Message = notification });
        }

        [HttpPost]
        [Route("warehousepickinglist")]
        public IHttpActionResult GetWarehousePickingList(int clientID)
        {
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;

            IEnumerable<DTO.ECommercialInvoiceMng.WarehousePickingList> data = bll.GetWarehousePickingList(clientID, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.ECommercialInvoiceMng.WarehousePickingList>>() { Data = data, Message = notification});
        }

        //[HttpPost]
        //[Route("InitWarehouseInfo")]
        //public IHttpActionResult GetInitWarehouseInfos()
        //{
        //    // authentication
        //    Module.Framework.BLL fwBll = new Module.Framework.BLL();
        //    if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
        //    {
        //        return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
        //    }

        //    BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
        //    Library.DTO.Notification notification;

        //    int totalRows = 0;
        //    IEnumerable<DTO.ECommercialInvoiceMng.InitWarehouseInfo> data = bll.GetInitWarehouseInfos(ControllerContext.GetAuthUserId(), out totalRows, out notification);
            
        //    return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.ECommercialInvoiceMng.InitWarehouseInfo>>() { Data = data, Message = notification, TotalRows = totalRows });
        //}


        //[HttpPost]
        //[Route("initfobinvoice")]
        //public IHttpActionResult GetInitInfos()
        //{
        //    // authentication
        //    Module.Framework.BLL fwBll = new Module.Framework.BLL();
        //    if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
        //    {
        //        return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
        //    }

        //    BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
        //    Library.DTO.Notification notification;

        //    int totalRows = 0;
        //    IEnumerable<DTO.ECommercialInvoiceMng.InitFobInvoice> initInfos = bll.GetInitFobInvoices(ControllerContext.GetAuthUserId(), out totalRows, out notification);
        //    return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.ECommercialInvoiceMng.InitFobInvoice>>() { Data = initInfos, Message = notification, TotalRows = totalRows });
        //}

        [HttpPost]
        [Route("getInitData")]
        public IHttpActionResult GetInitData()
        {
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            object data = bll.GetInitData();
            return Ok(new Library.DTO.ReturnData<object>() { Data = data });
        }

        [HttpPost]
        [Route("confirminvoice")]
        public IHttpActionResult ConfirmInvoice(int id, bool isConfirmed)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (isConfirmed)
            {
                if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
                {
                    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                }
            }
            else
            {
                if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanReset))
                {
                    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                }
            }
            
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;
            bll.ConfirmInvoice(id, isConfirmed, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("initfobinvoice")]
        public IHttpActionResult GetInitFobInvoice(DTO.Search searchInput)
        {
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.ECommercialInvoiceMng.InitFobInvoice> data = bll.GetInitFobInvoice(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.ECommercialInvoiceMng.InitFobInvoice>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("initwarehouseinfo")]
        public IHttpActionResult GetInitWarehouseInvoice(DTO.Search searchInput)
        {
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.ECommercialInvoiceMng.InitWarehouseInfo> data = bll.GetInitWarehouseInvoice(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.ECommercialInvoiceMng.InitWarehouseInfo>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("client")]
        public IHttpActionResult GetClient(DTO.Search searchInput)
        {
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.ECommercialInvoiceMng.Client> data = bll.GetClient(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.ECommercialInvoiceMng.Client>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("exportexcel")]
        public IHttpActionResult GetReportExportExcelInvoice(string season)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            string reportFileName = bll.GetReportExportExcelInvoice(season, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getreturngoods")]
        public IHttpActionResult GetReturnGoods(int eCommercialInvoiceID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;
            DTO.ECommercialInvoiceMng.ReturnGoods data = bll.GetReturnGoods(eCommercialInvoiceID, out notification);

            return Ok(new Library.DTO.ReturnData<DTO.ECommercialInvoiceMng.ReturnGoods>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("returngoods")]
        public IHttpActionResult GetReturnGoods(DTO.ECommercialInvoiceMng.ReturnGoods dtoReturnData)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;
            int data = bll.CreateReturGoods(dtoReturnData, out notification);

            return Ok(new Library.DTO.ReturnData<int>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("setdonepayment")]
        public IHttpActionResult SetDonePayment(int eCommercialInvoiceID, bool isDonePayment)
        {
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;
            DTO.ECommercialInvoiceMng.ECommercialInvoice dtoItem;
            bool data = bll.SetDonePayment(eCommercialInvoiceID, isDonePayment, out dtoItem, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ECommercialInvoiceMng.ECommercialInvoice>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("exportexactonlinesoftware")]
        public IHttpActionResult ExportExactOnlineSoftware(string ecommercialInvoiceIds)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            string reportFileName = bll.ExportExactOnlineSoftware(ecommercialInvoiceIds, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getBillTransport")]
        public IHttpActionResult GetBillTransport(DTO.Search searchInput)
        {
            Library.DTO.Notification notification;
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            object data = bll.GetBillTransport(searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getoverview")]
        public IHttpActionResult GetOverview(int id, int? typOfInvoice, int? internalCompanyID, int? clientID, int? parentID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;
            DTO.ECommercialInvoiceMng.DataOverview ECommercialInvoice = bll.GetDataOverview(id, typOfInvoice, internalCompanyID, clientID, parentID, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<DTO.ECommercialInvoiceMng.DataOverview>() { Data = ECommercialInvoice, Message = notification });
        }

        [HttpPost]
        [Route("getPurchasingQnt")]
        public IHttpActionResult GetPurchasingQnt(int eCommercialInvoiceID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;

            List<DTO.ECommercialInvoiceMng.ECommercialInvoicePurchasing> data = bll.GetPurchasingQnt(eCommercialInvoiceID, out notification);

            return Ok(new Library.DTO.ReturnData<List<DTO.ECommercialInvoiceMng.ECommercialInvoicePurchasing>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("bizbloqs-import-invoice")]
        public IHttpActionResult BizBloqsImportData(List<DTO.ECommercialInvoiceMng.BizBloqsInvoice> bizBloqsInvoice)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ECommercialInvoiceMng bll = new BLL.ECommercialInvoiceMng();
            Library.DTO.Notification notification;
            List<DTO.ECommercialInvoiceMng.BizBloqsInvoice> data = bll.BizBloqsImportData(bizBloqsInvoice, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.ECommercialInvoiceMng.BizBloqsInvoice>>() { Data = data, Message = notification });
        }

    }
}
