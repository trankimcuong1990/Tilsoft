namespace Module.BifaCompanyMng
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public object CustomFunction(int userID, string identifier, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString().Trim())) ? System.Convert.ToInt32(filters["id"].ToString().Trim()) : 0;

            switch (identifier.ToLower())
            {
                case "updatebifacity":
                    return bll.UpdateBifaCity(userID, filters, out notification);
                case "deletebifacity":
                    return bll.DeleteBifaCity(userID, id, out notification);

                case "updatebifaindustry":
                    return bll.UpdateBifaIndustry(userID, filters, out notification);
                case "deletebifaindustry":
                    return bll.DeleteBifaIndustry(userID, id, out notification);

                case "getbifaaddress":
                    return bll.GetBifaAddress(userID, id, out notification);
                case "updatebifaaddress":
                    return bll.UpdateBifaAddress(userID, filters, out notification);
                case "deletebifaaddress":
                    return bll.DeleteBifaAddress(userID, id, out notification);

                case "updatebifaclub":
                    return bll.UpdateBifaClub(userID, id, filters, out notification);
                case "deletebifaclub":
                    return bll.DeleteBifaClub(userID, id, out notification);

                case "updatebifaclubmember":
                    return bll.UpdateBifaClubMember(userID, id, filters, out notification);
                case "deletebifaclubmember":
                    return bll.DeleteBifaClubMember(userID, id, out notification);

                case "updatebifaemailaddress":
                    return bll.UpdateBifaEmailAddress(userID, id, filters, out notification);
                case "deletebifaemailaddress":
                    return bll.DeleteBifaEmailAddress(userID, id, out notification);

                case "updatetelephone":
                    return bll.UpdateTelephone(userID, id, filters, out notification);
                case "deletetelephone":
                    return bll.DeleteTelephone(userID, id, out notification);

                case "updateperson":
                    return bll.UpdatePerson(userID, id, filters, out notification);
                case "deleteperson":
                    return bll.DeletePerson(userID, id, out notification);

                case "updateevent":
                    return bll.UpdateEvent(userID, id, filters, out notification);
                case "deleteevent":
                    return bll.DeleteEvent(userID, id, out notification);

                default:
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = "Can not matched to excute handle!";
                    return null;
            }
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            return bll.GetData(userId, id, out notification);
        }

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return bll.GetInitData(userId, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }
    }
}
