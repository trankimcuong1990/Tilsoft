//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BLL
//{
//    public class LabelingPackagingMng : Lib.BLLBase2<DTO.LabelingPackagingMng.LabelingPackagingSearchResult , DTO.LabelingPackagingMng.EditFormData, DTO.LabelingPackagingMng.LabelingPackaging>
//    {
//        private DAL.LabelingPackagingMng.DataFactory factory;
//        private BLL.Framework fwBLL = new Framework();

//        public LabelingPackagingMng()
//        {
//            factory = new DAL.LabelingPackagingMng.DataFactory();
//        }
//        public LabelingPackagingMng(string tempFolder)
//        {
//            factory = new DAL.LabelingPackagingMng.DataFactory(tempFolder);
//        }

//        //public override IEnumerable<DTO.LabelingPackagingMng.LabelingPackagingSearchResult> GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
//        //{
//        //    // keep log entry
//        //    fwBLL.WriteLog(iRequesterID, 0, "get list of offer");

//        //    // query data
//        //    return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
//        //}

//        //public override DTO.LabelingPackagingMng.LabelingPackaging  GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
//        {
//            throw new NotImplementedException();
//            // keep log entry
//            //fwBLL.WriteLog(iRequesterID, 0, "delete labeling" + id.ToString());
//            //return factory.DeleteData(id, iRequesterID, out notification);
//        }

//        public override bool UpdateData(int id, ref DTO.LabelingPackagingMng.LabelingPackaging  dtoItem, int iRequesterID, out Library.DTO.Notification notification)
//        {
//            fwBLL.WriteLog(iRequesterID, 0, "update material " + id.ToString());

//            // query data
//            return factory.UpdateData(id, ref dtoItem, out notification);
//        }
//        public override DTO.LabelingPackagingMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
//        {
//            // keep log entry
//            fwBLL.WriteLog(iRequesterID, 0, "get material " + id.ToString());

//            // query data
//            return factory.GetData(id, out notification);
//        }
//        //public DTO.LabelingPackagingMng.SupportDataContainer GetSupportData(out Library.DTO.Notification notification)
//        //{
//        //    //return factory.GetSupportData(out notification);

//        //    throw new NotImplementedException();
//        //}

//        //public DTO.OfferMng.DataContainer GetDataContainer(int id, int iRequesterID, out Library.DTO.Notification notification)
//        //{
//        //    // keep log entry
//        //    //fwBLL.WriteLog(iRequesterID, 0, "get offer " + id.ToString());
//        //    throw new NotImplementedException();
//        //    //// query data
//        //    //return factory.GetDataContainer(id, out notification);
//        //}

       

//        public bool Confirm(int id, int actionType, ref DTO.OfferMng.Offer dtoItem, int iRequesterID, out Library.DTO.Notification notification)
//        {
//            // keep log entry
//            throw new NotImplementedException();

//        }

//        public bool Revise(int id, int actionType, ref DTO.OfferMng.Offer dtoItem, int iRequesterID, out Library.DTO.Notification notification)
//        {
//            // keep log entry
//            throw new NotImplementedException();
//        }

//        public bool UpdateData(int id, int actionType, ref DTO.OfferMng.Offer dtoItem, int iRequesterID, out Library.DTO.Notification notification)
//        {
//            // keep log entry
//            throw new NotImplementedException();
//        }

   

//        public string GetPrintDataOffer(int offerID, int iRequesterID, out Library.DTO.Notification notification)
//        {
//            throw new NotImplementedException();
//        }


        

//        public bool UploadOfferScanFile(int offerID, bool offerScanFileHasChange, string newOfferScanFile, out string offerScanFile, out string concurrencyFlag_String, out Library.DTO.Notification notification)
//        {
//             throw new NotImplementedException();
//                //return factory.UploadOfferScanFile(offerID, offerScanFileHasChange, newOfferScanFile, out offerScanFile, out concurrencyFlag_String, out notification);
//        }

//        public override DTO.LabelingPackagingMng.LabelingPackagingSearchResult GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
//        {
//            throw new NotImplementedException();
//        }


//        public override bool Approve(int id, ref DTO.LabelingPackagingMng.LabelingPackaging dtoItem, int iRequesterID, out Library.DTO.Notification notification)
//        {
//            throw new NotImplementedException();
//        }

//        public override bool Reset(int id, ref DTO.LabelingPackagingMng.LabelingPackaging dtoItem, int iRequesterID, out Library.DTO.Notification notification)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
