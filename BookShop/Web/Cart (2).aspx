<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="BookShop.Web.Cart" %>
<%@ Import Namespace="BookShop.Model" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link href="Css/themes/ui-lightness/jquery-ui-1.8.2.custom.css" rel="stylesheet" />
    <script src="js/jquery-ui-1.8.2.custom.min.js"></script>
    <script type="text/javascript">
        $(function () {
            getTotalMoney();
        });
        function showDialog() {
            $("#dialog-message").dialog({
                modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
        function changeBar(operator, bookId, cartId) {
            var control=$("#txtCount" + bookId)
            var count =control.val();//获取文本框中的值。
            count = parseInt(count);
            if (operator == '+') {
                count = count + 1;
                if (count > 1000) {
                    $("#showUserLoginMsg").text("数量最多为1000");
                    showDialog();
                    //alert("数量最多为1000");
                    return false;
                }
            } else {
                count = count - 1;
                if (count < 1) {
                    //alert("数量最少为1");
                    $("#showUserLoginMsg").text("数量最少为1");
                    showDialog();
                    return false;
                }
            }
            $.post("/ashx/EditCart.ashx", {"count":count,"cartId":cartId,"action":"edit"}, function (data) {
                if (data == "ok") {
                    //更新文本框中显示的数量.
                    control.val(count);
                    //更新商品的价格
                    getTotalMoney();
                } else {
                    $("#showUserLoginMsg").text("更新失败");
                    showDialog();
                    //alert("更新失败");
                }

            });

        }
        //计算总金额。
        function getTotalMoney() {
            var totalMoney = 0;
            $(".align_Center:gt(0)").each(function () {
                var price = $(this).find(".price").text();//获取单价
                var count = $(this).find("input").val();
                totalMoney=totalMoney+(parseInt(count)*parseFloat(price));
            });
            $("#tMoney").text(fmoney(totalMoney,2));
        }
        //控制价格的格式
        function fmoney(s, n) {
            n = n > 0 && n <= 20 ? n : 2;
            s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";//更改这里n数也可确定要保留的小数位           
            var l = s.split(".")[0].split("").reverse(),
         r = s.split(".")[1];
            t = "";
            for (i = 0; i < l.length; i++) {
                t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
            }
            return t.split("").reverse().join("") + "." + r.substring(0, 2);//保留2位小数  如果要改动 把substring 最后一位数改动就可  
        }
        //删除商品
        function removeProductOnShoppingCart(cartId,control) {
            if (confirm("确定要删除吗?")) {
                $.post("/ashx/EditCart.ashx", { "cartId": cartId, "action": "del" }, function (data) {
                    if (data == "ok") {
                        $(control).parent().parent().remove();
                        //更新商品的价格
                        getTotalMoney();
                    } else {
                        $("#showUserLoginMsg").text("删除失败");
                        showDialog();
                    }
                });
            }
        }
        //失去焦点时执行
        function changeTextOnBlur(control,cartId) {
            var count = $(control).val();
            var regex = /^\d+$/;
            if (regex.test(count)) {
                //发送一个异步请求，更新数量
            } else {
                $("#showUserLoginMsg").text("数量只能是数字!!");
                showDialog();
                $(control).val($("#txtProductCount").val());//失去焦点时重新赋值。
            }
        }
        //获取焦点
        function changeTxtOnFocus(control) {
            var count = $(control).val();
            //将获取的数量存储到隐藏域中。
            $("#txtProductCount").val(count);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="hidden" id="txtProductCount" />
    <div>
    <table cellpadding="0" cellspacing="0"  width="98%">
        <tr>
            <td colspan="2">
                <img height="27" 
                    src="images/shop-cart-header-blue.gif" width="206" /><img alt="" 
                    src="Images/png-0170.png" /><asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl="~/myorder.aspx">我的订单</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" width="98%">
          
                <table  cellpadding='0' cellspacing='0' width='100%' >
 <tr class='align_Center Thead'>
    <td width='7%' style='height:30px'>图片</td>
    <td>图书名称</td>
    <td width='14%'>单价</td>
    <td width='11%'>购买数量</td>
    <td width='7%'>删除图书</td>
 </tr>

       
   <%foreach(Cart modelCart in this.CartList){%>      
<!--一行数据的开始 -->
<tr class='align_Center'>
   <td style='padding:5px 0 5px 0;'><img src='/images/bookcovers/<%=modelCart.Book.ISBN %>.jpg' width="40" height="50" border="0" /></td>
   <td class='align_Left'><%=modelCart.Book.Title %></td>
   <td>
<span class='price'><%=modelCart.Book.UnitPrice.ToString("0.00") %></span>
</td>
   <td><a href='#none' title='减一' onclick="changeBar('-',<%=modelCart.Book.Id %>,<%=modelCart.Id%>)" style='margin-right:2px;' ><img src="Images/bag_close.gif" width="9" height="9" border='none' style='display:inline' /></a>
     <input type='text' id='txtCount<%=modelCart.Book.Id%>' name='txtCount<%=modelCart.Book.Id%>' maxlength='3' style='width:30px' onKeyDown='if(event.keyCode == 13) event.returnValue = false' value='<%=modelCart.Count %>' onfocus='changeTxtOnFocus(this);' onblur="changeTextOnBlur(this,<%=modelCart.Id%>);" />
     <a href='#none' title='加一' onclick="changeBar('+',<%=modelCart.Book.Id %>,<%=modelCart.Id%>)" style='margin-left:2px;' ><img src='/images/bag_open.gif' width="9" height="9" border='none' style='display:inline' /></a>   </td>
   <td>
   <a href='#none' id='btn_del_1000357315' onclick="removeProductOnShoppingCart(<%=modelCart.Id %>,this)" >
       删除</a></td>
</tr>
<!--一行数据的结束 -->
  <%} %>
   
 <tr>
    <td class='align_Right Tfoot' colspan='5' style='height:30px'>&nbsp;</td>
 </tr>
</table>
</td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;&nbsp;&nbsp; 商品金额总计：<span ID="tMoney" style="font-size:20px;color:red;font-weight:bolder;font-family:微软雅黑" 
                   >0</span>元</td>
            <td>
                &nbsp;
               <a href="booklist.aspx"> <img alt="" src="Images/gobuy.jpg" width="103" height="36" border="0" /> </a><a href="OrderConfirm.aspx"><img src="images/balance.gif" 
                     border="0" /></a>
              
            </td>
        </tr>
    </table>
    </div>
    <div id="dialog-message" title="错误提示!!">
	<span id="showUserLoginMsg" style="font-size:14px;color:red"></span>
</div>

</asp:Content>
