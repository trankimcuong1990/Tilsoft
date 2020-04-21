using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GeneralTool
{
    public partial class Form1 : Form
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public Form1()
        {
            InitializeComponent();

            // 
            // custom configuration        
            //
            FrameworkSetting.Setting.AbsoluteFileFolder = System.Configuration.ConfigurationManager.AppSettings["FileUrl"];
            FrameworkSetting.Setting.AbsoluteThumbnailFolder = System.Configuration.ConfigurationManager.AppSettings["ThumbnailUrl"];
            FrameworkSetting.Setting.AbsoluteReportTmpFolder = System.Configuration.ConfigurationManager.AppSettings["ReportTempUrl"];
            FrameworkSetting.Setting.AbsoluteBaseFolder = System.Configuration.ConfigurationManager.AppSettings["BaseFolder"];
            FrameworkSetting.Setting.SqlConnection = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
        }

        private void cmdParse_tabFileInFoder_Click(object sender, EventArgs e)
        {
            txtLog_tabFileInFolder.Text = string.Empty;
            txtResult_tabFileInFolder.Text = string.Empty;

            if (!string.IsNullOrEmpty(txtFolder_tabFileInFoder.Text))
            {
                string path = txtFolder_tabFileInFoder.Text;
                try
                {
                    if (!Directory.Exists(path))
                    {
                        throw new Exception("Directory [" + path + "] does not exists!");
                    }

                    foreach (FileInfo fInfo in (new DirectoryInfo(path)).GetFiles())
                    {
                        txtResult_tabFileInFolder.Text += fInfo.Name + Environment.NewLine;
                    }
                    MessageBox.Show("Done!");
                }
                catch (Exception ex)
                {
                    txtLog_tabFileInFolder.Text = GetInnerException(ex).Message;
                }
            }

            Refresh();
        }

        // private
        private Exception GetInnerException(Exception ex)
        {
            Exception exception = ex;
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
            return exception;
        }

        private void cmdCopy_tabCopyFileFromList_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSource_tabCopyFileFromList.Text) && !string.IsNullOrEmpty(txtDestination_tabCopyFileFromList.Text) && !string.IsNullOrEmpty(txtFileList_tabCopyFileFromList.Text))
            {
                foreach (string fileName in txtFileList_tabCopyFileFromList.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    if (File.Exists(txtSource_tabCopyFileFromList.Text + fileName.ToLower()))
                    {
                        File.Copy(txtSource_tabCopyFileFromList.Text + fileName.ToLower(), txtDestination_tabCopyFileFromList.Text + fileName.ToLower());
                    }                    
                }
                MessageBox.Show("Done!");
            }

            Refresh();
        }

        private void cmdImport_tabTILSOFTFile_Click(object sender, EventArgs e)
        {            
            string uniqueFileName = Path.GetTempPath() + @"\" + System.Guid.NewGuid().ToString().Replace("-", "") + ".xlsx";
            string pointer = string.Empty;
            System.IO.FileInfo epFInfo = new System.IO.FileInfo(uniqueFileName);
            OfficeOpenXml.ExcelPackage ePackagae = new OfficeOpenXml.ExcelPackage(epFInfo);
            OfficeOpenXml.ExcelWorksheet pWS = ePackagae.Workbook.Worksheets.Add("Result");
            List<DTO.FilePointer> result = new List<DTO.FilePointer>();
            foreach (FileInfo fInfo in (new DirectoryInfo(FrameworkSetting.Setting.AbsoluteReportTmpFolder)).GetFiles())
            {
                pointer = string.Empty;
                DTO.FilePointer fPointer = new DTO.FilePointer() { PhysicalFileName = fInfo.Name};
                try
                {
                    fPointer.FilePointerUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteReportTmpFolder, fInfo.Name, string.Empty);
                    fwFactory.CreateImageCache(fPointer.FilePointerUD, 120, 120, false);
                    result.Add(fPointer);
                }
                catch (Exception ex)
                {
                }
            }
            pWS.Cells["A1"].LoadFromCollection(result, true);
            ePackagae.Save();
            System.Diagnostics.Process.Start(uniqueFileName);
        }

        private void cmdRemove_tabTILSOFTFile_Click(object sender, EventArgs e)
        {
            foreach (string filePointerUD in txtFileList_tabTILSOFTFile.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                fwFactory.RemoveImageFile(filePointerUD);
            }
            MessageBox.Show("done");
        }

        private void cmdImportToFilesTable_Click(object sender, EventArgs e)
        {
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            foreach (FileInfo fInfo in (new DirectoryInfo(txtFolderToImport.Text)).GetFiles())
            {
                fwFactory.CreateNoneImageFilePointer(txtFolderToImport.Text, fInfo.Name, string.Empty);
            }
        }
    }
}
