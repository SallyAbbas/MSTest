<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ServiceArea.aspx.cs" Inherits="Lemo.Admin.Pages.ServiceArea" %>
<%@ Register src="../Controls/ManageAroundCities.ascx" tagname="ManageAroundCities" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<uc1:ManageAroundCities ID="ManageAroundCities1" runat="server" />
</asp:Content>
