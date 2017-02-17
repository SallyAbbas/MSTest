<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CorporatePayment.aspx.cs" Inherits="Lemo.Pages.CorporatePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div runat="server" id="divConfirmation" visible="false" style="font-weight: bold;
        font-size: 30px; color: Green;">
        Your reservation has been saved.<div style="font-weight: bold; font-size: 20px; color: Green;">
            Your confirmation number has been sent to your email.</div>
    </div>
    <div runat="server" id="divProblem" visible="false" style="font-weight: bold; font-size: 30px;
        color: Red;">
        There are error with server.
    </div>
    <div style="padding: 5px; margin: 5px;">
        <asp:Panel Style="text-align: left;" runat="server" ID="panelMain" DefaultButton="butLogin">
            <fieldset style="width: 90%;">
                <legend>
                    <asp:Label runat="server" ID="lblTitle" Text="Corporate" CssClass="TitleInnerPage"></asp:Label></legend>
                <asp:Label runat="server" ID="lblError" Text="Incorrect username or password" CssClass="TitleError"
                    Visible="false"></asp:Label>
                <table>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtUsername" runat="server" Width="150px"></asp:TextBox><asp:RequiredFieldValidator
                                ControlToValidate="txtUsername" ErrorMessage="Required" ID="RequiredFieldValidator0"
                                ValidationGroup="vgLogin" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtPassword" runat="server" Width="150px" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                                ControlToValidate="txtPassword" ErrorMessage="Required" ID="RequiredFieldValidator1"
                                ValidationGroup="vgLogin" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>                            
                            <asp:ImageButton runat="server" ID="butLogin" ValidationGroup="vgLogin" ImageUrl="~/Image/ConfirmBooking.png"
                                OnClick="butLogin_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
