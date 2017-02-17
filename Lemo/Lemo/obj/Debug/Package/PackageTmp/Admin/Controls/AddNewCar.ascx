<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddNewCar.ascx.cs" Inherits="Lemo.Admin.Controls.AddNewCar" %>
<script type="text/javascript" src="../../../UI/jquery-1.8.3.js"></script>
<script src="../../Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    $(function () {
        AdjustCarNameText();
        $(".selectCarType").click(function () {
            $(".selectCarType").removeClass("selected");
            $(this).addClass("selected");
            $(this).find("input[type='radio'").prop('checked', true);
            var carName = $(this).find("label").text();
            if (carName != "Other") {
                $("#<%= txtCarName.ClientID %>").val(carName);
            }
            AdjustCarNameText();
        });
    });

    function AdjustCarNameText() {
        if ($("#<%= rbOther.ClientID %>").is(':checked')) {
            $("#<%= txtCarName.ClientID %>").prop("readonly", false);
            $(".uploadImage").css("visibility", "visible");
        }
        else {
            $("#<%= txtCarName.ClientID %>").prop("readonly", true);
            $(".uploadImage").css("visibility", "hidden");
        }
    }
</script>
<div style="padding: 5px; margin: 5px;">
    <asp:Label runat="server" ID="lblError" Text="There are problem with this page."
        CssClass="TitleError" Visible="false"></asp:Label>
    <div style="width: 75%; text-align: left;">
        <fieldset>
            <legend>
                <asp:Label runat="server" ID="Label1" Text="Car" CssClass="TitleInnerPage"></asp:Label></legend>
            <table width="100%">
                <tr>
                    <td class="style1" colspan="3" valign="top">
                        <asp:Label runat="server" ID="Label4" Text="Select car type or select other option to upload your car type (recommended size 210*80)"
                            CssClass="TitleInnerPage" ForeColor="Black"></asp:Label>
                        <br />
                        <table width="100%">
                            <tr>
                                <td class="selectCarType">
                                    <asp:RadioButton ID="rbSedan" runat="server" GroupName="CarType" Text="Sedan" />
                                    <%--<asp:Label runat="server" ID="lblSedan" Text="Sedan" CssClass="carTypeLabel"></asp:Label>--%>
                                    <img id="imgSedan" src="../../Image/Cars/Small/Sedan.png" />
                                </td>
                                <td class="selectCarType">
                                    <asp:RadioButton ID="rbSuv" runat="server" GroupName="CarType" Text="Suv" />
                                    <%--<asp:Label runat="server" ID="lblSuv" Text="Suv" CssClass="carTypeLabel"></asp:Label>--%>
                                    <img id="imgSuv" src="../../Image/Cars/Small/suv.png" />
                                </td>
                                <td class="selectCarType">
                                    <asp:RadioButton ID="rbVan" runat="server" GroupName="CarType" Text="Van" />
                                    <%--<asp:Label runat="server" ID="lblVan" Text="Van" CssClass="carTypeLabel"></asp:Label>--%>
                                    <img id="imgVan" src="../../Image/Cars/Small/van.png" />
                                </td>
                                <td class="selectCarType">
                                    <asp:RadioButton ID="rbStretchLimo" runat="server" GroupName="CarType" Text="Stretch Limo" />
                                    <%--<asp:Label runat="server" ID="lblStretchLimo" Text="Stretch Limo" CssClass="carTypeLabel"></asp:Label>--%>
                                    <img id="imgStretchLimo" src="../../Image/Cars/Small/Stretch Limo.png" />
                                </td>
                            </tr>
                            <tr>
                                <td class="selectCarType">
                                    <asp:RadioButton ID="rbStretchSUV" runat="server" GroupName="CarType" Text="Stretch SUV" />
                                    <%--<asp:Label runat="server" ID="lblStretchSUV" Text="Stretch SUV" CssClass="carTypeLabel"></asp:Label>--%>
                                    <img id="imgStretchSUV" src="../../Image/Cars/Small/Stretch SUV.png" />
                                </td>
                                <td class="selectCarType">
                                    <asp:RadioButton ID="rbMinibus" runat="server" GroupName="CarType" Text="Mini Bus" />
                                    <%--<asp:Label runat="server" ID="lblMinibus" Text="Mini Bus" CssClass="carTypeLabel"></asp:Label>--%>
                                    <img id="imgMinibus" src="../../Image/Cars/Small/minibus.png" />
                                </td>
                                <td class="selectCarType">
                                    <asp:RadioButton ID="rbBus" runat="server" GroupName="CarType" Text="Bus" />
                                    <%--<asp:Label runat="server" ID="lblBus" Text="Bus" CssClass="carTypeLabel"></asp:Label>--%>
                                    <img id="imgBUS" src="../../Image/Cars/Small/BUS.png" />
                                </td>
                                <td class="selectCarType">
                                    <asp:RadioButton ID="rbBMW" runat="server" GroupName="CarType" Text="BMW" />
                                    <%--<asp:Label runat="server" ID="lblMinibus" Text="Mini Bus" CssClass="carTypeLabel"></asp:Label>--%>
                                    <img id="imgMercedes" src="../../Image/Cars/Small/bmw.png" />
                                </td>
                            </tr>
                            <tr>
                                <td class="selectCarType">
                                    <asp:RadioButton ID="rbCadilac" runat="server" GroupName="CarType" Text="Cadilac" />
                                    <%--<asp:Label runat="server" ID="lblBus" Text="Bus" CssClass="carTypeLabel"></asp:Label>--%>
                                    <img id="imgmercedes" src="../../Image/Cars/Small/cadilac.png" />
                                </td>
                                <td class="selectCarType">
                                    <asp:RadioButton ID="rbMercedes" runat="server" GroupName="CarType" Text="Mercedes" />
                                    <%--<asp:Label runat="server" ID="lblBus" Text="Bus" CssClass="carTypeLabel"></asp:Label>--%>
                                    <img id="imgMercedes" src="../../Image/Cars/Small/mercedes.png" />
                                </td>
                                <td class="selectCarType">
                                    <asp:RadioButton ID="rbOther" runat="server" GroupName="CarType" Text="Other" />
                                    <%--<asp:Label runat="server" ID="lblOthe" Text="Other" CssClass="carTypeLabel"></asp:Label>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="2">
                        <asp:Label ID="lblCarName" runat="server" Text="Car Name"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCarName" runat="server" Width="295px"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtCarName" ErrorMessage="Required" ID="RequiredFieldValidator0"
                            ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                        <br />
                    </td>
                    <td class="style1">
                        <div class="uploadImage">
                            <asp:Label ID="Label2" runat="server" Text="Upload car photo"></asp:Label>
                            <br />
                            <asp:FileUpload ID="fu_Image" runat="server" />
                            &nbsp;
                            <asp:RegularExpressionValidator ID="REV_Img0" runat="server" ControlToValidate="fu_Image"
                                Display="Static" ErrorMessage="RegularExpressionValidator" Font-Bold="True" ForeColor="Red"
                                ToolTip="ex: upload just jpg | gif | png " ValidationExpression="^.*\.(jpg|JPG|gif|GIF|PNG|png)$"
                                ValidationGroup="vgSignUp"><img 
                            alt="" src="../../Image/icon_error.gif" />upload just jpg | gif | png</asp:RegularExpressionValidator>
                            <asp:Image runat="server" ID="imgLogo" Height="24px" Width="24px" />
                        </div>
                    </td>
                </tr>
                <tr>
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
                            <asp:ListItem Text="15" Value="15" />
                            <asp:ListItem Text="16" Value="16" />
                            <asp:ListItem Text="17" Value="17" />
                            <asp:ListItem Text="18" Value="18" />
                            <asp:ListItem Text="19" Value="19" />
                            <asp:ListItem Text="20" Value="20" />
                            <asp:ListItem Text="21" Value="21" />
                            <asp:ListItem Text="22" Value="22" />
                            <asp:ListItem Text="23" Value="23" />
                            <asp:ListItem Text="24" Value="24" />
                            <asp:ListItem Text="25" Value="25" />
                            <asp:ListItem Text="26" Value="26" />
                            <asp:ListItem Text="27" Value="27" />
                            <asp:ListItem Text="28" Value="28" />
                            <asp:ListItem Text="29" Value="29" />
                            <asp:ListItem Text="30" Value="30" />
                            <asp:ListItem Text="31" Value="31" />
                            <asp:ListItem Text="32" Value="32" />
                            <asp:ListItem Text="33" Value="33" />
                            <asp:ListItem Text="34" Value="34" />
                            <asp:ListItem Text="35" Value="35" />
                            <asp:ListItem Text="36" Value="36" />
                            <asp:ListItem Text="37" Value="37" />
                            <asp:ListItem Text="38" Value="38" />
                            <asp:ListItem Text="39" Value="39" />
                            <asp:ListItem Text="40" Value="40" />
                            <asp:ListItem Text="41" Value="41" />
                            <asp:ListItem Text="42" Value="42" />
                            <asp:ListItem Text="43" Value="43" />
                            <asp:ListItem Text="44" Value="44" />
                            <asp:ListItem Text="45" Value="45" />
                            <asp:ListItem Text="46" Value="46" />
                            <asp:ListItem Text="47" Value="47" />
                            <asp:ListItem Text="48" Value="48" />
                            <asp:ListItem Text="49" Value="49" />
                            <asp:ListItem Text="50" Value="50" />
                        </asp:DropDownList>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblPricePerMile" runat="server" Text="Price Per Mile"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPricePerMile" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPricePerMile"
                            ForeColor="Red" ValidationExpression="^(?!0$|0\d)\d{1,3}(\.\d{1,2})?$|^$" ErrorMessage="Enter a valid Fee"
                            ValidationGroup="vgSignUp">*</asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ControlToValidate="txtPricePerMile" ErrorMessage="Required"
                            ID="RequiredFieldValidator1" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblPricePerHour" runat="server" Text="Price Per Hour"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPricePerHour" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPricePerHour"
                            ForeColor="Red" ValidationExpression="^(?!0$|0\d)\d{1,3}(\.\d{1,2})?$|^$" ErrorMessage="Enter a valid Fee"
                            ValidationGroup="vgSignUp">*</asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ControlToValidate="txtPricePerHour" ErrorMessage="Required"
                            ID="RequiredFieldValidator2" ValidationGroup="vgSignUp" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
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
                    <td valign="top">
                        <asp:Label ID="lblCarNumber" runat="server" Text="Car Number"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCarNumber" runat="server" Width="50px"></asp:TextBox>
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
