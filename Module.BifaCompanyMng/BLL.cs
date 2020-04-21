namespace Module.BifaCompanyMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public object GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }
        public object GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }
        public object GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public bool UpdateData(int iRequesterID, int id, ref object viewItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(iRequesterID, id, ref viewItem, out notification);
        }

        public object UpdateBifaCity(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.UpdateBifaCity(filters, out notification);
        }
        public object DeleteBifaCity(int iRequesterID, int bifaCityID, out Library.DTO.Notification notification)
        {
            return factory.DeleteBifaCity(bifaCityID, out notification);
        }

        public object UpdateBifaIndustry(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.UpdateBifaIndustry(filters, out notification);
        }
        public object DeleteBifaIndustry(int iRequesterID, int bifaIndustryID, out Library.DTO.Notification notification)
        {
            return factory.DeleteBifaIndustry(bifaIndustryID, out notification);
        }

        public object GetBifaAddress(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetBifaAddress(id, out notification);
        }
        public object UpdateBifaAddress(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.UpdateBifaAddress(iRequesterID, filters, out notification);
        }
        public object DeleteBifaAddress(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteBifaAddress(id, out notification);
        }

        public object UpdateBifaClub(int iRequesterID, int id, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.UpdateBifaClub(iRequesterID, id, filters, out notification);

        }
        public object DeleteBifaClub(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteBifaClub(id, out notification);
        }

        public object UpdateBifaClubMember(int iRequesterID, int id, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.UpdateBifaClubMember(iRequesterID, id, filters, out notification);
        }
        public object DeleteBifaClubMember(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteBifaClubMember(id, out notification);
        }

        public object UpdateBifaEmailAddress(int iRequesterID, int id, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.UpdateBifaEmailAddress(iRequesterID, id, filters, out notification);
        }
        public object DeleteBifaEmailAddress(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteBifaEmailAddress(id, out notification);
        }

        public object UpdateTelephone(int iRequesterID, int id, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.UpdateTelephone(iRequesterID, id, filters, out notification);
        }
        public object DeleteTelephone(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteTelephone(id, out notification);
        }

        public object UpdatePerson(int iRequesterID, int id, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.UpdatePerson(iRequesterID, id, filters, out notification);
        }
        public object DeletePerson(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeletePerson(id, out notification);
        }

        public object UpdateEvent(int iRequesterID, int id, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.UpdateEvent(iRequesterID, id, filters, out notification);
        }
        public object DeleteEvent(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteEvent(id, out notification);
        }
    }
}
