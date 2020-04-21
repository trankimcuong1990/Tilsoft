using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.SaleOrderMng
{
    public class Executor : Library.Base.IExecutor
    {
        BLL bll = new BLL();
        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            string OrderType = "";
            int SaleOrderID = 0;
            int OfferID = 0;
            object dtoItem = null;
            bool WithoutSigned = false;
            string newFile = "";
            string oldPointer = "";
            string tempFolder = "";
            string reportName = "";
            switch (identifier.ToLower())
            {
                case "getsaleorder":
                   
                    if (input["orderType"] != null)
                    {
                        OrderType = input["orderType"].ToString();
                    }
                    if (input["ID"] != null)
                    {
                        SaleOrderID = Convert.ToInt32(input["ID"]);
                    }
                    if (input["offerID"] != null)
                    {
                        OfferID = Convert.ToInt32(input["offerID"]);
                    }
                    return bll.GetDataContainer(SaleOrderID, OfferID, OrderType, userId, out notification);

                case "getofferline":
                    if (input["orderType"] != null)
                    {
                        OrderType = input["orderType"].ToString();
                    }
                    if (input["offerID"] != null)
                    {
                        OfferID = Convert.ToInt32(input["offerID"]);
                    }
                    return bll.GetOfferLine(OfferID, OrderType, out notification);

                case "getofferlinesparepart":
                    if (input["orderType"] != null)
                    {
                        OrderType = input["orderType"].ToString();
                    }
                    if (input["offerID"] != null)
                    {
                        OfferID = Convert.ToInt32(input["offerID"]);
                    }
                    return bll.GetOfferLineSparepart(OfferID, OrderType, out notification);
                case "confirm":
                    if (input["ID"] != null)
                    {
                        SaleOrderID = Convert.ToInt32(input["ID"]);
                    }
                    if(input["dtoItem"] != null)
                    {
                        dtoItem = input["dtoItem"];
                    }
                    if (input["withoutSigned"] != null)
                    {
                        WithoutSigned = Convert.ToBoolean(input["withoutSigned"]);
                    }
                    return bll.Confirm(SaleOrderID, ref dtoItem, userId, WithoutSigned, out notification);
                case "revise":
                    if (input["ID"] != null)
                    {
                        SaleOrderID = Convert.ToInt32(input["ID"]);
                    }
                    if (input["dtoItem"] != null)
                    {
                        dtoItem = input["dtoItem"];
                    }
                    return bll.Revise(SaleOrderID, ref dtoItem, userId, out notification);
                case "upload-signed-pi-file":
                    if (input["ID"] != null)
                    {
                        SaleOrderID = Convert.ToInt32(input["ID"]);
                    }
                    if (input["dtoItem"] != null)
                    {
                        dtoItem = input["dtoSaleOrder"];
                    }
                    if (input["newFile"] != null)
                    {
                        newFile = input["newFile"].ToString();
                    }
                    if (input["oldPointer"] != null)
                    {
                        oldPointer = input["oldPointer"].ToString();
                    }
                    if (input["tempFolder"] != null)
                    {
                        tempFolder = input["tempFolder"].ToString();
                    }
                    return bll.UploadSignedPIFile(SaleOrderID, newFile, oldPointer, tempFolder, out notification);
                case "up-load-client-po-file":
                    if (input["ID"] != null)
                    {
                        SaleOrderID = Convert.ToInt32(input["ID"]);
                    }
                    if (input["dtoItem"] != null)
                    {
                        dtoItem = input["dtoSaleOrder"];
                    }
                    if (input["newFile"] != null)
                    {
                        newFile = input["newFile"].ToString();
                    }
                    if (input["oldPointer"] != null)
                    {
                        oldPointer = input["oldPointer"].ToString();
                    }
                    if (input["tempFolder"] != null)
                    {
                        tempFolder = input["tempFolder"].ToString();
                    }
                    return bll.UploadClientPOFile(SaleOrderID, newFile, oldPointer, tempFolder, out notification);
                case "printpi":
                    if (input["ID"] != null)
                    {
                        SaleOrderID = Convert.ToInt32(input["ID"]);
                    }
                    if (input["reportName"] != null)
                    {
                        reportName = input["reportName"].ToString();
                    }
                    return bll.GetPrintoutData(SaleOrderID, reportName, out notification);
                case "getloadingplan2":
                    if (input["ID"] != null)
                    {
                        SaleOrderID = Convert.ToInt32(input["ID"]);
                    }
                    return bll.GetLoadingPlan2(SaleOrderID, out notification);
                case "returngoods":
                    if (input["dtoReturns"] != null)
                    {
                        dtoItem = input["dtoReturns"];
                    }
                    return bll.CreateReturnData(dtoItem, out notification);
            }
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }

        public bool DeleteData(int id, int userId, out Notification notification)
        {
            return bll.DeleteData(id, userId, out notification);
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return bll.UpdateData(id, ref dtoItem, userId, out notification);
        }

        public object GetOfferLine(int offerId, string orderType , out Notification notification)
        {
            return bll.GetOfferLine(offerId, orderType, out notification);
        }

        public object GetOfferLineSparepart(int offerId, string orderType, out Notification notification)
        {
            return bll.GetOfferLineSparepart(offerId, orderType, out notification);
        }

        public DTO.SaleOrder UploadSignedPIFile(int saleOrderID, string newFile, string oldPointer, string tempFolder, out Library.DTO.Notification notification)
        {
            return bll.UploadSignedPIFile(saleOrderID, newFile, oldPointer, tempFolder, out notification);
        }

        public DTO.SaleOrder UploadClientPOFile(int saleOrderID, string newFile, string oldPointer, string tempFolder, out Library.DTO.Notification notification)
        {
            return bll.UploadClientPOFile(saleOrderID, newFile, oldPointer, tempFolder, out notification);
        }
    }
}
