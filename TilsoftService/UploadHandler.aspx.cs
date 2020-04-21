using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TilsoftService
{
    public partial class UploadHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userTempFolder = string.Empty;
            if (Request.Params["t"] != null)
            {
                userTempFolder = Request.Params["t"].ToString();
                if (!Directory.Exists(Server.MapPath("~/Media/Temp/" + userTempFolder + "/tmp/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Media/Temp/" + userTempFolder + "/tmp/"));
                }
            }

            if (Request.Files.Count > 0 && !string.IsNullOrEmpty(userTempFolder))
            {
                int chunk = Request["chunk"] != null ? int.Parse(Request["chunk"]) : 0;
                int chunks = Request["chunks"] != null ? int.Parse(Request["chunks"]) : 0;
                string fileName = Request["name"] != null ? Request["name"] : string.Empty;

                HttpPostedFile fileUpload = Request.Files[0];

                var uploadPath = Server.MapPath("~/Media/Temp/" + userTempFolder + "/tmp/");
                using (var fs = new FileStream(Path.Combine(uploadPath, fileName), chunk == 0 ? FileMode.Create : FileMode.Append))
                {
                    var buffer = new byte[fileUpload.InputStream.Length];
                    fileUpload.InputStream.Read(buffer, 0, buffer.Length);

                    fs.Write(buffer, 0, buffer.Length);
                }

                if (chunks == 1 || chunk == chunks - 1) // upload complete
                {
                    string uniqueFileName = Library.Helper.getUniqueFileName(Server.MapPath("~/Media/Temp/" + userTempFolder) + @"\", fileName);

                    // move to user folder and rename accordingly
                    File.Move(Server.MapPath("~/Media/Temp/" + userTempFolder + "/tmp/" + fileName), Server.MapPath("~/Media/Temp/" + userTempFolder + "/" + uniqueFileName));

                    // create thumbnail if needed
                    FileInfo fInfo = new FileInfo(uniqueFileName);
                    string[] imageExtension = { ".jpg", ".jpeg", ".gif", ".bmp", ".png" };
                    if (imageExtension.Contains(fInfo.Extension.ToLower()))
                    {
                        Library.FileHelper.ImageManager imgFactory = new Library.FileHelper.ImageManager("", "");
                        imgFactory.CreateThumbnail2(Server.MapPath("~/Media/Temp/" + userTempFolder + "/thumbnail/") + @"\", Server.MapPath("~/Media/Temp/" + userTempFolder + "/" + uniqueFileName));
                    }
                }
            }
        }
    }
}