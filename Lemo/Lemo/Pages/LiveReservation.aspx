<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LiveReservation.aspx.cs" Inherits="Lemo.Pages.LiveReservation" %>

<%@ Register Src="../Controls/MapForLiveReservation.ascx" TagName="MapForLiveReservation"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 1000px; height: 500px;">
        <uc1:MapForLiveReservation ID="MapForLiveReservation1" runat="server" />
    </div>
</asp:Content>
