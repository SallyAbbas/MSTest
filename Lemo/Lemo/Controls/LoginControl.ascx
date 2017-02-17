<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginControl.ascx.cs"
    Inherits="Lemo.Controls.LoginControl" %>
<div style="padding: 5px; margin: 5px;">    
    <asp:Panel style="text-align: left;" runat="server" ID="panelMain" DefaultButton="butLogin">
        <fieldset style="width: 90%;">
            <legend>
                <asp:Label runat="server" ID="lblTitle" Text="Customer" CssClass="TitleInnerPage"></asp:Label></legend>
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
                        <asp:ImageButton runat="server" ID="butSignUp" ImageUrl="~/Image/signupbtn.png" OnClick="butSignUp_Click" />
                        <asp:ImageButton runat="server" ID="butLogin" ValidationGroup="vgLogin" ImageUrl="~/Image/loginbtn.png"
                            OnClick="butLogin_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
</div>
