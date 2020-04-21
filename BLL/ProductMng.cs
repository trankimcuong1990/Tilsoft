using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductMng : Lib.BLLBase2<DTO.ProductMng.SearchFormData, DTO.ProductMng.EditFormData, DTO.ProductMng.Product>
    {
        private DAL.ProductMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public ProductMng(string tempFolder)
        {
            factory = new DAL.ProductMng.DataFactory(tempFolder);
        }

        public override DTO.ProductMng.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of products");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.ProductMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete Product " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.ProductMng.Product dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update Product " + id.ToString());

            // query data
            if (id == 0)
                dtoItem.CreatedBy = iRequesterID;
            else
                dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.ProductMng.Product dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "approve Product " + id.ToString());

            // query data
            return factory.Approve(id, ref dtoItem, out notification);
        }

        public override bool Reset(int id, ref DTO.ProductMng.Product dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "reset Product " + id.ToString());

            // query data
            return factory.Reset(id, ref dtoItem, out notification);
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.ProductMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            return factory.GetFilterData(out notification);
        }

        public DTO.ProductMng.EditFormData GetData(int id, int modelID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get Product " + id.ToString());

            return factory.GetData(id, modelID, out notification);
        }

        public bool GeneratePreparingDataPieceEANCode(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GeneratePreparingDataPieceEANCode(id, out notification);
        }

        public bool GenerateEANCode(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GenerateEANCode(id, out notification);
        }

        public bool GenerateEANCodeForSET(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GenerateEANCodeForSET(id, out notification);
        }

        public bool ResetEANCodeForSET(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.ResetEANCodeForSET(id, out notification);
        }

        public bool GeneratePieceCode(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GeneratePieceCode(id, out notification);
        }

        //ean code new feature
        public object CreateSetEanCode(int productID, string EANCode, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.CreateSetEanCode(productID, EANCode, out notification);
        }
        public object CreateColli(int productSetEANCodeID, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.CreateColli(productSetEANCodeID, out notification);
        }
        public object UpdateColli(int productColliID, DTO.ProductMng.ProductColli dataItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateColli(productColliID, dataItem, out notification);
        }
        public object CreateColliPiece(int productColliID, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.CreateColliPiece(productColliID, out notification);
        }

        //delete 
        public object DeleteSetEanCode(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.DeleteSetEanCode(id, out notification);
        }
        public object DeleteColli(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.DeleteColli(id, out notification);
        }
        public object DeleteColliPiece(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.DeleteColliPiece(id, out notification);
        }

        //update
        public object UpdateColliPiece(DTO.ProductMng.ProductColliPiece dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.UpdateColliPiece(dtoItem, out notification);
        }

        public string PrintEANCode(int ProductColliPieceID, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.PrintEANCode(ProductColliPieceID, out notification);
        }
    }
}
