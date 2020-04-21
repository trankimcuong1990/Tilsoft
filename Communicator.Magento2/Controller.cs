using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Communicator.Magento2
{
    public class Controller : Communicator.Base.Communicator
    {
        public Controller(string url, string token)
        {
            this.apiToken = token;
            this.apiURL = url;
        }

        public DTO.EurofarstockAccount CreateCustomer(DTO.POST.Customer data, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + this.apiToken);
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.UploadString(this.apiURL + "V1/customers", "POST", Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    return new DTO.EurofarstockAccount() { Username = data.customer.email, Password = data.password };
                }
            }
            catch (WebException exception)
            {
                var responseStream = exception.Response?.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        errorMsg = reader.ReadToEnd();
                    }
                }
                if (string.IsNullOrEmpty(errorMsg))
                {
                    errorMsg = exception.Message;
                }
                return null;
            }
            catch (Exception ex)
            {
                errorMsg = Library.Helper.GetInnerException(ex).Message;
                return null;
            }
        }
    }
}
