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

namespace AppTool
{
    public partial class Form1 : Form
    {
        private List<DTO.CustomFileInfo> files = new List<DTO.CustomFileInfo>();
        private string[] allowExtensions = { ".jpg", ".jpeg", ".bmp", ".gif", ".png" };

        public Form1()
        {
            InitializeComponent();
            bindingSource1.DataSource = files;
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            foreach (DTO.CustomFileInfo aFile in files.ToArray())
            {
                try
                {
                    File.Delete(aFile.FullPath);
                    files.Remove(aFile);
                }
                catch { }
            }
            bindingSource1.ResetBindings(false);
            Refresh();
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDiag = new FolderBrowserDialog();
            if (folderDiag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFolder.Text = folderDiag.SelectedPath;
                Refresh();
            }            
        }

        private void cmdCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFolder.Text))
            {
                MessageBox.Show("Invalid path!");
                return;
            }

            if (!Directory.Exists(txtFolder.Text))
            {
                MessageBox.Show("Directory is not exists!");
                return;
            }

            files.Clear();
            bindingSource1.ResetBindings(false);
            cmdCheck.Enabled = false;
            cmdDelete.Enabled = false;
            Refresh();
            backgroundWorker1.RunWorkerAsync(txtFolder.Text);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string path = e.Argument.ToString();
            backgroundWorker1.ReportProgress(0, "Search for files with extension other than [jpg|jpeg|png|bmp|gif]");
            GetAllFiles(path);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblStatus.Text = e.UserState.ToString();
            Refresh();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblStatus.Text = files.Count.ToString() + " file(s) found!";
            bindingSource1.ResetBindings(false);
            cmdCheck.Enabled = true;
            cmdDelete.Enabled = true;

            // copy to the text field
            txtFileToExport.Clear();
            foreach (DTO.CustomFileInfo fInfo in files)
            {
                txtFileToExport.AppendText(fInfo.FileName + Environment.NewLine);
            }

            Refresh();
        }

        private void GetAllFiles(string path)
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);
            foreach (FileInfo fInfo in dInfo.GetFiles().Where(o => !allowExtensions.Contains(o.Extension.ToLower())))
            {
                files.Add(new DTO.CustomFileInfo() { Extension = fInfo.Extension, FileName = fInfo.Name, FullPath = fInfo.FullName });
            }

            foreach (DirectoryInfo sInfo in dInfo.GetDirectories())
            {
                GetAllFiles(sInfo.FullName);
            }
        }

        private void cmdExportFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDestination.Text))
            {
                MessageBox.Show("Destination folder is not valid!");
            }
            Directory.CreateDirectory(txtDestination.Text);

            string path = txtDestination.Text;
            foreach (string fileName in txtFileToExport.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                FileInfo fInfo = new FileInfo(path + @"\" + fileName);
                Directory.CreateDirectory(fInfo.FullName.Replace(fInfo.Name, ""));

                File.Copy(txtFolder.Text + @"\" + fileName, path + @"\" + fileName);
            }
            MessageBox.Show("Done!");
            txtFileToExport.Text = string.Empty;
            Refresh();
        }
    }
}
