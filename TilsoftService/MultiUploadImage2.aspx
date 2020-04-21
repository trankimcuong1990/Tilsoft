<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiUploadImage2.aspx.cs" Inherits="TilsoftService.MultiUploadImage2" %>
<%
    string userTempFolder = string.Empty;
    string autoResize = "1";
    if (Request.Params["t"] != null)
    {
        userTempFolder = Request.Params["t"].ToString();
    }
    if (Request.Params["a"] != null)
    {
        autoResize = Request.Params["a"].ToString();
    }
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload multiple images <% if (Request.Params["a"].ToString() == "1") { %>(images will be resized) <% } %></title>
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
            <% 
        if (autoResize == "1")
        {
            %>
            resize: {
                    width: 1027,
                    height: 768,
                    preserve_headers: false
            },
            <%
        }
            %>
            url: 'UploadHandler2.aspx?t=<%=userTempFolder%>',
            chunk_size: '200kb',
            max_retries: 3,
            chunk_size: '500kb',
            unique_names: false,
            filters: {
                max_file_size: '800mb',
                mime_types: [
                    { title: "Image files", extensions: "jpg,gif,png,bmp,jpeg" }
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
            document.getElementById('filelist').innerHTML = '';
            window.top.postMessage(result, '*');
        });

        document.getElementById('start-upload').onclick = function () {
            uploader.start();
        };
    </script>
</body>
</html>
