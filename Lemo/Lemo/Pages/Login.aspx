<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Lemo.Pages.Login" %>

<%@ Register Src="../Controls/LoginControl.ascx" TagName="LoginControl" TagPrefix="uc1" %>
<%@ Register Src="../Admin/Controls/Login.ascx" TagName="Login" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center;">
        <table width="100%">
            <tr>
                <td align="center" style="width:35%;display:none;">
                    <uc1:LoginControl ID="LoginControl1" runat="server" />
                </td>
                <td align="center" style="width:35%;">
                    <uc2:Login ID="CompanyLogin" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
