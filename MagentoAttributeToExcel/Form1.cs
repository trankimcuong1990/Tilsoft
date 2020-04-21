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
using System.Collections;

namespace MagentoAttributeToExcel
{
    public partial class Form1 : Form
    {
        private string separator = ",";

        public Form1()
        {
            InitializeComponent();
        }

        private void cmdConvert_Click(object sender, EventArgs e)
        {
            string uniqueFileName = Path.GetTempPath() + System.Guid.NewGuid().ToString().Replace("-", "") + ".csv";
            //System.IO.FileInfo fInfo = new System.IO.FileInfo(uniqueFileName);
            //OfficeOpenXml.ExcelPackage ePackagae = new OfficeOpenXml.ExcelPackage(fInfo);
            //OfficeOpenXml.ExcelWorksheet atts = ePackagae.Workbook.Worksheets.Add("Attributes");

            ////atts.Cells["A1"].LoadFromCollection(products.ToList(), true);
            //ePackagae.Save();

            List<string> attributes = new List<string>();
            string[] attribute;
            Hashtable rowData = new Hashtable();

            // get column name
            foreach (string attrLine in txtTextToBeConverted.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                foreach (string attr in attrLine.Split(new[] { @"""," }, StringSplitOptions.None))
                {
                    attribute = attr.Split(new[] { "=" }, StringSplitOptions.None);
                    if (!attributes.Contains(attribute[0]))
                    {
                        attributes.Add(attribute[0]);
                    }
                }
            }

            // export to csv
            StringBuilder sb = new StringBuilder();

            string lineContent = string.Empty;
            foreach (string header in attributes)
            {
                if (string.IsNullOrEmpty(lineContent))
                {
                    lineContent += header;
                }
                else
                {
                    lineContent += "," + header;
                }
            }
            sb.AppendLine(lineContent);

            foreach (string attrLine in txtTextToBeConverted.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                rowData = new Hashtable();
                lineContent = string.Empty;
                foreach (string attr in attrLine.Split(new[] { @"""," }, StringSplitOptions.None))
                {
                    attribute = attr.Replace(@"""", "").Split(new[] { "=" }, StringSplitOptions.None);
                    rowData[attribute[0]] = attribute[1];
                }
                foreach (string header in attributes)
                {
                    if (rowData.ContainsKey(header))
                    {
                        if (string.IsNullOrEmpty(lineContent))
                        {
                            lineContent += @"""" + rowData[header] + @"""";
                        }
                        else
                        {
                            lineContent += @",""" + rowData[header] + @"""";
                        }
                    }
                    else
                    {
                        lineContent += ",";
                        //if (string.IsNullOrEmpty(lineContent))
                        //{
                        //    lineContent += ",";
                        //}
                        //else
                        //{
                        //    lineContent += ",,";
                        //}
                    }
                }
                sb.AppendLine(lineContent);
            }
            StreamWriter sw = new StreamWriter(uniqueFileName);
            sw.Write(sb.ToString());
            sw.Flush();
            sw.Close();
            System.Diagnostics.Process.Start(uniqueFileName);
        }
    }
}
