<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCompany.ascx.cs"
    Inherits="Lemo.Admin.Controls.AddCompany" %>
<div style="text-align: center;">
    <asp:Label runat="server" ID="lblError" Text="Company name already exist" CssClass="TitleError"
        Visible="false"></asp:Label>
    <div style="width: 50%; text-align: left; padding: 5px; margin: 5px;">
        <fieldset>
            <legend>
                <asp:Label runat="server" ID="Label1" Text="Add New Company" CssClass="TitleInnerPage"></asp:Label></legend>
            <table>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtCompanyName" ErrorMessage="Required" ID="RequiredFieldValidator0"
                            Display="Dynamic" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblUserName" runat="server" Text="User Name"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtUserName" ErrorMessage="Required" ID="RequiredFieldValidator1"
                            Display="Dynamic" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1" id="tdPassword" runat="server">
                        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtPassword" ErrorMessage="Required" ID="RequiredFieldValidator4"
                            Display="Dynamic" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td id="tdtdConfirmPassword" runat="server">
                        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtConfirmPassword" ErrorMessage="Required" ID="RequiredFieldValidator5"
                            Display="Dynamic" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passwords do not match."
                            ValidationGroup="vgSignUp" ForeColor="Red" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                            Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMobilePhone" runat="server" Text="Phone"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtMobilePhone" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMobilePhone"
                            Display="Static" ErrorMessage="Required" ForeColor="Red" ValidationGroup="vgSignUp"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="vgSignUp"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" ErrorMessage="Invalid email address" ForeColor="Red" ValidationExpression="^((?:(?:(?:[a-zA-Z0-9][\.\-\+_]?)*)[a-zA-Z0-9])+)\@((?:(?:(?:[a-zA-Z0-9][\.\-_]?){0,62})[a-zA-Z0-9])+)\.([a-zA-Z0-9]{2,6})$"
                            ValidationGroup="vgSignUp"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1" id="tdIsTop" runat="server">
                        Is Top Company?<br />
                        <asp:CheckBox ID="cbIsTop" runat="server" />
                    </td>
                    <td id="tdIsAvailable" runat="server">
                        Is Available?<br />
                        <asp:CheckBox ID="cbIsAvailable" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="style1" id="tdIsActive" runat="server">
                        Is Active?<br />
                        <asp:CheckBox ID="cbIsActive" runat="server" />
                    </td>
                    <td id="tdIsDespatch" runat="server">>
                        Allow Despatch?<br />
                        <asp:CheckBox ID="cbAllowDespatch" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Image/Save.png" OnClick="butSignUp_Click"
                            ValidationGroup="vgSignUp" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</div>
