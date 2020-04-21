namespace MagentoAttributeToExcel
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
            this.cmdConvert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTextToBeConverted = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdConvert
            // 
            this.cmdConvert.Location = new System.Drawing.Point(12, 370);
            this.cmdConvert.Name = "cmdConvert";
            this.cmdConvert.Size = new System.Drawing.Size(117, 37);
            this.cmdConvert.TabIndex = 1;
            this.cmdConvert.Text = "Convert";
            this.cmdConvert.UseVisualStyleBackColor = true;
            this.cmdConvert.Click += new System.EventHandler(this.cmdConvert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Text to be converted";
            // 
            // txtTextToBeConverted
            // 
            this.txtTextToBeConverted.Location = new System.Drawing.Point(12, 25);
            this.txtTextToBeConverted.MaxLength = 2147483647;
            this.txtTextToBeConverted.Multiline = true;
            this.txtTextToBeConverted.Name = "txtTextToBeConverted";
            this.txtTextToBeConverted.Size = new System.Drawing.Size(786, 339);
            this.txtTextToBeConverted.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 419);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdConvert);
            this.Controls.Add(this.txtTextToBeConverted);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdConvert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTextToBeConverted;
    }
}

