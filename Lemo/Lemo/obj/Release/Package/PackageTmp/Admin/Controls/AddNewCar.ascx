<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddNewCar.ascx.cs" Inherits="Lemo.Admin.Controls.AddNewCar" %>
<div style="padding: 5px; margin: 5px;">
    <asp:Label runat="server" ID="lblError" Text="There are problem with this page." CssClass="TitleError"
        Visible="false"></asp:Label>
    <div style="width: 75%; text-align: left;">
        <fieldset>
            <legend>
                <asp:Label runat="server" ID="Label1" Text="Car" CssClass="TitleInnerPage"></asp:Label></legend>
            <table>
                <tr>
                    <td class="style1" colspan="2">
                        <asp:Label ID="lblCarName" runat="server" Text="Car Name"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCarName" runat="server" Width="295px"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtCarName" ErrorMessage="Required" ID="RequiredFieldValidator0"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                        <br />                        
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblPricePerMile" runat="server" Text="Price Per Mile"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPricePerMile" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPricePerMile"
                            ForeColor="Red" ValidationExpression="^(?!0$|0\d)\d{1,3}(\.\d{1,2})?$|^$" ErrorMessage="Enter a valid Fee"
                            ValidationGroup="vgSignUp">*</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator
                            ControlToValidate="txtPricePerMile" ErrorMessage="Required" ID="RequiredFieldValidator1"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblPricePerHour" runat="server" Text="Price Per Hour"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPricePerHour" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPricePerHour"
                            ForeColor="Red" ValidationExpression="^(?!0$|0\d)\d{1,3}(\.\d{1,2})?$|^$" ErrorMessage="Enter a valid Fee"
                            ValidationGroup="vgSignUp">*</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator
                            ControlToValidate="txtPricePerHour" ErrorMessage="Required" ID="RequiredFieldValidator2"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                    <asp:Label ID="Label2" runat="server" Text="Upload car photo"></asp:Label>
                        <br />
                        <asp:FileUpload ID="fu_Image" runat="server" />
                        &nbsp;
                        <asp:RegularExpressionValidator ID="REV_Img0" runat="server" ControlToValidate="fu_Image"
                            Display="Static" ErrorMessage="RegularExpressionValidator" Font-Bold="True"
                            ForeColor="Red" ToolTip="ex: upload just jpg | gif | png " ValidationExpression="^.*\.(jpg|JPG|gif|GIF|PNG|png)$"
                            ValidationGroup="vgSignUp"><img 
                            alt="" src="../../Image/icon_error.gif" />upload just jpg | gif | png</asp:RegularExpressionValidator>
                        <asp:Image runat="server" ID="imgLogo" Height="24px" Width="24px" />
                    </td>
                    <td>
                    <asp:Label ID="Label3" runat="server" Text="No Passengers"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlNoPassengers" runat="server" Width="77px">
                            <asp:ListItem Text="1" Value="1" />
                            <asp:ListItem Text="2" Value="2" />
                            <asp:ListItem Text="3" Value="3" />
                            <asp:ListItem Text="4" Value="4" />
                            <asp:ListItem Text="5" Value="5" />
                            <asp:ListItem Text="6" Value="6" />
                            <asp:ListItem Text="7" Value="7" />
                            <asp:ListItem Text="8" Value="8" />
                            <asp:ListItem Text="9" Value="9" />
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="11" Value="11" />
                            <asp:ListItem Text="12" Value="12" />
                            <asp:ListItem Text="13" Value="13" />
                            <asp:ListItem Text="14" Value="14" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="2">
                        <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtDescription" runat="server" Height="64px" TextMode="MultiLine"
                            Width="492px"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtDescription" ErrorMessage="Required"
                            ID="RequiredFieldValidator12" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"
                            Display="Static"></asp:RequiredFieldValidator>
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
