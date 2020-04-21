using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationShell
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BEGIN: ...");
            Console.ReadLine();

            //
            // GET SETTING
            //
            FrameworkSetting.Setting.SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"];
            FrameworkSetting.Setting.SMTPRelayServer = System.Configuration.ConfigurationManager.AppSettings["SMTPRelayServer"];
            FrameworkSetting.Setting.SMTPUsername = System.Configuration.ConfigurationManager.AppSettings["SMTPUsername"];
            FrameworkSetting.Setting.SMTPPassword = System.Configuration.ConfigurationManager.AppSettings["SMTPPassword"];
            FrameworkSetting.Setting.SMTPEmailAddress = System.Configuration.ConfigurationManager.AppSettings["SMTPEmailAddress"];
            FrameworkSetting.Setting.SMTPPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMTPPort"]);
            if (System.Configuration.ConfigurationManager.AppSettings["SMTPSSL"] == "1")
            {
                FrameworkSetting.Setting.SMTPSSL = true;
            }
            FrameworkSetting.Setting.SqlConnection = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;

            System.Configuration.Configuration configFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationShell.exe");
            var config = ConfigurationManager.GetSection("registeredTasks") as PluginTaskSection;
            NotificationBase.ITask executor;
            foreach (PluginTaskElement node in config.Instances)
            {                
                executor = (NotificationBase.ITask)Activator.CreateInstance(Type.GetType(node.Code));
                //Console.WriteLine("Enter to execute task: " + executor.GetDescription());
                //Console.ReadLine();

                Console.Write("Performing task: " + executor.GetDescription() + "...");
                executor.ExecuteTask();
                Console.WriteLine("DONE!");
            }
        }
    }
}
