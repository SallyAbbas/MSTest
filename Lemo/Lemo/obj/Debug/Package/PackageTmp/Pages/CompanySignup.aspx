<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanySignup.aspx.cs" Inherits="Lemo.Pages.CompanySignup" %>
<%@ Register src="../Admin/Controls/AddCompany.ascx" tagname="AddCompany" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:AddCompany ID="AddCompany1" runat="server" />
</asp:Content>
