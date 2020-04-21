<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterUploader.aspx.cs" Inherits="TilsoftService.MasterUploader" %>
<%
    string userTempFolder = string.Empty;
    string imageOnly = "0";
    string multiSelect = "0";
    if (Request.Params["t"] != null)
    {
        userTempFolder = Request.Params["t"].ToString();
    }
    if (Request.Params["i"] != null)
    {
        imageOnly = Request.Params["i"].ToString();
    }
    if (Request.Params["m"] != null)
    {
        multiSelect = Request.Params["m"].ToString();
    }
%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload multiple files</title>
    <script type="text/javascript" src="js/plupload.full.min.js"></script>
    <style>
        #filelist {
            padding: 0px;
            margin: 10px;
        }
            #filelist li {
                list-style-position: inside;
            }
    </style>
</head>
<body style="margin: 0px; padding: 0px;">
    <div id="container">
        <button id="browse">Select File</button>
        <button id="start-upload">Upload</button>
        <label><input id="chkResize" type="checkbox" onclick="uploaderConfig()" /> Resize image to 1027x768</label>
    </div>
    <div style="min-height: 200px;">
        <ul id="filelist"></ul>
    </div>    
    <br />
    <pre id="console"></pre>
 
    <script type="text/javascript">
        function uploaderConfig() {
            if (document.getElementById('chkResize').checked) {
                uploader.settings.resize = {
                    width: 1027,
                    height: 768,
                    preserve_headers: false
                };
            }
            else {
                uploader.settings.resize = false;
            }
        }

        var uploader = new plupload.Uploader({
            browse_button: 'browse', // this can be an id of a DOM element or the DOM element itself
            url: 'MasterUploadHandler.aspx?t=<%=userTempFolder%>',
            max_retries: 3,
            chunk_size: '500kb',
            unique_names: true,
            multi_selection: <%= multiSelect=="1" ? "true" : "false"%>,
            filters: {
                max_file_size: '800mb',
                mime_types: [
                    { title: "Image files", extensions: "jpg,gif,png,bmp,jpeg" },
        <%
        if (imageOnly == "0")
        {
        %>
                    { title: "Zip files", extensions: "zip,rar" },
                    { title: "Other files", extensions: "pdf,psd,doc,docx,xlsx,xls,dwg,mp4,pptx,pptm,ppt" }
        <%
        }
        %>
                ]
            }
        });

        uploader.init();
        uploader.bind('FilesAdded', function (up, files) {
            var html = '';
            plupload.each(files, function (file) {
                html += '<li id="' + file.id + '">' + file.name + ' (' + plupload.formatSize(file.size) + ') <b></b></li>';
            });
            document.getElementById('filelist').innerHTML += html;
        });
        uploader.bind('UploadProgress', function (up, file) {
            document.getElementById(file.id).getElementsByTagName('b')[0].innerHTML = '<span>' + file.percent + "%</span>";
        });
        uploader.bind('Error', function (up, err) {
            document.getElementById('console').innerHTML += "\nError #" + err.code + ": " + err.message;
        });
        uploader.bind('UploadComplete', function (up, files) {
            var result = {
                isError: false,
                message: '',
                info: []
            };
            // clear file upload ui
            while (files.length > 0) {
                result.info.push({
                    filename: files[0].target_name,
                    friendlyName: files[0].name,
                    fileURL: '<%=System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] %>/Media/Temp/<%=userTempFolder%>/' + files[0].target_name
                });

                files.splice(0, 1);
            }
            document.getElementById('filelist').innerHTML = '';
            window.top.postMessage(result, '*');
        });

        document.getElementById('start-upload').onclick = function () {
            uploader.start();
        };
    </script>
</body>
</html>