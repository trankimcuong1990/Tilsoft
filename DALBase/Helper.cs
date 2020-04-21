using System.Data.Entity.Core.EntityClient;
using System.Linq;
using AutoMapper;
using FrameworkSetting;
using System.Data;
using System.Xml;
using System;

namespace DALBase
{
    public static class Helper
    {
        private static System.Globalization.CultureInfo _FrontEndCulture = new System.Globalization.CultureInfo("vi-VN");

        public static string TEXT_CONCURRENCY_CONFLICT = "The data has been changed sinced the last time it was loaded into your screen, please reload";
        public static string TEXT_CONFIRMED_CONFLICT = "Can not make change to a confirmed object !";

        public static string TEXT_STATUS_PENDING = "PENDING";
        public static string TEXT_STATUS_CONFIRMED = "CONFIRMED";
        public static string TEXT_STATUS_CANCELED = "CANCELED";
        public static string TEXT_STATUS_REVISED = "REVISED";
        public static string TEXT_STATUS_REVISION_CONFIRMED = "REVISION CONFIRMED";
        public static string TEXT_STATUS_CREATED = "CREATED";

        public static int CREATED = 1;
        public static int UPDATE = 2;
        public static int CONFIRMED = 3;
        public static int REVISED = 4;
        public static int REVISION_CONFIRMED = 5;
        public static int CANCEL = 6;
        public static int ACKNOWLEDGE = 7;

        public static int FOB_INVOICE = 1;
        public static int WAREHOUSE_INVOICE = 2;
        public static int OTHER_INVOICE = 3;
        public static int CREDITNOTE_INVOICE = 4;
        public static int CONTAINER_TRANSPORT = 5;

        public static string COST_TYPE_DISCOUNT = "D";
        public static string COST_TYPE_SEA_FREIGHT = "S";
        public static string COST_TYPE_TRUCKING = "T";
        public static string COST_TYPE_OTHER = "O";
        public static string COST_TYPE_WAREHOUSE = "W";

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType == sourceType && x.DestinationType == destinationType);
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }

        public static string CreateEntityConnectionString(string edmxFileName)
        {
            // Initialize the EntityConnectionStringBuilder.
            EntityConnectionStringBuilder entityConnBuilder = new EntityConnectionStringBuilder
            {
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = Setting.SqlConnection,
                Metadata = @"res://*/{{edmx}}.csdl|res://*/{{edmx}}.ssdl|res://*/{{edmx}}.msl".Replace("{{edmx}}", edmxFileName)
            };

            //Set the provider name.

            // Set the provider-specific connection string.

            // Set the Metadata location.
            return entityConnBuilder.ToString();
        }

        public static string GetSQLConnectionString() 
        {
            return Setting.SqlConnection;
        }

        public static string CreateReportFiles(DataSet ds, string reportFileName)
        {
            string filename = System.Guid.NewGuid().ToString().Replace("-", "");
            string fullpath = Setting.AbsoluteReportTmpFolder + filename;

            try
            {
                ds.WriteXml(fullpath + ".xml");
                System.IO.File.Copy(Setting.AbsoluteReportFolder + reportFileName + ".xlsm", fullpath + ".xlsm");

                return filename;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static DateTime? ConvertStringToDateTime(this string dateString)
        {
            return dateString.ConvertStringToDateTime(_FrontEndCulture);
        }
        public static DateTime? ConvertStringToDateTime(this string dateString, System.Globalization.CultureInfo culture)
        {
            DateTime tmpDate;
            if (!string.IsNullOrEmpty(dateString) && DateTime.TryParse(dateString, culture, System.Globalization.DateTimeStyles.None, out tmpDate))
            {
                return tmpDate;
            }

            return null;
        }

        public static void DevCreateReportXMLSource(DataSet ds, string name)
        {
            string pathToExport = Setting.AbsoluteReportTmpFolder + "Dev/";

            XmlDocument xsdDoc = new XmlDocument();
            xsdDoc.LoadXml(ds.GetXmlSchema());
            xsdDoc.Save(pathToExport + name + ".xsd");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(ds.GetXml());
            xmlDoc.Save(pathToExport + name + ".xml");
        }

        public static string GetCurrentSeason()
        {
            int curYear = DateTime.Now.Year;
            if (DateTime.Now.Month >= 10)
            {
                return curYear.ToString() + "/" + (curYear + 1).ToString();
            }
            return (curYear - 1).ToString() + "/" + curYear.ToString();
        }
    }
}
