<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyInformation.ascx.cs"
    Inherits="Lemo.Admin.Controls.CompanyInformation" %>
<script type="text/javascript">
    $(function () {
        $(".LimoAPIQuestion").click(function () {
            window.open("../Pages/LimoAnyWereSteps.aspx", "Google", "width=500,height=500");
        });
    });
</script>
<div style="padding: 5px; margin: 5px;">
    <div style="width: 95%; text-align: left;">
        <div runat="server" id="divConfirmation" visible="false" style="font-weight: bold;
            font-size: 30px; color: Green;">
            Your information have been saved.
        </div>
        <div runat="server" id="divError" visible="false" style="font-weight: bold; font-size: 30px;
            color: red;">
            There are problem with your information.
        </div>
        <fieldset>
            <legend>
                <asp:Label runat="server" ID="Label1" Text="Company Information" CssClass="TitleInnerPage"></asp:Label></legend>
            <table width="100%">
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblFirstName" runat="server" Text="Company Name"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCompanyName" runat="server" ReadOnly="True"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtCompanyName" ErrorMessage="Required" ID="RequiredFieldValidator0"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        Is Available?&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="cbIsAvailable" runat="server" />
                        &nbsp;
                        <br />
                        Are You Service&nbsp; All State?
                        <asp:CheckBox ID="cbAllState" runat="server" />
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblUsername" runat="server" Text="User name"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtUsername" runat="server" ReadOnly="true"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtUsername" ErrorMessage="Required" ID="RequiredFieldValidator8"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td runat="server" id="tdIsActive">
                        Is Top?
                        <asp:CheckBox ID="cbIsTop" runat="server" />
                        <br />
                        Is Active?
                        <asp:CheckBox ID="cbIsActive" runat="server" />
                        <%-- <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtPassword" ErrorMessage="Required" ID="RequiredFieldValidator4"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        --%>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblCountry" runat="server" Text="State"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlState" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblCityTown" runat="server" Text="City/Town"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCityTown" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtCityTown" ErrorMessage="Required" ID="RequiredFieldValidator9"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblPrimaryAddress" runat="server" Text="Address"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPrimaryAddress" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtPrimaryAddress" ErrorMessage="Required" ID="RequiredFieldValidator6"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblZipPost" runat="server" Text="Zip/Post"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtZipPost" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtZipPost" ErrorMessage="Required" ID="RequiredFieldValidator7"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                            Display="Static" ErrorMessage="Required" ForeColor="Red" ValidationGroup="vgSignUp"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                            Display="Static" ErrorMessage="Invalid email address" ForeColor="Red" ValidationExpression="^((?:(?:(?:[a-zA-Z0-9][\.\-\+_]?)*)[a-zA-Z0-9])+)\@((?:(?:(?:[a-zA-Z0-9][\.\-_]?){0,62})[a-zA-Z0-9])+)\.([a-zA-Z0-9]{2,6})$"
                            ValidationGroup="vgSignUp"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblMobilePhone" runat="server" Text="Phone"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtMobilePhone" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMobilePhone"
                            Display="Static" ErrorMessage="Required" ForeColor="Red" ValidationGroup="vgSignUp"></asp:RequiredFieldValidator>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblPricePerHour" runat="server" Text="Upload your Logo"></asp:Label>
                        <br />
                        <asp:FileUpload ID="fu_Image" runat="server" />
                        &nbsp;
                        <asp:RegularExpressionValidator ID="REV_Img0" runat="server" ControlToValidate="fu_Image"
                            Display="Static" ErrorMessage="RegularExpressionValidator" Font-Bold="True" ForeColor="Red"
                            ToolTip="ex: upload just jpg | gif | png " ValidationExpression="^.*\.(jpg|JPG|gif|GIF|PNG|png)$"
                            ValidationGroup="vgSignUp"><img 
                            alt="" src="../../Image/icon_error.gif" />upload just jpg | gif | png</asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:Image runat="server" ID="imgLogo" Width="24px" Height="24px" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblLimoAPIID" runat="server" Text="Limo API ID"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtLimoAPIID" runat="server"></asp:TextBox>
                        <%-- <asp:RequiredFieldValidator
                            ControlToValidate="txtLimoAPIID" ErrorMessage="Required" ID="RequiredFieldValidator10"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>--%>
                        <img src="../../Image/question.png" width="16px" height="16px" class="LimoAPIQuestion"
                            style="cursor: pointer;" />
                    </td>
                    <td>
                        <asp:Label ID="lblLimoAPIKey" runat="server" Text="Limo API Key"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtLimoAPIKey" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator
                            ControlToValidate="txtLimoAPIKey" ErrorMessage="Required" ID="RequiredFieldValidator11"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>--%>
                        <img src="../../Image/question.png" width="16px" height="16px" class="LimoAPIQuestion"
                            style="cursor: pointer;" />
                    </td>
                    <td id="tdAllowDespatch" runat="server">
                     Allow Despatch?
                        <asp:CheckBox ID="cbAllowDespatch" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <asp:Label runat="server" ID="Label2" Text="Company Profile" CssClass="TitleInnerPage"></asp:Label></legend>
            <table width="100%">
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblAbout_Us" runat="server" Text="About Us"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtAbout_Us" runat="server" Height="64px" TextMode="MultiLine" Width="492px"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtAbout_Us" ErrorMessage="Required"
                            ID="RequiredFieldValidator1" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"
                            Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td class="style1">
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
                    <td class="style1">
                        <asp:Label ID="lblOUR_SERVICE" runat="server" Text="Our Service"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtOUR_SERVICE" runat="server" Height="64px" TextMode="MultiLine"
                            Width="492px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ControlToValidate="txtOUR_SERVICE" ErrorMessage="Required"
                            ID="RequiredFieldValidator5" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"
                            Display="Static"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblOUR_FLEET" runat="server" Text="Our Fleet"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtOUR_FLEET" runat="server" Height="64px" TextMode="MultiLine"
                            Width="492px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ControlToValidate="txtOUR_FLEET" ErrorMessage="Required"
                            ID="RequiredFieldValidator10" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"
                            Display="Static"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblContact_Us" runat="server" Text="Contact Us"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtContact_Us" runat="server" Height="64px" TextMode="MultiLine"
                            Width="492px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ControlToValidate="txtContact_Us" ErrorMessage="Required"
                            ID="RequiredFieldValidator4" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"
                            Display="Static"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <asp:Label runat="server" ID="Label3" Text="Taxes" CssClass="TitleInnerPage"></asp:Label></legend>
            <table width="100%">
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblTaxState" runat="server" Text="State Tax"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtTaxState" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtTaxState"
                            ForeColor="Red" ValidationExpression="^(?!0$|0\d)\d{1,3}(\.\d{1,2})?$|^$" ErrorMessage="Enter a valid Tax"
                            ValidationGroup="vgSignUp">*</asp:RegularExpressionValidator>
                        <%--<asp:RequiredFieldValidator ControlToValidate="txtTaxState" ErrorMessage="Required"
                            ID="RequiredFieldValidator14" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblTaxCity" runat="server" Text="City Tax"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtTaxCity" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtTaxCity"
                            ForeColor="Red" ValidationExpression="^(?!0$|0\d)\d{1,3}(\.\d{1,2})?$|^$" ErrorMessage="Enter a valid Tax"
                            ValidationGroup="vgSignUp">*</asp:RegularExpressionValidator>
                    </td>               
                    <td class="style1">
                        <asp:Label ID="lblOtherTax1" runat="server" Text="Other Tax1"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtOtherTax1" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtOtherTax1"
                            ForeColor="Red" ValidationExpression="^(?!0$|0\d)\d{1,3}(\.\d{1,2})?$|^$" ErrorMessage="Enter a valid Tax"
                            ValidationGroup="vgSignUp">*</asp:RegularExpressionValidator>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblOtherTax1Note" runat="server" Text="Other Tax1 Note"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtOtherTax1Note" runat="server"></asp:TextBox>
                    </td>
                     </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblOtherTax2" runat="server" Text="Other Tax2"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtOtherTax2" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtOtherTax2"
                            ForeColor="Red" ValidationExpression="^(?!0$|0\d)\d{1,3}(\.\d{1,2})?$|^$" ErrorMessage="Enter a valid Tax"
                            ValidationGroup="vgSignUp">*</asp:RegularExpressionValidator>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblOtherTax2Note" runat="server" Text="Other Tax2 Note"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtOtherTax2Note" runat="server"></asp:TextBox>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblGratuity" runat="server" Text="Gratuity"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtGratuity" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtGratuity"
                            ForeColor="Red" ValidationExpression="^(?!0$|0\d)\d{1,3}(\.\d{1,2})?$|^$" ErrorMessage="Enter a valid Tax"
                            ValidationGroup="vgSignUp">*</asp:RegularExpressionValidator>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblGratuityNote" runat="server" Text="Gratuity Note"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtGratuityNote" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Image/Save.png" OnClick="butSignUp_Click"
                        ValidationGroup="vgSignUp" />
                </td>
            </tr>
        </table>
    </div>
</div>
