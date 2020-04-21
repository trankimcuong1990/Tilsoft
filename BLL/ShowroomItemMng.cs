using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ShowroomItemMng : Lib.BLLBase2<DTO.ShowroomItemMng.SearchFormData,DTO.ShowroomItemMng.EditFormData,DTO.ShowroomItemMng.ShowroomItem>
    {
        private DAL.ShowroomItemMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public ShowroomItemMng() {
            factory = new DAL.ShowroomItemMng.DataFactory();
        }
       
        public ShowroomItemMng(string tempFolder)
        {
            factory = new DAL.ShowroomItemMng.DataFactory(tempFolder);
        }
        
        public override bool Approve(int id, ref DTO.ShowroomItemMng.ShowroomItem dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.DeleteData(id, out notification);
        }
        public override DTO.ShowroomItemMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }
        public override DTO.ShowroomItemMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public override bool Reset(int id, ref DTO.ShowroomItemMng.ShowroomItem dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool UpdateData(int id, ref DTO.ShowroomItemMng.ShowroomItem dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public string GetExportExcelFile(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetExportExcelFile(out notification);
        }

        public  List<DTO.ShowroomItemMng.SampleProduct> QuickSearchSampleProduct(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchSampleProduct(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
    }
}
