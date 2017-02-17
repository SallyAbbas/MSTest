<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Lemo.Pages.Login" %>

<%@ Register Src="../Controls/LoginControl.ascx" TagName="LoginControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
