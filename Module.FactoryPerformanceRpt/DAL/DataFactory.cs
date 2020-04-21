using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPerformanceRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private FactoryPerformanceRptEntities CreateContext()
        {
            return new FactoryPerformanceRptEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryPerformanceRptModel"));
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
        // CUSTOM FUNCTIONS
        //
        public DTO.HtmlReportData GetHTMLReportData(int userID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.HtmlReportData data = new DTO.HtmlReportData();
            data.AnnualShippedData = new List<DTO.AnnualShipped>();
            data.WeeklyShippedData = new List<DTO.WeeklyShipped>();
            data.WeekInfoData = new List<DTO.WeekInfo>();
            data.FactoryInfoData = new List<DTO.FactoryInfo>();
            data.TotalCapacityAndOrderDatas = new List<DTO.TotalCapacityAndOrderData>();
            data.TotalCapacityAndOrderByWeekDatas = new List<DTO.TotalCapacityAndOrderByWeekData>();

            //try to get data
            try
            {
                Task task1 = Task.Factory.StartNew(() =>
                {
                    using (FactoryPerformanceRptEntities context = CreateContext())
                    {
                        data.WeekInfoData = converter.DB2DTO_WeekInfo(context.FactoryPerformanceRpt_WeekInfoReportData_View.Where(o => o.Season == season).ToList());
                    }
                });

                Task task2 = Task.Factory.StartNew(() =>
                {
                    using (FactoryPerformanceRptEntities context = CreateContext())
                    {
                        data.WeeklyShippedData = converter.DB2DTO_WeeklyShipped(context.FactoryPerformanceRpt_function_GetWeeklyShippedReportData(season).ToList());
                    }
                });
                Task task3 = Task.Factory.StartNew(() =>
                {
                    using (FactoryPerformanceRptEntities context = CreateContext())
                    {
                        data.AnnualShippedData = converter.DB2DTO_AnnualShipped(context.FactoryPerformanceRpt_function_GetYearlyShippedReportData(season).ToList());
                    }
                });
                Task task4 = Task.Factory.StartNew(() =>
                {
                    using (FactoryPerformanceRptEntities context = CreateContext())
                    {
                        data.FactoryInfoData = converter.DB2DTO_FactoryInfo(context.FactoryPerformanceRpt_FactoryInfoReportData_View.Where(o => o.Season == season).OrderBy(o => o.FactoryUD).ToList());
                    }
                });

                Task task5 = Task.Factory.StartNew(() =>
                {
                    using (FactoryPerformanceRptEntities context = CreateContext())
                    {
                        data.WeeklyProducedData = converter.DB2DTO_WeeklyProduced(context.FactoryPerformanceRpt_function_GetWeeklyProducedReportData(season).ToList());
                    }
                });
                Task task6 = Task.Factory.StartNew(() =>
                {
                    using (FactoryPerformanceRptEntities context = CreateContext())
                    {
                        data.AnnualProducedData = converter.DB2DTO_AnnualProduced(context.FactoryPerformanceRpt_function_GetYearlyProducedReportData(season).ToList());
                    }
                });
                Task task7 = Task.Factory.StartNew(() =>
                {
                    using (FactoryPerformanceRptEntities context = CreateContext())
                    {
                        data.TotalCapacityAndOrderDatas = converter.DB2DTO_TotalCapacityAndOrder(context.FactoryPerformanceRpt_function_TotalCapacityAndOrder(season, userID).ToList());
                        data.TotalCapacityAndOrderByWeekDatas = converter.DB2DTO_TotalCapacityAndOrderByWeek(context.FactoryPerformanceRpt_function_TotalCapacityAndOrderByWeekOfFactory(season, userID).ToList());
                    }
                });
                Task.WaitAll(task1, task2, task3, task4, task5, task6, task7);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
    }
}
