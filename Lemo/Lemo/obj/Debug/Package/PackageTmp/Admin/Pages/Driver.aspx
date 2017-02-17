<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Driver.aspx.cs" Inherits="Lemo.Admin.Pages.Driver" %>
<%@ Register src="../Controls/Driver.ascx" tagname="Driver" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:Driver ID="Driver1" runat="server" />
</asp:Content>
