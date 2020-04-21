<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiUploadFile.aspx.cs" Inherits="TilsoftService.MultiUploadFile" %>
<%
    string userTempFolder = string.Empty;
    if (Request.Params["t"] != null)
    {
        userTempFolder = Request.Params["t"].ToString();
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
    </div>
    <div style="min-height: 200px;">
        <ul id="filelist"></ul>
    </div>    
    <br />
    <pre id="console"></pre>
 
    <script type="text/javascript">
        var uploader = new plupload.Uploader({
            browse_button: 'browse', // this can be an id of a DOM element or the DOM element itself
            url: 'UploadHandler.aspx?t=<%=userTempFolder%>',
            chunk_size: '200kb',
            max_retries: 3,
            chunk_size: '500kb',
            unique_names: true,
            filters: {
                max_file_size: '800mb',
                mime_types: [
                    { title: "Image files", extensions: "jpg,gif,png,bmp" },
                    { title: "Zip files", extensions: "zip,rar" },
                    { title: "Other files", extensions: "pdf,psd,doc,docx,xlsx,xls,dwg,mp4,pptx,pptm,ppt" }
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
