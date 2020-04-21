namespace Module.PriceListClientCharge
{
    using Library.DTO;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class BLL
    {
        #region ** Variable & Constant **

        DAL.DataFactory factory = new DAL.DataFactory();

        Framework.BLL bll = new Framework.BLL();

        #endregion

        #region ** Constructors **

        public BLL()
        {
        }

        #endregion

        #region ** Function & Method **

        public DTO.SearchData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditData GetData(int iRequesterID, int id, out Notification notification)
        {
            return factory.GetData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Notification notification)
        {
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        #endregion
    }
}
