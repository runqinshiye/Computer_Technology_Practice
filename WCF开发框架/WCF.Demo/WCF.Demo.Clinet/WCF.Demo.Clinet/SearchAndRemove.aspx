<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchAndRemove.aspx.cs" Inherits="WCF.Demo.Client.SearchAndRemove" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        列表页面(修改、删除)：<br />
        <asp:Repeater ID="repUsers" runat="server">
            <ItemTemplate>
                编号：<%#Eval("UserID") %>，用户名：<%#Eval("UserName") %>，密码：<%#Eval("Password") %>，描述：<%#Eval("Discribe") %>提交时间：<%#Eval("SubmitTime") %>----<a href="Save.aspx?UserID=<%#Eval("UserID") %>">修改</a>
                <asp:LinkButton ID="lbtRemove" runat="server" CommandName='<%#Eval("UserID") %>' OnCommand="lbtnRemoveCommand" OnClientClick="return confirm('确定删除吗?');">删除
                </asp:LinkButton><br />
            </ItemTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
