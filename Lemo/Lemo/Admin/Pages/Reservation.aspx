<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="Reservation.aspx.cs" Inherits="Lemo.Admin.Pages.Reservation" %>

<%@ Register src="../Controls/Reservation.ascx" tagname="Reservation" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <uc1:Reservation ID="Reservation2" runat="server" />
   
</asp:Content>
