<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditCar.aspx.cs" Inherits="Lemo.Admin.Pages.EditCar" %>
<%@ Register src="../Controls/AddNewCar.ascx" tagname="AddNewCar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:AddNewCar ID="AddNewCar1" runat="server" />
</asp:Content>
