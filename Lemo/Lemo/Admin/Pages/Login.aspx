<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Lemo.Admin.Pages.Login" %>
<%@ Register Src="../Controls/Login.ascx" TagName="LoginControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center;">
        <table width="100%">
            <tr>
                <td align="center">
                    <uc1:LoginControl ID="LoginControl1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
