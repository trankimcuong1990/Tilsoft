using System.Configuration;
using System.Collections.Generic;

namespace FrameworkSetting
{
    public static class Setting
    {
        public static string SqlConnection = "";
        public static bool EnabledLog = false;

        public static string AbsoluteBaseFolder = "";
        public static string AbsoluteFileFolder = "";
        public static string AbsoluteThumbnailFolder = "";
        public static string AbsoluteUserTempFolder = "";
        public static string AbsoluteReportTmpFolder = "";
        public static string AbsoluteReportFolder = "";

        public static string ThumbnailUrl = "";
        public static string FullSizeUrl = "";
        public static string ReportTempUrl = "";
        public static string ServiceUrl = "";

        public static string MediaUrl = "";
        public static string MediaThumbnailUrl 
        {
            get 
            {
                return MediaUrl + "thumbnail/";
            }
        }
        public static string MediaFullSizeUrl
        {
            get
            {
                return MediaUrl + "file/";
            }
        }

        public static string SMTPRelayServer = "";
        public static string SMTPServer = "";
        public static bool SMTPSSL = false;
        public static string SMTPUsername = "";
        public static string SMTPPassword = "";
        public static int SMTPPort = 25;
        public static string SMTPEmailAddress = "";
        public static List<string> Maps = new List<string>();

        public static string FrontendUrl = "";
    }
}
