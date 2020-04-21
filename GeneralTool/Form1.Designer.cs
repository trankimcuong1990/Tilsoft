namespace GeneralTool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabFileInFoder = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLog_tabFileInFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtResult_tabFileInFolder = new System.Windows.Forms.TextBox();
            this.cmdParse_tabFileInFoder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolder_tabFileInFoder = new System.Windows.Forms.TextBox();
            this.tabCopyFileFromList = new System.Windows.Forms.TabPage();
            this.txtFileList_tabCopyFileFromList = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDestination_tabCopyFileFromList = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdCopy_tabCopyFileFromList = new System.Windows.Forms.Button();
            this.txtSource_tabCopyFileFromList = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabTILSOFTFile = new System.Windows.Forms.TabPage();
            this.cmdRemove_tabTILSOFTFile = new System.Windows.Forms.Button();
            this.cmdImport_tabTILSOFTFile = new System.Windows.Forms.Button();
            this.txtFileList_tabTILSOFTFile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSource_tabTILSOFTFile = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtFolderToImport = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmdImportToFilesTable = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabFileInFoder.SuspendLayout();
            this.tabCopyFileFromList.SuspendLayout();
            this.tabTILSOFTFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabFileInFoder);
            this.tabControl1.Controls.Add(this.tabCopyFileFromList);
            this.tabControl1.Controls.Add(this.tabTILSOFTFile);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(753, 438);
            this.tabControl1.TabIndex = 0;
            // 
            // tabFileInFoder
            // 
            this.tabFileInFoder.Controls.Add(this.label3);
            this.tabFileInFoder.Controls.Add(this.txtLog_tabFileInFolder);
            this.tabFileInFoder.Controls.Add(this.label2);
            this.tabFileInFoder.Controls.Add(this.txtResult_tabFileInFolder);
            this.tabFileInFoder.Controls.Add(this.cmdParse_tabFileInFoder);
            this.tabFileInFoder.Controls.Add(this.label1);
            this.tabFileInFoder.Controls.Add(this.txtFolder_tabFileInFoder);
            this.tabFileInFoder.Location = new System.Drawing.Point(4, 22);
            this.tabFileInFoder.Name = "tabFileInFoder";
            this.tabFileInFoder.Padding = new System.Windows.Forms.Padding(3);
            this.tabFileInFoder.Size = new System.Drawing.Size(745, 412);
            this.tabFileInFoder.TabIndex = 0;
            this.tabFileInFoder.Text = "File in Folder";
            this.tabFileInFoder.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(557, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Log";
            // 
            // txtLog_tabFileInFolder
            // 
            this.txtLog_tabFileInFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog_tabFileInFolder.Location = new System.Drawing.Point(560, 43);
            this.txtLog_tabFileInFolder.Multiline = true;
            this.txtLog_tabFileInFolder.Name = "txtLog_tabFileInFolder";
            this.txtLog_tabFileInFolder.Size = new System.Drawing.Size(163, 349);
            this.txtLog_tabFileInFolder.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Result";
            // 
            // txtResult_tabFileInFolder
            // 
            this.txtResult_tabFileInFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResult_tabFileInFolder.Location = new System.Drawing.Point(62, 43);
            this.txtResult_tabFileInFolder.Multiline = true;
            this.txtResult_tabFileInFolder.Name = "txtResult_tabFileInFolder";
            this.txtResult_tabFileInFolder.Size = new System.Drawing.Size(483, 349);
            this.txtResult_tabFileInFolder.TabIndex = 2;
            // 
            // cmdParse_tabFileInFoder
            // 
            this.cmdParse_tabFileInFoder.Location = new System.Drawing.Point(470, 15);
            this.cmdParse_tabFileInFoder.Name = "cmdParse_tabFileInFoder";
            this.cmdParse_tabFileInFoder.Size = new System.Drawing.Size(75, 23);
            this.cmdParse_tabFileInFoder.TabIndex = 1;
            this.cmdParse_tabFileInFoder.Text = "Parse";
            this.cmdParse_tabFileInFoder.UseVisualStyleBackColor = true;
            this.cmdParse_tabFileInFoder.Click += new System.EventHandler(this.cmdParse_tabFileInFoder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Folder";
            // 
            // txtFolder_tabFileInFoder
            // 
            this.txtFolder_tabFileInFoder.Location = new System.Drawing.Point(62, 17);
            this.txtFolder_tabFileInFoder.Name = "txtFolder_tabFileInFoder";
            this.txtFolder_tabFileInFoder.Size = new System.Drawing.Size(402, 20);
            this.txtFolder_tabFileInFoder.TabIndex = 0;
            // 
            // tabCopyFileFromList
            // 
            this.tabCopyFileFromList.Controls.Add(this.txtFileList_tabCopyFileFromList);
            this.tabCopyFileFromList.Controls.Add(this.label6);
            this.tabCopyFileFromList.Controls.Add(this.txtDestination_tabCopyFileFromList);
            this.tabCopyFileFromList.Controls.Add(this.label5);
            this.tabCopyFileFromList.Controls.Add(this.cmdCopy_tabCopyFileFromList);
            this.tabCopyFileFromList.Controls.Add(this.txtSource_tabCopyFileFromList);
            this.tabCopyFileFromList.Controls.Add(this.label4);
            this.tabCopyFileFromList.Location = new System.Drawing.Point(4, 22);
            this.tabCopyFileFromList.Name = "tabCopyFileFromList";
            this.tabCopyFileFromList.Padding = new System.Windows.Forms.Padding(3);
            this.tabCopyFileFromList.Size = new System.Drawing.Size(745, 412);
            this.tabCopyFileFromList.TabIndex = 1;
            this.tabCopyFileFromList.Text = "Copy file from list";
            this.tabCopyFileFromList.UseVisualStyleBackColor = true;
            // 
            // txtFileList_tabCopyFileFromList
            // 
            this.txtFileList_tabCopyFileFromList.Location = new System.Drawing.Point(86, 69);
            this.txtFileList_tabCopyFileFromList.MaxLength = 9999999;
            this.txtFileList_tabCopyFileFromList.Multiline = true;
            this.txtFileList_tabCopyFileFromList.Name = "txtFileList_tabCopyFileFromList";
            this.txtFileList_tabCopyFileFromList.Size = new System.Drawing.Size(494, 316);
            this.txtFileList_tabCopyFileFromList.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "File List";
            // 
            // txtDestination_tabCopyFileFromList
            // 
            this.txtDestination_tabCopyFileFromList.Location = new System.Drawing.Point(86, 43);
            this.txtDestination_tabCopyFileFromList.Name = "txtDestination_tabCopyFileFromList";
            this.txtDestination_tabCopyFileFromList.Size = new System.Drawing.Size(355, 20);
            this.txtDestination_tabCopyFileFromList.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Destination";
            // 
            // cmdCopy_tabCopyFileFromList
            // 
            this.cmdCopy_tabCopyFileFromList.Location = new System.Drawing.Point(447, 17);
            this.cmdCopy_tabCopyFileFromList.Name = "cmdCopy_tabCopyFileFromList";
            this.cmdCopy_tabCopyFileFromList.Size = new System.Drawing.Size(133, 46);
            this.cmdCopy_tabCopyFileFromList.TabIndex = 3;
            this.cmdCopy_tabCopyFileFromList.Text = "Copy";
            this.cmdCopy_tabCopyFileFromList.UseVisualStyleBackColor = true;
            this.cmdCopy_tabCopyFileFromList.Click += new System.EventHandler(this.cmdCopy_tabCopyFileFromList_Click);
            // 
            // txtSource_tabCopyFileFromList
            // 
            this.txtSource_tabCopyFileFromList.Location = new System.Drawing.Point(86, 17);
            this.txtSource_tabCopyFileFromList.Name = "txtSource_tabCopyFileFromList";
            this.txtSource_tabCopyFileFromList.Size = new System.Drawing.Size(355, 20);
            this.txtSource_tabCopyFileFromList.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Source";
            // 
            // tabTILSOFTFile
            // 
            this.tabTILSOFTFile.Controls.Add(this.cmdImportToFilesTable);
            this.tabTILSOFTFile.Controls.Add(this.txtFolderToImport);
            this.tabTILSOFTFile.Controls.Add(this.label9);
            this.tabTILSOFTFile.Controls.Add(this.cmdRemove_tabTILSOFTFile);
            this.tabTILSOFTFile.Controls.Add(this.cmdImport_tabTILSOFTFile);
            this.tabTILSOFTFile.Controls.Add(this.txtFileList_tabTILSOFTFile);
            this.tabTILSOFTFile.Controls.Add(this.label7);
            this.tabTILSOFTFile.Controls.Add(this.txtSource_tabTILSOFTFile);
            this.tabTILSOFTFile.Controls.Add(this.label8);
            this.tabTILSOFTFile.Location = new System.Drawing.Point(4, 22);
            this.tabTILSOFTFile.Name = "tabTILSOFTFile";
            this.tabTILSOFTFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabTILSOFTFile.Size = new System.Drawing.Size(745, 412);
            this.tabTILSOFTFile.TabIndex = 2;
            this.tabTILSOFTFile.Text = "TILSOFT File";
            this.tabTILSOFTFile.UseVisualStyleBackColor = true;
            // 
            // cmdRemove_tabTILSOFTFile
            // 
            this.cmdRemove_tabTILSOFTFile.Location = new System.Drawing.Point(428, 95);
            this.cmdRemove_tabTILSOFTFile.Name = "cmdRemove_tabTILSOFTFile";
            this.cmdRemove_tabTILSOFTFile.Size = new System.Drawing.Size(133, 46);
            this.cmdRemove_tabTILSOFTFile.TabIndex = 11;
            this.cmdRemove_tabTILSOFTFile.Text = "Remove";
            this.cmdRemove_tabTILSOFTFile.UseVisualStyleBackColor = true;
            this.cmdRemove_tabTILSOFTFile.Click += new System.EventHandler(this.cmdRemove_tabTILSOFTFile_Click);
            // 
            // cmdImport_tabTILSOFTFile
            // 
            this.cmdImport_tabTILSOFTFile.Location = new System.Drawing.Point(428, 43);
            this.cmdImport_tabTILSOFTFile.Name = "cmdImport_tabTILSOFTFile";
            this.cmdImport_tabTILSOFTFile.Size = new System.Drawing.Size(133, 46);
            this.cmdImport_tabTILSOFTFile.TabIndex = 10;
            this.cmdImport_tabTILSOFTFile.Text = "Import";
            this.cmdImport_tabTILSOFTFile.UseVisualStyleBackColor = true;
            this.cmdImport_tabTILSOFTFile.Click += new System.EventHandler(this.cmdImport_tabTILSOFTFile_Click);
            // 
            // txtFileList_tabTILSOFTFile
            // 
            this.txtFileList_tabTILSOFTFile.Location = new System.Drawing.Point(67, 43);
            this.txtFileList_tabTILSOFTFile.MaxLength = 9999999;
            this.txtFileList_tabTILSOFTFile.Multiline = true;
            this.txtFileList_tabTILSOFTFile.Name = "txtFileList_tabTILSOFTFile";
            this.txtFileList_tabTILSOFTFile.Size = new System.Drawing.Size(355, 98);
            this.txtFileList_tabTILSOFTFile.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "File List";
            // 
            // txtSource_tabTILSOFTFile
            // 
            this.txtSource_tabTILSOFTFile.Location = new System.Drawing.Point(67, 17);
            this.txtSource_tabTILSOFTFile.Name = "txtSource_tabTILSOFTFile";
            this.txtSource_tabTILSOFTFile.Size = new System.Drawing.Size(355, 20);
            this.txtSource_tabTILSOFTFile.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Source";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(745, 412);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtFolderToImport
            // 
            this.txtFolderToImport.Location = new System.Drawing.Point(67, 179);
            this.txtFolderToImport.Name = "txtFolderToImport";
            this.txtFolderToImport.Size = new System.Drawing.Size(355, 20);
            this.txtFolderToImport.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 182);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Folder";
            // 
            // cmdImportToFilesTable
            // 
            this.cmdImportToFilesTable.Location = new System.Drawing.Point(428, 179);
            this.cmdImportToFilesTable.Name = "cmdImportToFilesTable";
            this.cmdImportToFilesTable.Size = new System.Drawing.Size(133, 32);
            this.cmdImportToFilesTable.TabIndex = 14;
            this.cmdImportToFilesTable.Text = "Import to Files table";
            this.cmdImportToFilesTable.UseVisualStyleBackColor = true;
            this.cmdImportToFilesTable.Click += new System.EventHandler(this.cmdImportToFilesTable_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 462);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl1.ResumeLayout(false);
            this.tabFileInFoder.ResumeLayout(false);
            this.tabFileInFoder.PerformLayout();
            this.tabCopyFileFromList.ResumeLayout(false);
            this.tabCopyFileFromList.PerformLayout();
            this.tabTILSOFTFile.ResumeLayout(false);
            this.tabTILSOFTFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabFileInFoder;
        private System.Windows.Forms.TabPage tabCopyFileFromList;
        private System.Windows.Forms.Button cmdParse_tabFileInFoder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFolder_tabFileInFoder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtResult_tabFileInFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLog_tabFileInFolder;
        private System.Windows.Forms.Button cmdCopy_tabCopyFileFromList;
        private System.Windows.Forms.TextBox txtSource_tabCopyFileFromList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFileList_tabCopyFileFromList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDestination_tabCopyFileFromList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabTILSOFTFile;
        private System.Windows.Forms.Button cmdImport_tabTILSOFTFile;
        private System.Windows.Forms.TextBox txtFileList_tabTILSOFTFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSource_tabTILSOFTFile;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button cmdRemove_tabTILSOFTFile;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button cmdImportToFilesTable;
        private System.Windows.Forms.TextBox txtFolderToImport;
        private System.Windows.Forms.Label label9;
    }
}

