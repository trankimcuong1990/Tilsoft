namespace Module.PurchasingPriceComparisonMng
{
    internal class BLL
    {
        private DAL.DataFactory dataFactory = new DAL.DataFactory();
        private DAL.ReportFactory reportFactory = new DAL.ReportFactory();

        public object GetInitForm(int userID, out Library.DTO.Notification notification)
        {
            return dataFactory.GetInitForm(out notification);
        }

        public object GetReportData(int userID, string season, out Library.DTO.Notification notification)
        {
            return reportFactory.GetReportData(season, out notification);
        }
    }
}
