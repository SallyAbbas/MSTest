﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddCar.aspx.cs" Inherits="Lemo.Admin.Pages.AddCar" %>
<%@ Register src="../Controls/AddNewCar.ascx" tagname="AddNewCar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:AddNewCar ID="AddNewCar1" runat="server" />
</asp:Content>