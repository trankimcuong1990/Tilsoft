using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class ErrorHelper
    {
        public static void DataExceptionParser(System.Data.DataException ex, out int number, out string indexName)
        {
            number = -1;
            indexName = string.Empty;

            if (ex.InnerException is System.Data.Entity.Core.UpdateException)
            {
                SqlException sqlEx = (SqlException)ex.InnerException.InnerException;
                number = sqlEx.Number;

                switch (sqlEx.Number)
                {
                    case 2601:
                        indexName = SQL2601Parser(sqlEx.Message);
                        break;

                    case 2627:
                        indexName = SQL2627Parser(sqlEx.Message);
                        break;
                }
            }
        }

        public static string DataExceptionParser(System.Data.DataException ex)
        {
            if (ex.InnerException is System.Data.Entity.Core.UpdateException)
            {
                SqlException sqlEx = (SqlException)ex.InnerException.InnerException;
                return sqlEx.Message;
            }
            return string.Empty;
        }

        private static string SQL2601Parser(string errMsg)
        {
            try
            {
                return errMsg.Split(new[] { '\r', '\n' })[0].Split('\'')[3];
            }
            catch 
            {
                return string.Empty;
            }            
        }

        private static string SQL2627Parser(string errMsg)
        {
            try
            {
                return errMsg.Split(new[] { '\r', '\n' })[0].Split('\'')[1];
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
