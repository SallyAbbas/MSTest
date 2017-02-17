<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageSettle.aspx.cs" Inherits="Lemo.Admin.Pages.ManageSettle" %>

<%@ Register src="../Controls/ManageSettle.ascx" tagname="ManageDispatchControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ManageDispatchControl ID="ManageDispatchControl" runat="server" /> 
</asp:Content>
