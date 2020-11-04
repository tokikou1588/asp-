<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BookShop.Web.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <style type="text/css">
        .txtInput {
        	height:26px;
	     line-height:26px;
         border:1px #ccc solid;
         margin-bottom:5px;
         width:200px;
        }
        .regnow {
	width:300px;
	margin-left:30px;
	height:40px;
	background:#db2f2f;
	border:none;
	color: #FFF;
	font-size: 15px;
	font-weight: 700;
    cursor:pointer;
} 
    </style>
    <script type="text/javascript">
        $(function () {
            $("#txtUName").blur(function () {
                validateUserName($(this));
            });
            $("#txtUEmail").blur(function () {
                validateUserMail($(this));
            });
            $("#vCode").click(function () {
                $("#imgCode").attr("src", $("#imgCode").attr("src")+1);
            });
            $("#txtCode").blur(function () {
                validateUserCode($(this));
            });
            $("#btnReg").click(function () {
                validateUserReg();
            });
        });
        //检查用户名
        function validateUserName(control) {
            
            var userName = control.val();
            if (userName != "") {
                var reg = /^[a-zA-Z0-9_\u4e00-\u9fa5]{4,20}$/;
                if (reg.test(userName)) {
                    $.post("/ashx/ValidateUserName.ashx", { "userName": userName }, function (data) {
                        if (data == "ok") {
                            $("#userErrorMsg").text("用户名被占用!!");
                        } else {
                            $("#userErrorMsg").text("用户名可用!!");
                        }
                    });
                } else {
                    $("#userErrorMsg").text("只能是数字,字母,中文组合!!");
                }

            } else {
                $("#userErrorMsg").text("用户名不能为空");
            }
        }
        //检查用户邮箱
        function validateUserMail(control) {
            var userMail = control.val();
            if (userMail != "") {
                var reg = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
                if (reg.test(userMail)) {
                    $.post("/ashx/ValidateUserMail.ashx", { "userMail": userMail }, function (data) {
                        if (data == "ok") {
                            $("#mailErrorMsg").text("邮箱占用!!");
                        } else {
                            $("#mailErrorMsg").text("邮箱可用!!");
                        }
                    });
                } else {
                    $("#mailErrorMsg").text("邮箱格式错误!!");
                }
            } else {
                $("#mailErrorMsg").text("邮箱不能为空!!");
            }
        }
        //校验用户验证码
        function validateUserCode(control) {
            var vcode = control.val();
            if (vcode != "") {
                var reg = /^[0-9]*$/;
                if (reg.test(vcode)) {
                    $.post("/ashx/ValidateUserCode.ashx", { "vcode": vcode }, function (data) {
                        if (data == "ok") {
                            $("#vcodeMsg").text("验证码正确!!");
                        } else {
                            $("#vcodeMsg").text("验证码错误!!");
                        }
                    });
                } else {
                    $("#vcodeMsg").text("验证码格式错误!!");
                }
            } else {
                $("#vcodeMsg").text("验证码不能为空!!");
            }
        }
        //注册
        function validateUserReg() {
            if ($("#txtUName").val() == "") { $("#userErrorMsg").text("用户名不能为空"); return; }
            if ($("#txtUEmail").val() == "") { $("#mailErrorMsg").text("邮箱不能为空"); return; }
            if ($("#txtCode").val() == "") { $("#vcodeMsg").text("验证码不能为空!!"); return; };
            var pars = $("#aspnetForm").serializeArray();
            $.post("/ashx/UserReg.ashx", pars, function (data) {
                var serverData = data.split(':');
                if (serverData[0] == "ok") {
                    var url = $("#hiddenReturnUrl").val();
                    if (url == "") {
                        window.location.href = "/Default.aspx";
                    } else {
                        window.location.href = url;
                    }
                } else {
                    $("#regMsg").text(serverData[1]);
                }
            });
        }
      
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="hidden" id="hiddenReturnUrl" value="<%=ReturnUrl %>" />
 <div style="font-size:small">
  <table width="80%" height="22" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td style="width: 10px"><img src="../Images/az-tan-top-left-round-corner.gif" width="10" height="28" /></td>
    <td bgcolor="#DDDDCC"><span class="STYLE1">注册新用户</span></td>
    <td width="10"><img src="../Images/az-tan-top-right-round-corner.gif" width="10" height="28" /></td>
  </tr>
</table>

    
<table width="80%" height="22" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="2" bgcolor="#DDDDCC">&nbsp;</td>
    <td><div align="center">
      <table height="61" cellpadding="0" cellspacing="0" style="height: 332px">
        <tr>
          <td height="33" colspan="6"><p class="STYLE2" style="text-align: center">注册新帐户方便又容易</p></td>
        </tr>
        <tr>
          <td width="24%" align="center" valign="top" style="height: 26px">用户名</td>
          <td valign="top" width="37%" align="left" style="height: 26px">
              <input type="text" name="txtUserName" id="txtUName" placeholder="请输入用户名" class="txtInput" /><span id="userErrorMsg" style="font-size:14px;color:red"></span></td>          
        </tr>
        <tr>
          <td width="24%" height="26" align="center" valign="top">真实姓名：</td>
          <td valign="top" width="37%" align="left">
               <input type="text" name="txtRealUserName" class="txtInput"  /></td>          
        </tr>
        <tr>
          <td width="24%" height="26" align="center" valign="top">密码：</td>
          <td valign="top" width="37%" align="left">
              <input type="password" name="txtUserPwd" class="txtInput" /></td>          
        </tr>
        <tr>
          <td width="24%" height="26" align="center" valign="top">确认密码：</td>
          <td valign="top" width="37%" align="left">
              <input type="password" name="txtConfirmPwd" class="txtInput" /></td>          
        </tr>
         <tr>
          <td width="24%" height="26" align="center" valign="top">Email：</td>
          <td valign="top" width="37%" align="left">
             <input type="text" name="txtUserEmail" id="txtUEmail"  class="txtInput"/>
              <span id="mailErrorMsg" style="font-size:14px;color:red"></span>

          </td>          
        </tr>
        <tr>
          <td width="24%" height="26" align="center" valign="top">地址：</td>
          <td valign="top" width="37%" align="left">
              <input type="text" name="txtUserAddress" class="txtInput" /></td>          
        </tr>
        <tr>
          <td width="24%" height="26" align="center" valign="top">手机：</td>
          <td valign="top" width="37%" align="left">
              <input type="text" name="txtUserPhone" class="txtInput" /></td>          
        </tr>
        <tr>
          <td width="24%" height="26" align="center" valign="top">
              验证码：</td>
          <td valign="top" width="37%" align="left">
              <input type="text" name="txtValidateCode" id="txtCode" class="txtInput" placeholder="不区分大小" />
              <a href="javascript:void(0)" id="vCode">  <img src="/ashx/ValidateCode.ashx?id=1" id="imgCode" /></a>
            <span id="vcodeMsg" style="font-size:14px;color:red"></span>
          </td>          
        </tr>
        <tr>
          <td colspan="2" align="center"><input type="button" value="同意协议并注册"  id="btnReg" class="regnow"/>
               <span id="regMsg" style="font-size:14px;color:red"></span>
          </td>           
        </tr>
        <tr>
          <td colspan="2" align="center">&nbsp;</td>           
        </tr>
      </table>
      <div class="STYLE5">---------------------------------------------------------</div>
    </div>	
    </td>
    <td width="2" bgcolor="#DDDDCC">&nbsp;</td>
  </tr>
</table>

<table width="80%" height="3" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="3" bgcolor="#DDDDCC"><img src="../Images/touming.gif" width="27" height="9" /></td>
  </tr>
</table>
</div>

</asp:Content>
