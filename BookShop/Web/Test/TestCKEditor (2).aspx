﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestCKEditor.aspx.cs" Inherits="BookShop.Web.Test.TestCKEditor" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../ckeditor/ckeditor.js"></script>
    <script src="../ckeditor/_samples/sample.js"></script>
    <link href="../ckeditor/_samples/sample.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      
    <textarea cols="100" id="editor1" name="editor1" rows="10"></textarea>
          
		<script type="text/javascript">
		    //<![CDATA[
		    // Replace the <textarea id="editor1"> with an CKEditor instance.

		    var editor = CKEDITOR.replace('editor1');
		    //]]>
		</script>
    </div>
    </form>
</body>
</html>