<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settle.ascx.cs" Inherits="Lemo.Admin.Controls.Settle" %>
<div>
    <div runat="server" id="divConfirmation" visible="false" style="font-weight: bold;
        font-size: 30px; color: Green;">
        Your information have been saved.
    </div>
    <div runat="server" id="divError" visible="false" style="font-weight: bold; font-size: 30px;
        color: red;">
        There are problem with your information.
    </div>
    <div id="Main" style="width: 90%; padding: 5px;">
        <br />
        <div id="StatusAndDriver" runat="server">
            <table width="95%">
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblStatus" runat="server" Text="Select Status"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStatus" Width="200px" Height="20px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlStatus" ErrorMessage="*" InitialValue="0"
                            ID="RequiredFieldValidator12" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblDriver" runat="server" Text="Select Driver"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDrivers" Width="200px" Height="20px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlDrivers" ErrorMessage="*" InitialValue="0"
                            ID="RequiredFieldValidator13" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
        <div runat="server" id="divUserInf">
            <fieldset>
                <legend>
                    <asp:Label runat="server" ID="Label1" Text="Customer Information" CssClass="TitleInnerPage"></asp:Label></legend>
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
                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                ControlToValidate="txtLastName" ErrorMessage="Required" ID="RequiredFieldValidator1"
                                ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txUserEmail"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txUserEmail" ErrorMessage="Required"
                                ID="RequiredFieldValidator2" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"
                                Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ControlToValidate="txUserEmail"
                                    ValidationExpression="^((?:(?:(?:[a-zA-Z0-9][\.\-\+_]?)*)[a-zA-Z0-9])+)\@((?:(?:(?:[a-zA-Z0-9][\.\-_]?){0,62})[a-zA-Z0-9])+)\.([a-zA-Z0-9]{2,6})$"
                                    ForeColor="Red" ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid email address"
                                    ValidationGroup="vgSignUp" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblMobilePhone" runat="server" Text="Mobile Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPH" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtPH" ErrorMessage="Required" ID="RequiredFieldValidator3"
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
        <fieldset>
            <legend>
                <asp:Label runat="server" ID="Label2" Text="Job Details" CssClass="TitleInnerPage"></asp:Label></legend>
            <table width="75%">
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblFrom" runat="server" Text="From"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtFrom" runat="server" Width="520px"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtFrom" ErrorMessage="Required" ID="RequiredFieldValidator4"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTo" runat="server" Text="To"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtTo" runat="server" Width="520px"></asp:TextBox></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtTo" ErrorMessage="Required" ID="RequiredFieldValidator5"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label5" runat="server" Text="Pick Up Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPickUpDate" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtPickUpDate" ErrorMessage="Required" ID="RequiredFieldValidator6"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Pick Up Time"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPickUpTime" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtPickUpTime" ErrorMessage="Required" ID="RequiredFieldValidator7"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <%-- <asp:RegularExpressionValidator
                    ControlToValidate="txtMobilePhone" ValidationExpression="" ForeColor="Red"
                    ID="RegularExpressionValidator1" runat="server" ErrorMessage="Not a valid phone"
                    ValidationGroup="vgSignUp"></asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
            </table>
        </fieldset>
        <div id="divPriceDetails" runat="server">
            <fieldset>
                <legend>
                    <asp:Label runat="server" ID="Label11" Text="Price Details" CssClass="TitleInnerPage"></asp:Label></legend>
                <table>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label3" runat="server" Text="Estimated Fare Price"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEstimatedFarePrice" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                ControlToValidate="txtEstimatedFarePrice" ErrorMessage="Required" ID="RequiredFieldValidator8"
                                ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Gratuity Pricee"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGratuity" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                ControlToValidate="txtGratuity" ErrorMessage="Required" ID="RequiredFieldValidator9"
                                ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label7" runat="server" Text="Processing Fee"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtProcessingFee" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                ControlToValidate="txtProcessingFee" ErrorMessage="Required" ID="RequiredFieldValidator10"
                                ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Taxes"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaxes" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                ControlToValidate="txtTaxes" ErrorMessage="Required" ID="RequiredFieldValidator11"
                                ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label9" runat="server" Text="Other Price"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOtherPrice" runat="server"></asp:TextBox>
                            <%--  <asp:RequiredFieldValidator
                                ControlToValidate="txtOtherPrice" ErrorMessage="Required" ID="RequiredFieldValidator12"
                                ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Other Price Not"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOtherPriceNote" runat="server"></asp:TextBox>
                            <%--  <asp:RequiredFieldValidator
                                ControlToValidate="txtOtherPriceNote" ErrorMessage="Required" ID="RequiredFieldValidator13"
                                ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>  --%>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div style="text-align: center;">
            <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Image/Save.png" OnClick="butSignUp_Click"
                ValidationGroup="vgSignUp" />
        </div>
    </div>
</div>
