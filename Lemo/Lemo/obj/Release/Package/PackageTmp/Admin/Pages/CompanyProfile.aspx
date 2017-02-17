<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CompanyProfile.aspx.cs" Inherits="Lemo.Admin.Pages.CompanyProfile" %>

<%@ Register src="../Controls/CompanyInformation.ascx" tagname="CompanyInformation" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:CompanyInformation ID="CompanyInformation1" runat="server" />
</asp:Content>
