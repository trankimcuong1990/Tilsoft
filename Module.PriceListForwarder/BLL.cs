namespace Module.PriceListForwarder
{
    using DAL;
    using DTO;
    using Library.DTO;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class BLL
    {
        #region ** Variables & Constants **

        DataFactory dataFactory = new DataFactory();
        Framework.BLL bll = new Framework.BLL();

        #endregion

        #region ** Constructors **

        public BLL()
        {
        }

        #endregion

        #region ** Events **

        /// <summary>
        /// Get data Price List Forwarder with id
        /// </summary>
        public EditData GetData(int userId, int id, out Notification notification)
        {
            // Keep log entry
            bll.WriteLog(userId, id, string.Format("Read data with id: {0}", id));
            return dataFactory.GetData(id, out notification);
        }

        /// <summary>
        /// Get list data Price List Forwarder with filters
        /// </summary>
        public SearchData GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string sortedBy, string sortedDirection, out int totalRows, out Notification notification)
        {
            // Keep log entry
            bll.WriteLog(userId, 0, string.Format("Read list data with filters: {0}", filters));
            return dataFactory.GetDataWithFilter(filters, pageSize, pageIndex, sortedBy, sortedDirection, out totalRows, out notification);
        }

        /// <summary>
        /// Update data Price List Forwarder
        /// </summary>
        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return dataFactory.UpdateData(userId, id, ref dtoItem, out notification);
        }

        #endregion
    }
}
