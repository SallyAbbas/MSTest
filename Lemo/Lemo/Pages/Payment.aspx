<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Payment.aspx.cs" Inherits="Lemo.Pages.Payment" %>

<%@ Register Src="../Controls/FeesDetais.ascx" TagName="FeesDetais" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(function () {
            EnableButton();
            $("#cbTermsConditions").click(function () {
                EnableButton();
            });

            var clicky = $("#clicky");
            clicky.click(function () {
                window.open("TermsConditions.aspx", "test", 'left=20,top=20,width=1000,height=500,toolbar=1,resizable=0');
                return false;
            });

        });
        function EnableButton() {
            if ($("#cbTermsConditions").is(':checked')) {
                $("#<%= butConfirm.ClientID %>").removeAttr('disabled');
            } else {
                $("#<%= butConfirm.ClientID %>").attr('disabled', 'disabled');
            }
        }
    </script>
    <div>
        <table width="100%" class="PaymentLable">
            <tr>
                <td colspan="2" align="center">
                    <div runat="server" id="divConfirmation" visible="false" style="font-weight: bold;
                        font-size: 30px; color: Green;">
                        Your reservation has been saved.<div style="font-weight: bold; font-size: 20px; color: Green;">
                            Your confirmation number has been sent to your email.</div>
                    </div>
                    <div runat="server" id="divProblem" visible="false" style="font-weight: bold; font-size: 30px;
                        color: Red;">
                        There are error with server.
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 75%; padding: 10px 15px 15px;" valign="top;">
                    <fieldset>
                        <legend style="font-weight: bold; font-size: 24px;">Enter Payment Details</legend>
                        <table>
                            <tr>
                                <td colspan="2" align="center">
                                    <img src="../Image/visa_logo.gif" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <img src="../Image/master_logo.gif" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <img src="../Image/american_exp_logo.gif" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <img src="../Image/direct_club_logo.gif" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <img src="../Image/discover_logo.gif" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Card Number:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCardNumber" Width="260px" MaxLength="16"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtCardNumber" ErrorMessage="Required"
                                        ID="RequiredFieldValidator2" ValidationGroup="vgPayment" runat="server" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Zip Code:                                    
                                </td>
                                <td>
                                <asp:TextBox runat="server" ID="txtZipCode" Width="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtZipCode" ErrorMessage="Required"
                                        ID="RequiredFieldValidator6" ValidationGroup="vgPayment" runat="server" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                        <%--
                                    Card Verification Numbe:
                                    <asp:TextBox runat="server" ID="txtCardVerifNumber" Width="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtCardVerifNumber" ErrorMessage="Required"
                                        ID="RequiredFieldValidator10" ValidationGroup="vgPayment" runat="server" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtAdddress" Width="260px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtAdddress" ErrorMessage="Required"
                                        ID="RequiredFieldValidator13" ValidationGroup="vgPayment" runat="server" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    State/Prov:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlState">
                                        <%--<asp:ListItem Value="-1">Select State</asp:ListItem>
                                        <asp:ListItem Value="AL" Text="Alabama"></asp:ListItem>
                                        <asp:ListItem Value="AK" Text="Alaska"></asp:ListItem>
                                        <asp:ListItem Value="AZ" Text="Arizona"></asp:ListItem>
                                        <asp:ListItem Value="AR" Text="Arkansas"></asp:ListItem>
                                        <asp:ListItem Value="CA" Text="California"></asp:ListItem>
                                        <asp:ListItem Value="CO" Text="Colorado"></asp:ListItem>
                                        <asp:ListItem Value="CT" Text="Connecticut"></asp:ListItem>
                                        <asp:ListItem Value="DE" Text="Delaware"></asp:ListItem>
                                        <asp:ListItem Value="DC" Text="District Of Columbia"></asp:ListItem>
                                        <asp:ListItem Value="FL" Text="Florida"></asp:ListItem>
                                        <asp:ListItem Value="GA" Text="Georgia"></asp:ListItem>
                                        <asp:ListItem Value="HI" Text="Hawaii"></asp:ListItem>
                                        <asp:ListItem Value="ID" Text="Idaho"></asp:ListItem>
                                        <asp:ListItem Value="IL" Text="Illinois"></asp:ListItem>
                                        <asp:ListItem Value="IN" Text="Indiana"></asp:ListItem>
                                        <asp:ListItem Value="IA" Text="Iowa"></asp:ListItem>
                                        <asp:ListItem Value="KS" Text="Kansas"></asp:ListItem>
                                        <asp:ListItem Value="KY" Text="Kentucky"></asp:ListItem>
                                        <asp:ListItem Value="LA" Text="Louisiana"></asp:ListItem>
                                        <asp:ListItem Value="ME" Text="Maine"></asp:ListItem>
                                        <asp:ListItem Value="MD" Text="Maryland"></asp:ListItem>
                                        <asp:ListItem Value="MA" Text="Massachusetts"></asp:ListItem>
                                        <asp:ListItem Value="MI" Text="Michigan"></asp:ListItem>
                                        <asp:ListItem Value="MN" Text="Minnesota"></asp:ListItem>
                                        <asp:ListItem Value="MS" Text="Mississippi"></asp:ListItem>
                                        <asp:ListItem Value="MO" Text="Missouri"></asp:ListItem>
                                        <asp:ListItem Value="MT" Text="Montana"></asp:ListItem>
                                        <asp:ListItem Value="NE" Text="Nebraska"></asp:ListItem>
                                        <asp:ListItem Value="NV" Text="Nevada"></asp:ListItem>
                                        <asp:ListItem Value="NH" Text="New Hampshire"></asp:ListItem>
                                        <asp:ListItem Value="NJ" Text="New Jersey"></asp:ListItem>
                                        <asp:ListItem Value="NM" Text="New Mexico"></asp:ListItem>
                                        <asp:ListItem Value="NY" Text="New York"></asp:ListItem>
                                        <asp:ListItem Value="NC" Text="North Carolina"></asp:ListItem>
                                        <asp:ListItem Value="ND" Text="North Dakota"></asp:ListItem>
                                        <asp:ListItem Value="OH" Text="Ohio"></asp:ListItem>
                                        <asp:ListItem Value="OK" Text="Oklahoma"></asp:ListItem>
                                        <asp:ListItem Value="OR" Text="Oregon"></asp:ListItem>
                                        <asp:ListItem Value="PA" Text="Pennsylvania"></asp:ListItem>
                                        <asp:ListItem Value="RI" Text="Rhode Island"></asp:ListItem>
                                        <asp:ListItem Value="SC" Text="South Carolina"></asp:ListItem>
                                        <asp:ListItem Value="SD" Text="South Dakota"></asp:ListItem>
                                        <asp:ListItem Value="TN" Text="Tennessee"></asp:ListItem>
                                        <asp:ListItem Value="TX" Text="Texas"></asp:ListItem>
                                        <asp:ListItem Value="UT" Text="Utah"></asp:ListItem>
                                        <asp:ListItem Value="VT" Text="Vermont"></asp:ListItem>
                                        <asp:ListItem Value="VA" Text="Virginia"></asp:ListItem>
                                        <asp:ListItem Value="WA" Text="Washington"></asp:ListItem>
                                        <asp:ListItem Value="WV" Text="West Virginia"></asp:ListItem>
                                        <asp:ListItem Value="WI" Text="Wisconsin"></asp:ListItem>
                                        <asp:ListItem Value="WY" Text="Wyoming"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlState" ErrorMessage="Required"
                                        ID="RequiredFieldValidator9" InitialValue="-1" ValidationGroup="vgPayment" runat="server"
                                        ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Expiration Date:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlExpirationMonth" runat="server">
                                        <asp:ListItem Value="-1">MM</asp:ListItem>
                                        <asp:ListItem Value="01">01</asp:ListItem>
                                        <asp:ListItem Value="02">02</asp:ListItem>
                                        <asp:ListItem Value="03">03</asp:ListItem>
                                        <asp:ListItem Value="04">04</asp:ListItem>
                                        <asp:ListItem Value="05">05</asp:ListItem>
                                        <asp:ListItem Value="06">06</asp:ListItem>
                                        <asp:ListItem Value="07">07</asp:ListItem>
                                        <asp:ListItem Value="08">08</asp:ListItem>
                                        <asp:ListItem Value="09">09</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlExpirationMonth" ErrorMessage="Required"
                                        ID="RequiredFieldValidator1" InitialValue="-1" ValidationGroup="vgPayment" runat="server"
                                        ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlExpirationYear" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlExpirationYear" ErrorMessage="Required"
                                        ID="RequiredFieldValidator3" InitialValue="-1" ValidationGroup="vgPayment" runat="server"
                                        ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span id="ctl10_ctl00_ctl00_MainFlightHandlerUpdatePanel"><span id="ctl10_ctl00_ctl00_FlightHandlerUpdatePanel">
                                        Card Holder&#39;s </span></span>First Name:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtFirstName"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtFirstName" ErrorMessage="Required"
                                        ID="RequiredFieldValidator5" ValidationGroup="vgPayment" runat="server" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span id="ctl10_ctl00_ctl00_MainFlightHandlerUpdatePanel0"><span id="ctl10_ctl00_ctl00_FlightHandlerUpdatePanel0">
                                        Card Holder&#39;s </span></span>Last Name:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtLastName"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtLastName" ErrorMessage="Required"
                                        ID="RequiredFieldValidator4" ValidationGroup="vgPayment" runat="server" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset>
                        <legend style="font-weight: bold; font-size: 24px;">Passenger Info</legend>
                        <table>
                            <tr>
                                <td>
                                    First Name:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPassFirstName"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtPassFirstName" ErrorMessage="Required"
                                        ID="RequiredFieldValidator11" ValidationGroup="vgPayment" runat="server" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Last Name:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPassLastName"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtPassLastName" ErrorMessage="Required"
                                        ID="RequiredFieldValidator12" ValidationGroup="vgPayment" runat="server" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Mobile Phone:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPassMobile"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtPassMobile" ErrorMessage="Required"
                                        ID="RequiredFieldValidator7" ValidationGroup="vgPayment" runat="server" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    E-mail:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPassEmail"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtPassEmail" ErrorMessage="Required"
                                        ID="RequiredFieldValidator8" ValidationGroup="vgPayment" runat="server" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ControlToValidate="txtPassEmail" ValidationExpression="^((?:(?:(?:[a-zA-Z0-9][\.\-\+_]?)*)[a-zA-Z0-9])+)\@((?:(?:(?:[a-zA-Z0-9][\.\-_]?){0,62})[a-zA-Z0-9])+)\.([a-zA-Z0-9]{2,6})$"
                                        ForeColor="Red" ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid email address"
                                        ValidationGroup="vgPayment" Display="Dynamic"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <div style="text-align: center; background-color: #CEE4F8;">
                        <asp:CheckBox runat="server" ID="cbTermsConditions" ClientIDMode="Static" />
                        <span style="font-weight: bold;" class="Lable">I agree to <a id="clicky" title="Limoallover">
                            terms and conditions</a></span>
                        <br />
                        <asp:ImageButton ID="butConfirm" runat="server" ValidationGroup="vgPayment" OnClick="butConfirm_Click"
                            CssClass="ConfirmButt" ImageUrl="~/Image/ConfirmBooking.png" />
                    </div>
                </td>
                <td valign="top" style="padding-top: 20px;">
                    <uc1:FeesDetais ID="FeesDetais1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
