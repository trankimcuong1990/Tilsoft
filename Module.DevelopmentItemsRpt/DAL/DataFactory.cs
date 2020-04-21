using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.DevelopmentItemsRpt.DTO;

namespace Module.DevelopmentItemsRpt.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {

        DevelopmentItemsEntities Crecontext()
        {
            return new DevelopmentItemsEntities(Library.Helper.CreateEntityConnectionString("DAL.DevelopmentItemsModel"));
        }

        private Support.DAL.DataFactory spFactory = new Support.DAL.DataFactory();
        Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        DataConverter DataConverter = new DataConverter();

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;

            notification = new Notification() { Type = NotificationType.Success };

            DTO.SearchFormData data = new SearchFormData();
            data.Data = new List<DevelopmentItemSearchResult>();
            data.seasons = new List<Support.DTO.Season>();
            data.sampleProductStatuses = new List<Support.DTO.SampleProductStatus>();

            string season = null;
            int? sampleProductStatusID = null;

            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                season = filters["Season"].ToString().Replace("'", "''");
            }
            //string season = filters["Season"]?.ToString().Replace("'", "''");
            string clientUD = filters["ClientUD"]?.ToString().Replace("'", "''");
            string vnSuggestedFactoryUD = filters["VNSuggestedFactoryUD"]?.ToString().Replace("'", "''");
            string articleDescription = filters["ArticleDescription"]?.ToString().Replace("'", "''");
            string materialDescription = filters["MaterialDescription"]?.ToString().Replace("'", "''");
            string materialTypeDescription = filters["MaterialTypeDescription"]?.ToString().Replace("'", "''");
            string materialColorDescription = filters["MaterialColorDescription"]?.ToString().Replace("'", "''");
            string material2Description = filters["Material2Description"]?.ToString().Replace("'", "''");
            string material2TypeDescription = filters["Material2TypeDescription"]?.ToString().Replace("'", "''");
            string material2ColorDescription = filters["Material2ColorDescription"]?.ToString().Replace("'", "''");
            string material3Description = filters["Material3Description"]?.ToString().Replace("'", "''");
            string material3TypeDescription = filters["Material3TypeDescription"]?.ToString().Replace("'", "''");
            string seatCushionDescription = filters["SeatCushionDescription"]?.ToString().Replace("'", "''");
            string backCushionDescription = filters["BackCushionDescription"]?.ToString().Replace("'", "''");
            string cushionColorDescription = filters["CushionColorDescription"]?.ToString().Replace("'", "''");
            string seatCushionSpecification = filters["SeatCushionSpecification"]?.ToString().Replace("'", "''");
            string backCushionSpecification = filters["BackCushionSpecification"]?.ToString().Replace("'", "''");
            if (filters.ContainsKey("SampleProductStatusID") && filters["SampleProductStatusID"] != null && !string.IsNullOrEmpty(filters["SampleProductStatusID"].ToString()))
            {
                sampleProductStatusID = Convert.ToInt32(filters["SampleProductStatusID"]);
            }

            try
            {
               using(var context = Crecontext())
                {
                    totalRows = context.DevelopmentItemsRpt_Function_DevelopmentItemsSearchView(season, clientUD, vnSuggestedFactoryUD, articleDescription, materialDescription, materialTypeDescription, materialColorDescription, material2Description, material2TypeDescription, material2ColorDescription, material3Description, material3TypeDescription, seatCushionDescription, backCushionDescription, cushionColorDescription, seatCushionSpecification, backCushionSpecification, sampleProductStatusID, orderBy, orderDirection).Count();
                    var result = context.DevelopmentItemsRpt_Function_DevelopmentItemsSearchView(season, clientUD, vnSuggestedFactoryUD, articleDescription, materialDescription, materialTypeDescription, materialColorDescription, material2Description, material2TypeDescription, material2ColorDescription, material3Description, material3TypeDescription, seatCushionDescription, backCushionDescription, cushionColorDescription, seatCushionSpecification, backCushionSpecification, sampleProductStatusID, orderBy, orderDirection);
                    data.Data = DataConverter.DB2DTO_SearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.seasons = spFactory.GetSeason().ToList();
                    data.sampleProductStatuses = spFactory.GetSampleProductStatus().ToList();
                }
            }
            catch (Exception ex)
            {

                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
            }
            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
