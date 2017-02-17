<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MyAccount.aspx.cs" Inherits="Lemo.Pages.MyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--<script language="javascript" type="text/javascript">
    $(function () {
        alert($('#iframAccount').contents().find('html #ores-main-menu'));
        $('#iframAccount').contents().find('html #ores-main-menu').css('background-color', 'red');
    });
</script>--%>
    <div style="width: 920px; height: 1150px; padding: 10px; margin: 5px;">
        <iframe width="920x" scrolling="auto" height="2000px" frameborder="no" align="middle" id="iframAccount" 
            name="frame1" src="https://book.mylimobiz.com/flyallover/Account/Welcome"></iframe>
    </div>
</asp:Content>
