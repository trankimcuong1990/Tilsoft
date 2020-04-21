using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace Library.Common
{
    public static class Helper
    {
        public static string LogFolder = "";

        public static string getNextCode(string iCode)
        {
            string lastCode = "";
            for (int i = 0; i < iCode.Length; i++)
            {
                lastCode += "Z";
            }
            if (iCode == lastCode) return string.Empty;

            string nextCode = string.Empty;
            char nextChar;
            bool isOK = false;
            for (int i = iCode.Length - 1; i >= 0; i--)
            {
                nextChar = iCode[i];
                if (isOK == false)
                {
                    nextChar = getNextChar(iCode[i]);
                    if (nextChar == '-')
                    {
                        return string.Empty;
                    }
                    else
                    {
                        if (nextChar != '0')
                        {
                            isOK = true;
                        }
                    }
                }

                nextCode = nextChar.ToString() + nextCode;
            }

            return nextCode;
        }

        public static string varDump(object obj)
        {
            return varDump(obj, 0);
        }

        public static void Log(string content)
        {
            if (!Directory.Exists(Library.Common.Helper.LogFolder))
            {
                Directory.CreateDirectory(Library.Common.Helper.LogFolder);
            }

            string logFile = Library.Common.Helper.LogFolder+@"\system_log.txt";
            StreamWriter sr = new StreamWriter(logFile, true);
            sr.WriteLine(DateTime.Now.ToString());
            sr.WriteLine(content);
            sr.WriteLine("=============================================================================");
            sr.Flush();
            sr.Close();
        }

        public static string formatIndex(string index, int indexLength, string leadingChars)
        {
            while (indexLength > index.Length)
            {
                index = leadingChars + index;
            }
            return index;
        }

        public static string textConcat(string parentString, string partString, string separator)
        {
            if (!string.IsNullOrEmpty(parentString))
            {
                return parentString + separator + partString;
            }
            return partString;
        }

        public static void SendMail(List<string> to, string subject, string content, List<string> attachment, List<string> ccField = null){
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential(FrameworkSetting.Setting.SMTPUsername, FrameworkSetting.Setting.SMTPPassword);
            MailMessage message = new MailMessage();

            // setup up the host, increase the timeout to 5 minutes
            if (!string.IsNullOrEmpty(FrameworkSetting.Setting.SMTPRelayServer))
            {
                smtpClient.Host = FrameworkSetting.Setting.SMTPRelayServer;
                smtpClient.UseDefaultCredentials = true;
            }
            else
            {
                smtpClient.Host = FrameworkSetting.Setting.SMTPServer;
                smtpClient.Port = FrameworkSetting.Setting.SMTPPort;
                smtpClient.EnableSsl = FrameworkSetting.Setting.SMTPSSL;
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;
            }
            smtpClient.Timeout = (60 * 5 * 1000);

            message.From = new MailAddress(FrameworkSetting.Setting.SMTPEmailAddress);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = content;
            foreach (string recipient in to) 
            {
                message.To.Add(recipient);
            }
            if (ccField != null)
            {
                foreach (string recipient in ccField)
                {
                    message.CC.Add(recipient);
                }
            }

            foreach(string filename in attachment)
            {
                message.Attachments.Add(new Attachment(filename));
            }            

            smtpClient.Send(message);
        }

        //
        // PRIVATE FUNCTION
        //
        private static char getNextChar(char iChar)
        {
            if (((int)iChar < 90 && (int)iChar >= 65) || ((int)iChar < 57 && (int)iChar >= 48))
            {
                return (char)((int)iChar + 1);
            }
            else if ((int)iChar == 57)
            {
                return 'A';
            }
            else if ((int)iChar == 90)
            {
                return '0';
            }

            return '-';
        }
        private static string varDump(object obj, int recursion)
        {
            StringBuilder result = new StringBuilder();

            // Protect the method against endless recursion
            if (recursion < 5)
            {
                // Determine object type
                Type t = obj.GetType();

                // Get array with properties for this object
                PropertyInfo[] properties = t.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    try
                    {
                        // Get the property value
                        object value = property.GetValue(obj, null);

                        // Create indenting string to put in front of properties of a deeper level
                        // We'll need this when we display the property name and value
                        string indent = String.Empty;
                        string spaces = "|   ";
                        string trail = "|...";

                        if (recursion > 0)
                        {
                            indent = new StringBuilder(trail).Insert(0, spaces, recursion - 1).ToString();
                        }

                        if (value != null)
                        {
                            // If the value is a string, add quotation marks
                            string displayValue = value.ToString();
                            if (value is string) displayValue = String.Concat('"', displayValue, '"');

                            // Add property name and value to return string
                            result.AppendFormat("{0}{1} = {2}\n", indent, property.Name, displayValue);

                            try
                            {
                                if (!(value is ICollection))
                                {
                                    // Call var_dump() again to list child properties
                                    // This throws an exception if the current property value
                                    // is of an unsupported type (eg. it has not properties)
                                    result.Append(varDump(value, recursion + 1));
                                }
                                else
                                {
                                    // 2009-07-29: added support for collections
                                    // The value is a collection (eg. it's an arraylist or generic list)
                                    // so loop through its elements and dump their properties
                                    int elementCount = 0;
                                    foreach (object element in ((ICollection)value))
                                    {
                                        string elementName = String.Format("{0}[{1}]", property.Name, elementCount);
                                        indent = new StringBuilder(trail).Insert(0, spaces, recursion).ToString();

                                        // Display the collection element name and type
                                        result.AppendFormat("{0}{1} = {2}\n", indent, elementName, element.ToString());

                                        // Display the child properties
                                        result.Append(varDump(element, recursion + 2));
                                        elementCount++;
                                    }

                                    result.Append(varDump(value, recursion + 1));
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            // Add empty (null) property to return string
                            result.AppendFormat("{0}{1} = {2}\n", indent, property.Name, "null");
                        }
                    }
                    catch
                    {
                        // Some properties will throw an exception on property.GetValue()
                        // I don't know exactly why this happens, so for now i will ignore them...
                    }
                }
            }

            return result.ToString();
        }

        public static string getAbsolutePathFromFile(string fileUrl)
        {
            return fileUrl.Replace("//", "/").Replace("http:/", "http://").Replace(FrameworkSetting.Setting.FullSizeUrl, FrameworkSetting.Setting.AbsoluteFileFolder).Replace("/", @"\");
        }

        public static DTO.FirstLastNameDTO ConvertFullnameToFirstLastName(string fullname)
        {
            if (!fullname.Contains(" "))
            {
                return null;
            }
            DTO.FirstLastNameDTO data = new DTO.FirstLastNameDTO();
            data.LastName = fullname.Split(' ')[fullname.Split(' ').Length - 1];
            data.FirstName = fullname.Replace(" " + data.LastName, "");

            return data;
        }
    }
}
