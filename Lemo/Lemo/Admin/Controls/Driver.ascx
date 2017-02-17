<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Driver.ascx.cs" Inherits="Lemo.Admin.Controls.Driver" %>
<div style="padding: 5px; margin: 5px;">
    <div style="width: 75%; text-align: left;">
        <asp:Label runat="server" ID="lblError" Text="The username you selected for this account already exists. Please select another username"
            CssClass="TitleError" Visible="false"></asp:Label>
        <fieldset>
            <legend>
                <asp:Label runat="server" ID="Label1" Text="Driver" CssClass="TitleInnerPage"></asp:Label></legend>
            <table>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtFirstName" ErrorMessage="Required" ID="RequiredFieldValidator0"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtLastName" ErrorMessage="Required" ID="RequiredFieldValidator1"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtEmail" ErrorMessage="Required" ID="RequiredFieldValidator2"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ControlToValidate="txtEmail" ValidationExpression="^((?:(?:(?:[a-zA-Z0-9][\.\-\+_]?)*)[a-zA-Z0-9])+)\@((?:(?:(?:[a-zA-Z0-9][\.\-_]?){0,62})[a-zA-Z0-9])+)\.([a-zA-Z0-9]{2,6})$"
                                ForeColor="Red" ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid email address"
                                ValidationGroup="vgSignUp" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                    </td>
                    <td>
                        <asp:Label ID="lblMobilePhone" runat="server" Text="Mobile Phone"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtMobilePhone" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtMobilePhone" ErrorMessage="Required" ID="RequiredFieldValidator3"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <%-- <asp:RegularExpressionValidator
                    ControlToValidate="txtMobilePhone" ValidationExpression="" ForeColor="Red"
                    ID="RegularExpressionValidator1" runat="server" ErrorMessage="Not a valid phone"
                    ValidationGroup="vgSignUp"></asp:RegularExpressionValidator>--%>
                    </td>
                </tr>               
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        Show on Map<br />
                        <asp:CheckBox ID="cbShowOnMap" runat="server" Checked="true" />
                    </td>
                </tr>               
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblCars" runat="server" Text="Select Car"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCars" runat="server" Height="20px" Width="250px">
                        </asp:DropDownList>
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
