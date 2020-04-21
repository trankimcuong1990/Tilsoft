using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ExportDataTool
{
    class Program
    {
        static void Main(string[] args)
        {
            ExportProductCategory();

            Console.WriteLine("End of process, press enter to quit!");
            Console.ReadLine();
        }

        static void ExportProductCategory()
        {
            Console.WriteLine("Exporting Category ... ");
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("AdHoc_function_ExportRawData", new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnection"].ToString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                adap.Fill(ds);
                if (System.IO.File.Exists(Environment.CurrentDirectory + @"\TILSOFTRawData.xlsx"))
                {
                    try
                    {
                        System.IO.File.Delete(Environment.CurrentDirectory + @"\TILSOFTRawData.xlsx");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(Library.Helper.HandleExceptionSingleLine(ex));
                        return;
                    }
                }

                Library.Helper.ExportDataSetWithEPPlus(ds, Environment.CurrentDirectory + @"\TILSOFTRawData.xlsx");
                Console.WriteLine("done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(Library.Helper.HandleExceptionSingleLine(ex));
                return;
            }            
        }
    }
}
