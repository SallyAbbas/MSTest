<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="AddCorporate.aspx.cs" Inherits="Lemo.Admin.Pages.AddCorporate" %>

<%@ Register Src="../Controls/AddCompany.ascx" TagName="AddCompany" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:AddCompany ID="AddCompany1" runat="server" IsCorporate="true" />
</asp:Content>
