<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="Lemo.Signup" %>
<%@ Register src="~/Controls/SignupControl.ascx" tagname="SignupControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="text-align:center;">
<table width="100%"><tr><td align="center">
    <uc1:SignupControl ID="SignupControl1" runat="server" /></div></td></tr></table>
</asp:Content>
