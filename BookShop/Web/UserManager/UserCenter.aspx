<%@ Page Title="" Language="C#" MasterPageFile="~/Master/UserMaster.Master" AutoEventWireup="true" CodeBehind="UserCenter.aspx.cs" Inherits="BookShop.Web.UserManager.UserCenter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Css/imgareaselect-default.css" rel="stylesheet" />
    <script src="/js/jquery.imgareaselect.min.js"></script>
    <link href="/SWFUpload/default.css" rel="stylesheet" />
    <script src="/SWFUpload/handlers.js" type="text/javascript"></script>
    <script src="/SWFUpload/swfupload.js" type="text/javascript"></script>
     <script type="text/javascript">
         var swfu;
         window.onload = function () {
             swfu = new SWFUpload({
                 // Backend Settings
                 upload_url: "/ashx/Upload.ashx?action=upload",
                 post_params: {
                     "ASPSESSID": "<%=Session.SessionID %>"
                },

                // File Upload Settings
                file_size_limit: "2 MB",
                file_types: "*.jpg;*.gif",
                file_types_description: "JPG Images",
                file_upload_limit: 0,    // Zero means unlimited

                // Event Handler Settings - these functions as defined in Handlers.js
                //  The handlers are not part of SWFUpload but are part of my website and control how
                //  my website reacts to the SWFUpload events.
                swfupload_preload_handler: preLoad,
                swfupload_load_failed_handler: loadFailed,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: showImage,
                upload_complete_handler: uploadComplete,

                // Button settings
                button_image_url: "/SWFUpload/images/XPButtonNoText_160x22.png",
                button_placeholder_id: "spanButtonPlaceholder",
                button_width: 160,
                button_height: 22,
                button_text: '<span class="button">请选择图片 <span class="buttonSmall">(2 MB Max)</span></span>',
                button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
                button_text_top_padding: 1,
                button_text_left_padding: 5,

                // Flash Settings
                flash_url: "/SWFUpload/swfupload.swf",	// Relative to this file
                flash9_url: "/SWFUpload/swfupload_FP9.swf",	// Relative to this file
                custom_settings: {
                    upload_target: "divFileProgressContainer"
                },

                // Debug Settings
                debug: false
            });
         }
        
        function showImage(file, serverData) {
            var data = serverData.split(':');
            if (data[0] == "ok") {
                //$("#imgDir").attr("src", serverData);
                //将上传成功的路径保存到隐藏域中.
                $("#hiddenImageUrl").val(data[1]);
                $("#selectbanner").attr("src", data[1]);
                $('#selectbanner').imgAreaSelect({
                    selectionColor: 'blue', x1: 0, y1: 0, x2: 150,

                     y2: 100, 

                    selectionOpacity: 0.2, onSelectEnd: preview
                });
                //$("#divContent").css("backgroundImage","url("+data[1]+")").css("width",data[2]+"px").css("height",data[3]+"px");
            } else {
                alert(data[1]);
            }
        }
         //选择结束以后调用该方法。
        function preview(img, selection) {

            $('#selectbanner').data('x', selection.x1);

            $('#selectbanner').data('y', selection.y1);

            $('#selectbanner').data('w', selection.width);

            $('#selectbanner').data('h', selection.height);

        }

        $(function () {
            //$("#divCut").resizable({
            //    containment: "#divContent"
            //}).draggable({ containment: "#divContent", scroll: false });
            $("#btnCut").click(function () {
                //计算范围.
                //var y = $("#divCut").offset().top - $("#divContent").offset().top;
                //var x = $("#divCut").offset().left - $("#divContent").offset().left;
                //var width = $("#divCut").width();
                //var height = $("#divCut").height();
                //var imgUrl = $("#hiddenImageUrl").val();
                //var pars = {
                //    "x":parseInt(x),
                //    "y": parseInt(y),
                //    "width":parseInt(width),
                //    "height": parseInt(height),
                //    "action":"cut",
                //    "url":imgUrl
                //};
                var imgUrl = $("#hiddenImageUrl").val();
                var pars = {
                    "x": parseInt($('#selectbanner').data('x')),
                    "y": parseInt($('#selectbanner').data('y')),
                    "width": parseInt($('#selectbanner').data('w')),
                    "height": parseInt($('#selectbanner').data('h')),
                    "action": "cut",
                    "url": imgUrl
                };
                $.post("/ashx/Upload.ashx", pars, function (data) {
                    $("#imgDir").attr("src",data);
                });

            });
        });
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <input type="hidden" id="hiddenImageUrl" />
    <div id="content">
	    <div id="swfu_container" style="margin: 0px 10px;">
		    <div>
				<span id="spanButtonPlaceholder"></span>
		    </div>
		    <div id="divFileProgressContainer" style="height: 75px;"></div>
		    <div id="thumbnails"></div>
   
	    </div>
       <%-- <div id="divContent" style="width:300px; height:300px"  >
            <div id="divCut" style="border:solid 1px blue;width:100px;height:100px"></div>
        </div>--%>
        <input type="button" value="头像截取" id="btnCut" />
        <br />
        <img id="selectbanner"/><br />
                 <img id="imgDir" />
		</div>
</asp:Content>
