<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Settle.aspx.cs" Inherits="Lemo.Admin.Pages.Settle" %>
<%@ Register src="~/Admin/Controls/Settle.ascx" tagname="Dispatch" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <uc1:Dispatch ID="Dispatch1" runat="server" />
   
</asp:Content>
