<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="Lemo.Admin.Controls.Login" %>
<div style="padding: 5px; margin: 5px;">
    <asp:Label runat="server" ID="lblError" Text="Incorrect username or password or your company not active" CssClass="TitleError"
        Visible="false"></asp:Label>
    <div style="width: 30%;text-align: left;">
        <fieldset>
            <legend>
                <asp:Label runat="server" ID="lblTitle" Text="Login To Your Account" CssClass="TitleInnerPage"></asp:Label></legend>
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
                    <td align="right">                        
                                       
                            <asp:ImageButton runat="server" ID="butLogin" ValidationGroup="vgLogin"  ImageUrl="~/Image/loginbtn.png" 
                            onclick="butLogin_Click" />      
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>