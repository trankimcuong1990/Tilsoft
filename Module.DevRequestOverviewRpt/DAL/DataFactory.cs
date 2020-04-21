using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestOverviewRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private DateTime tmpDate;
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private DevRequestOverviewRptEntities CreateContext()
        {
            return new DevRequestOverviewRptEntities(Library.Helper.CreateEntityConnectionString("DAL.DevRequestOverviewRptModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.ReportFormData GetOverview(int year, out Library.DTO.Notification notification)
        {
            DTO.ReportFormData data = new DTO.ReportFormData();
            data.Overviews = new List<DTO.Overview>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = string.Empty };
            try
            {
                using (DevRequestOverviewRptEntities context = CreateContext())
                {
                    data.Overviews = converter.DB2DTO_Overview(context.DevRequestOverviewRpt_function_getOverview(year).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.DetailFormData GetDetail(int year, int userId, out Library.DTO.Notification notification)
        {
            DTO.DetailFormData data = new DTO.DetailFormData();
            data.DetailByPersons = new List<DTO.DetailByPerson>();
            data.PlaningByPersons = new List<DTO.PlaningByPerson>();
            data.ResolvedRequestByPersons = new List<DTO.ResolvedRequestByPerson>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = string.Empty };
            try
            {
                using (DevRequestOverviewRptEntities context = CreateContext())
                {
                    data.DetailByPersons = converter.DB2DTO_DetailByPerson(context.DevRequestOverviewRpt_function_getDetailByPerson(year, userId).ToList());
                    data.PlaningByPersons = converter.DB2DTO_PlaningByPerson(context.DevRequestOverviewRpt_function_getPlaningByPerson(year, userId).ToList());
                    data.ResolvedRequestByPersons = converter.DB2DTO_ResolvedRequestByPerson(context.DevRequestOverviewRpt_function_getResolvedRequestByPerson(year, userId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            if (data.PlaningByPersons.Count() > 0)
            {
                DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);
                // fill in start date and end date
                foreach (DTO.PlaningByPerson dtoPlaning in data.PlaningByPersons)
                {
                    dtoPlaning.start_string = currentDate.ToString("dd-MM-yyyy HH:mm");
                    if (dtoPlaning.EstWorkingHour > 8)
                    {
                        currentDate = currentDate.AddDays(dtoPlaning.EstWorkingHour.Value / 8);
                        dtoPlaning.end_string = currentDate.ToString("dd-MM-yyyy HH:mm");
                    }
                    else
                    {
                        dtoPlaning.end_string = dtoPlaning.start_string;
                    }
                    currentDate = currentDate.AddDays(1);
                }
            }

            return data;
        }
    }
}
