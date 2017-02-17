<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageReservation.aspx.cs" Inherits="Lemo.Pages.ManageReservation" %>
<%@ Register src="../Admin/Controls/ManageReservation.ascx" tagname="ManageReservation" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ManageReservation ID="ManageReservation1" runat="server" />
</asp:Content>
