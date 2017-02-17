<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeesDetais.ascx.cs"
    Inherits="Lemo.Controls.FeesDetais" %>
<script language="javascript" type="text/javascript">
    $(function () {
        $("#lblTotal").text("$ " + parseFloat(Total).toFixed(2));
        $("#hfCarSeatValue").val("0");
        $("#hfInsidePickup").val("0");
        $("#cbCarSeat").click(function (ev) {
            UpdateTotal();
        });

        $("#cbInsidePickup").click(function (ev) {
            UpdateTotal();
        });
        UpdateValues();

        $(".taxes").toggle();
        //        $(".totalTaxes").click(function (ev) {
        //            $(".taxes").slideToggle("slow");
        //        });
    });

    function UpdateTotal() {
        if ($("#cbInsidePickup").is(':checked')) {
            $("#hfInsidePickup").val("15");
            if ($("#cbCarSeat").is(':checked')) {
                $("#lblTotal").text("$ " + parseFloat(Total + 30 + 15).toFixed(2));
                $("#hfCarSeatValue").val("30");
            }
            else {
                $("#lblTotal").text("$ " + parseFloat(Total + 15).toFixed(2));
                $("#hfCarSeatValue").val("0");
            }
        }
        else {
            $("#hfInsidePickup").val("0");
            if ($("#cbCarSeat").is(':checked')) {
                $("#lblTotal").text("$ " + parseFloat(Total + 30).toFixed(2));
                $("#hfCarSeatValue").val("30");
            }
            else {
                $("#lblTotal").text("$ " + parseFloat(Total).toFixed(2));
                $("#hfCarSeatValue").val("0");
            }
        }
    }

    function UpdateValues() {
        var BaseFees = $("#tdBaseFees").text().substring(1).trim();
        $("#tdBaseFees").text("$ " + parseFloat(BaseFees).toFixed(2));

        var Gratuity = $("#tdGratuity").text().substring(1).trim();
        $("#tdGratuity").text("$ " + parseFloat(Gratuity).toFixed(2));

        var ProcessingFees = $("#tdProcessingFees").text().substring(1).trim();
        $("#tdProcessingFees").text("$ " + parseFloat(ProcessingFees).toFixed(2));

        var StateTaxes = $("#tdStateTaxes").text().substring(1).trim();
        $("#tdStateTaxes").text("$ " + parseFloat(StateTaxes).toFixed(2));

        //        var LocalTxes = $("#tdLocalTxes").text().substring(1).trim();
        //        $("#tdLocalTxes").text("$ " + parseFloat(LocalTxes).toFixed(2));

        //        var DriversWorker = $("#tdDriversWorker").text().substring(1).trim();
        //        $("#tdDriversWorker").text("$ " + parseFloat(DriversWorker).toFixed(2));

        var Taxes = $("#tdTaxes").text().substring(1).trim();
        $("#tdTaxes").text("$ " + parseFloat(Taxes).toFixed(2));
    }
</script>
<script type="text/javascript" language="JavaScript1.3">
    $(window).scroll(function () {
        var top = $(window).scrollTop() - 50;
        $(".FeesDetais1").css("top", top + "px");
    });
</script>
<style>
    .labelFontfeesDetails
    {
        font-size: 12px;
        font-weight: bold;
    }
</style>
<div style="padding-left: 15px; padding-right: 5px; padding-top: 5px; padding-bottom: 5px;
    margin: 15px; width: 250px; background-color: #49BDFA; border-radius: 5px 5px 5px 5px;"
    class="FeesDetais1">
    <div class="TitleInnerPage" style="text-align: center; padding-bottom: 5px; color: White;">
        Booking Summary</div>
    <div style="background-color: #CEE4F8; border-radius: 5px 5px 5px 5px;">
        <div>
            <table width="100%">
                <tr>
                    <td class="Lable" id="tdFrom" runat="server" clientidmode="Static" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="Lable" id="tdTo" runat="server" clientidmode="Static" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td class="Lable labelFontfeesDetails" style="font-weight: bold;">
                        Estimated Fare:
                    </td>
                    <td class="Lable" id="tdBaseFees" runat="server" clientidmode="Static" width="75px"
                        style="color: #FE0002;">
                    </td>
                </tr>
                <tr>
                    <td class="Lable labelFontfeesDetails" style="font-weight: bold;">
                        Gratuity:
                    </td>
                    <td class="Lable" id="tdGratuity" runat="server" clientidmode="Static" width="75px"
                        style="color: #FE0002;">
                    </td>
                </tr>
                <tr>
                    <td class="Lable labelFontfeesDetails" style="font-weight: bold;">
                        Processing Fee:
                    </td>
                    <td class="Lable" id="tdProcessingFees" runat="server" clientidmode="Static" width="75px"
                        style="color: #FE0002;">
                    </td>
                </tr>
                <tr class="totalTaxes">
                    <td class="Lable labelFontfeesDetails" style="font-weight: bold;">
                        <img src="../Image/Details.png" width="16" height="16" />
                        Taxes:
                    </td>
                    <td class="Lable" id="tdTaxes" runat="server" clientidmode="Static" width="75px"
                        style="color: #FE0002;">
                    </td>
                </tr>
                <tr class="taxes">
                    <td class="" style="font-weight: normal; font-size: 10; padding-left: 15px;">
                        Taxes:
                    </td>
                    <td class="Lable" id="tdStateTaxes" runat="server" clientidmode="Static" width="75px"
                        style="color: #FE0002;">
                    </td>
                </tr>
                <tr class="taxes">
                    <td colspan="2" style="font-weight: normal; font-size: 16; padding-left: 15px;">
                        <%--Other charges may apply by service provider.--%>
                    </td>
                </tr>
                <%--<tr class="taxes">
                <td class="" style="font-weight: normal; font-size:16;padding-left:15px;">
                    Local Sales Taxes:
                </td>
                <td class="Lable" id="tdLocalTxes" runat="server" clientidmode="Static">
                </td>
            </tr>
            <tr class="taxes">
                <td class="" style="font-weight: normal; font-size:16;padding-left:15px;">
                    Workers Comp:
                </td>
                <td class="Lable" id="tdDriversWorker" runat="server" clientidmode="Static">
                </td>
            </tr>--%>
                <%-- <tr>
                <td class="Lable" style="font-weight: bold;">
                    Tolls:
                </td>
                <td class="Lable" id="tdTolls" runat="server" clientidmode="Static">
                </td>
            </tr>--%>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="cbCarSeat" ClientIDMode="Static" />
                        <asp:Label runat="server" ID="lblCarSeat" Text="Add Car Seat (+$30.00)" CssClass="Lable"
                            Style="font-weight: bold;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="cbInsidePickup" ClientIDMode="Static" />
                        <asp:Label runat="server" ID="lblInsidePickup" Text="Inside Pickup (+$15.00)" Style="font-weight: bold;"
                            CssClass="Lable"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div style="width: 95%; text-align: center;">
            <%--<table>
            <tr>
                <td colspan="2" align="center" class="LableBold">--%>
            <div style="font-size: 20px; color: #FE0002;" class="LableBold">
                Total Estimated Fare</div>
            <asp:Label runat="server" ID="lblTotal" CssClass="LableBold" ClientIDMode="Static"
                ForeColor="#FE0002"></asp:Label>
            <%--</td>
            </tr>
        </table>--%>
        </div>
    </div>
    <br />
    <%--<div>
        <asp:CheckBox runat="server" ID="cbHasAccount" ClientIDMode="Static" />
        <asp:Label runat="server" ID="lblHasAccount" Text="Has Corporate Account" Style="font-weight: bold;"
            CssClass="Lable"></asp:Label>
    </div>--%>
    <div style="text-align: center;">
        <asp:Button ID="butPayment" runat="server" Text="Continue to Payment" BackColor="#FEA716"
            Visible="false" CssClass="butContinue" OnClick="butPayment_Click" />
        <div>
            <asp:ImageButton ID="ImagebutPayment" runat="server" ImageUrl="~/Image/btnContinue.png"
                OnClick="ImageButton1_Click" /></div>
        <div>
            <asp:ImageButton ID="imgButCoentWithCorporat" runat="server" ImageUrl="~/Image/btnContinueCorportAccount.png"
                OnClick="imgButCoentWithCorporat_Click" /></div>
    </div>
    <br />
    <div style="font-size: 12px; font-weight: normal;">
        <%--  <tr>
                <td colspan="2" style="font-size: 12px; font-weight: normal;">--%>
        Price not include toll, parking, waiting time and any extra stops.<br />
        <div style="font-size: 9px;">
            PLEASE NOTE THAT THE TAXES SHOWN ARE JUST A ESTIMATE. YOUR ACTUAL TAX WILL BE DETERMINED
            BY THE SERVICE PROVIDER. LIMOALLOVER DOES NOT COLLECT ANY SALES TAXES IT IS THE
            SOLE RESPONSIBLITY OF THE SERVICE PROVIDER TO COLLECT THESE TAXES. IF YOU HAVE ANY
            ISUSES REGARDING THESE TAXES PLEASE CONTACT THE SERVICE PROVIDER DIRECTLY.
        </div>
        <%--   </td>
            </tr>--%>
    </div>
</div>
<asp:HiddenField runat="server" ID="hfCarSeatValue" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hfInsidePickup" ClientIDMode="Static" />
