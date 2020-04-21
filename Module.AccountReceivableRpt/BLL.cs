using System.Collections.Generic;

namespace Module.AccountReceivableRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.LiabilitiesDTO GetLiabilities(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetLiabilities(userId, filters, out notification);
        }      
        public List<DTO.SupportFactoryRawMaterialDTO> QuerySupplier(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            return factory.QuerySupplier(param, out notification);
        }
        public object GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(iRequesterID, out notification);
        }
    }
}
