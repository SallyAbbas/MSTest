<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="Reservation.aspx.cs" Inherits="Lemo.Admin.Pages.Reservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Main">
        <div runat="server" id="divUser">
            <fieldset>
                <legend>
                    <asp:Label runat="server" ID="Label1" Text="User Information" CssClass="TitleInnerPage"></asp:Label></legend>
                <table width="75%">
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                ControlToValidate="txtFirstName" ErrorMessage="Required" ID="RequiredFieldValidator0"
                                ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></asp:TextBox><asp:RequiredFieldValidator
                                ControlToValidate="txtLastName" ErrorMessage="Required" ID="RequiredFieldValidator1"
                                ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                ControlToValidate="txtEmail" ErrorMessage="Required" ID="RequiredFieldValidator2"
                                ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ControlToValidate="txtEmail" ValidationExpression="^((?:(?:(?:[a-zA-Z0-9][\.\-\+_]?)*)[a-zA-Z0-9])+)\@((?:(?:(?:[a-zA-Z0-9][\.\-_]?){0,62})[a-zA-Z0-9])+)\.([a-zA-Z0-9]{2,6})$"
                                    ForeColor="Red" ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid email address"
                                    ValidationGroup="vgSignUp" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblMobilePhone" runat="server" Text="Mobile Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobilePhone" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                ControlToValidate="txtMobilePhone" ErrorMessage="Required" ID="RequiredFieldValidator3"
                                ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            <%-- <asp:RegularExpressionValidator
                    ControlToValidate="txtMobilePhone" ValidationExpression="" ForeColor="Red"
                    ID="RegularExpressionValidator1" runat="server" ErrorMessage="Not a valid phone"
                    ValidationGroup="vgSignUp"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
</asp:Content>
