<%@ Page Title="Upload" Language="C#" %>
<%@ Import Namespace="System.IO" %>
<script runat="server" type="text/c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check to see whether there are uploaded files to process them
        string userTempFolder = string.Empty;
        if (Request.Params["t"] != null)
        {
            userTempFolder = Request.Params["t"].ToString();
        }

        if (Request.Files.Count > 0 && !string.IsNullOrEmpty(userTempFolder))
        {
            int chunk = Request["chunk"] != null ? int.Parse(Request["chunk"]) : 0;
            string fileName = Request["name"] != null ? Request["name"] : string.Empty;

            HttpPostedFile fileUpload = Request.Files[0];

            var uploadPath = Server.MapPath("~/Media/Temp/" + userTempFolder);
            using (var fs = new FileStream(Path.Combine(uploadPath, fileName), chunk == 0 ? FileMode.Create : FileMode.Append))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);

                fs.Write(buffer, 0, buffer.Length);
            }
        }
    }
</script>
<%
    string userTempFolder = string.Empty;
    if (Request.Params["t"] != null)
    {
        userTempFolder = Request.Params["t"].ToString();
    }
%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" dir="ltr">
<head>
<meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
<!-- production -->
<script type="text/javascript" src="js/plupload.full.min.js"></script>
</head>
<body style="margin: 0px;">
<div id="container">
    <button id="pickfiles">Select File</button>
    <button id="uploadfiles">Upload</button>
</div>
<div id="filelist"></div>
<script type="text/javascript">
    // Custom example logic
    var uploader = new plupload.Uploader({
        runtimes: 'html5,flash,silverlight,html4',
        browse_button: 'pickfiles', // you can pass an id...
        container: document.getElementById('container'), // ... or DOM Element itself
        url: 'Upload.aspx?t=<%=userTempFolder%>',
        flash_swf_url: 'js/Moxie.swf',
        silverlight_xap_url: 'js/Moxie.xap',
        chunk_size: '500kb',
        max_retries: 3,
        unique_names: true,
        multi_selection:false,

        filters: {
            max_file_size: '800mb',
            mime_types: [
                { title: "Image files", extensions: "jpg,gif,png,bmp,jpeg" },
                { title: "Zip files", extensions: "zip,rar" },
                { title: "Other files", extensions: "pdf,psd,doc,docx,xlsx,xls,dwg" }
            ]
        },

        init: {
            PostInit: function () {
                document.getElementById('filelist').innerHTML = '(No file selected)';
                document.getElementById('uploadfiles').onclick = function () {
                    uploader.start();
                    return false;
                };
            },

            FilesAdded: function (up, files) {
                plupload.each(files, function (file) {
                    document.getElementById('filelist').innerHTML = '<div id="' + file.id + '">' + file.name + ' (' + plupload.formatSize(file.size) + ') <b></b></div>';
                });
            },

            UploadProgress: function (up, file) {
                document.getElementById(file.id).getElementsByTagName('b')[0].innerHTML = '<span>' + file.percent + "%</span>";
            },

            Error: function (up, err) {
                var result = {
                    isError: true,
                    message: err.message,
                    filename: ''
                };
                window.top.postMessage(result, '*');
            },

            FileUploaded: function (up, file, info) {
                var result = {
                    isError: false,
                    message: '',
                    filename: file.target_name,
                    friendlyName: file.name,
                    fileURL: '<%=System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] %>/Media/Temp/<%=userTempFolder%>/' + file.target_name
                };
                window.top.postMessage(result, '*');
            }
        }
    });

    uploader.init();

</script>    
</body>
</html>
