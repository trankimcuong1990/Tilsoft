﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUploader.aspx.cs" Inherits="TilsoftService.FileUploader" %>
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
    <title>File Manager</title>
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
    <div style="margin-bottom: 10px; border: solid 1px #ddd; box-sizing: border-box; padding: 10px; height: 200px; overflow-y: scroll;">
        THE TARGET FOLDER: <%= Server.MapPath("~/Media/Temp/" + userTempFolder) %>
    </div>
    <div id="container">
        <button id="browse">Select File</button>
        <button id="start-upload">Upload</button>
        
        <button id="clear" style="float: right;">Clear</button>
        <button id="refresh" style="float: right; margin-right: 5px;">Refresh</button>
        <div style="clear: both;"></div>
    </div>
    <div style="height: 200px; overflow-y: scroll;">
        <ul id="filelist"></ul>
    </div>    
    <br />
    <pre id="console"></pre>
 
    <script type="text/javascript">
        var uploader = new plupload.Uploader({
            browse_button: 'browse', // this can be an id of a DOM element or the DOM element itself
            resize: {
                width: 1027,
                height: 768,
                preserve_headers: false
            },
            url: 'UploadHandler.aspx?t=<%=userTempFolder%>',
            max_retries: 3,
            chunk_size: '500kb',
            unique_names: true,
            filters: {
                max_file_size: '800mb',
                mime_types: [
                    { title: "Image files", extensions: "jpg,gif,png,bmp,jpeg" },
                    { title: "Zip files", extensions: "zip,rar" },
                    { title: "Other files", extensions: "pdf,psd,doc,docx,xlsx,xls,dwg,mp4,xml" }
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
