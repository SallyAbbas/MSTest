<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyReservation.aspx.cs" Inherits="Lemo.Pages.MyReservation" %>
<%@ Register src="../Admin/Controls/Reservation.ascx" tagname="Reservation" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:Reservation ID="Reservation1" runat="server" />
</asp:Content>
