<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ManageReservation.aspx.cs" Inherits="Lemo.Admin.Pages.ManageReservation" %>

<%@ Register src="../Controls/ManageReservation.ascx" tagname="ManageReservationControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ManageReservationControl ID="ManageReservation123" runat="server" /> 
</asp:Content>
