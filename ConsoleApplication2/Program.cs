using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data.Entity.Validation;
using System.Data;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("PRESS ENTER TO EXECUTE!");
            Console.ReadLine();

            Console.WriteLine(Path.GetTempPath());
            Console.WriteLine(Path.GetTempFileName());

            //using (DAL.TESTDBEntities context = new DAL.TESTDBEntities())
            //{
            //    DAL.Company dbComp = new DAL.Company();
            //    dbComp.CompanyUD = "AVS";
            //    dbComp.CompanyNM = "AU VIET SOFT";

            //    dbComp.Employee.Add(new DAL.Employee { EmployeeUD = "01", EmployeeNM = "NV-01"});
            //    dbComp.Employee.Add(new DAL.Employee { EmployeeUD = "02", EmployeeNM = "NV-02" });

            //    dbComp.CompanyStatus.Add(new DAL.CompanyStatus { IsCurrent = true, StatusID = 1, StatusUpdatedDate = DateTime.Now });
            //    dbComp.OperationStatus.Add(new DAL.OperationStatus { IsCurrent = true, StatusID = 1, StatusUpdatedDate = DateTime.Now });

            //    context.Company.Add(dbComp);
            //    context.SaveChanges();
            //}

            Console.WriteLine("PRESS ENTER TO QUIT!");
            Console.ReadLine();
        }
    }
}
