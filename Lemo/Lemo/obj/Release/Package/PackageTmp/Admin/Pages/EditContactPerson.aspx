﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditContactPerson.aspx.cs" Inherits="Lemo.Admin.Pages.EditContactPerson" %>
<%@ Register src="../Controls/AddContactPerson.ascx" tagname="AddContactPerson" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:AddContactPerson ID="AddContactPerson1" runat="server" />
</asp:Content>
