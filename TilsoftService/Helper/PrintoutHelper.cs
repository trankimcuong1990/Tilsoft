using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Microsoft.Reporting.WebForms;
using FrameworkSetting;
namespace TilsoftService.Helper
{
    public static class PrintoutHelper
    {
        public static string BuildPrintoutFile(LocalReport lr,string fileTypeID)
        {
            string reportType = fileTypeID;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + fileTypeID + "</OutputFormat>" +
            "  <PageWidth>21cm</PageWidth>" +
            "  <PageHeight>29.7cm</PageHeight>" +
            "  <MarginTop>0.5cm</MarginTop>" +
            "  <MarginLeft>2cm</MarginLeft>" +
            "  <MarginRight>1cm</MarginRight>" +
            "  <MarginBottom>0cm</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            //SAVE BYTE ARRAY TO FILE
            string filename = System.Guid.NewGuid().ToString().Replace("-", "");
            switch (fileTypeID)
            {
                case "PDF" :
                    filename = filename + ".pdf";
                    break;
                case "Excel":
                    filename = filename + ".xls";
                    break;
                case "Word":
                    filename = filename + ".doc";
                    break;
                case "Image":
                    filename = filename + ".jpg";
                    break;
                default:
                    break;
            }
            string fullpath = Setting.AbsoluteReportTmpFolder + filename;

            MemoryStream ms = new MemoryStream(renderedBytes);
            FileStream fs = File.OpenWrite(fullpath);
            ms.WriteTo(fs);
            fs.Close();

            return filename;
        }

        public static string BuildPrintoutFileCMR(LocalReport lr, string fileTypeID)
        {
            string reportType = fileTypeID;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + fileTypeID + "</OutputFormat>" +
            "  <PageWidth>21cm</PageWidth>" +
            "  <PageHeight>29.7cm</PageHeight>" +
            "  <MarginTop>0.5cm</MarginTop>" +
            "  <MarginLeft>1.3cm</MarginLeft>" +
            "  <MarginRight>0cm</MarginRight>" +
            "  <MarginBottom>0cm</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            //SAVE BYTE ARRAY TO FILE
            string filename = System.Guid.NewGuid().ToString().Replace("-", "");
            switch (fileTypeID)
            {
                case "PDF":
                    filename = filename + ".pdf";
                    break;
                case "Excel":
                    filename = filename + ".xlsx";
                    break;
                case "Word":
                    filename = filename + ".doc";
                    break;
                case "Image":
                    filename = filename + ".jpg";
                    break;
                default:
                    break;
            }
            string fullpath = Setting.AbsoluteReportTmpFolder + filename;

            MemoryStream ms = new MemoryStream(renderedBytes);
            FileStream fs = File.OpenWrite(fullpath);
            ms.WriteTo(fs);
            fs.Close();

            return filename;
        }
    }
}