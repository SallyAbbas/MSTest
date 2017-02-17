<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeesDetais.ascx.cs"
    Inherits="Lemo.Controls.FeesDetais" %>
<script language="javascript" type="text/javascript">
    $(function () {
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
        $(".totalTaxes").click(function (ev) {
            $(".taxes").slideToggle("slow");
        });
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
<div style="padding: 15px; margin: 15px; width: 250px; background-color: #C6C3CA;
    border-radius: 5px 5px 5px 5px;">
    <div class="TitleInnerPage">
        Booking Summary</div>
    <div>
        <table>
            <tr>
                <td class="Lable" id="tdFrom" runat="server" clientidmode="Static" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="Lable" id="tdTo" runat="server" clientidmode="Static" colspan="2">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td class="Lable" style="font-weight: bold;">
                    Estimated Fare:
                </td>
                <td class="Lable" id="tdBaseFees" runat="server" clientidmode="Static">
                </td>
            </tr>
            <tr>
                <td class="Lable" style="font-weight: bold;">
                    Gratuity:
                </td>
                <td class="Lable" id="tdGratuity" runat="server" clientidmode="Static">
                </td>
            </tr>
            <tr>
                <td class="Lable" style="font-weight: bold;">
                    Processing Fee:
                </td>
                <td class="Lable" id="tdProcessingFees" runat="server" clientidmode="Static">
                </td>
            </tr>
            <tr class="totalTaxes" style="cursor: pointer;" title="Details">
                <td class="Lable" style="font-weight: bold;">
                    <img src="../Image/Details.png" width="16" height="16" />
                    Taxes:
                </td>
                <td class="Lable" id="tdTaxes" runat="server" clientidmode="Static">
                </td>
            </tr>
            <tr class="taxes">
                <td class="" style="font-weight: normal; font-size: 16; padding-left: 15px;">
                    NJ State Sales Taxes:
                </td>
                <td class="Lable" id="tdStateTaxes" runat="server" clientidmode="Static">
                </td>
            </tr>
            <tr class="taxes">
                <td colspan="2" style="font-weight: normal; font-size: 16; padding-left: 15px;">
                    local taxes will be added if applied
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
            <tr>
                <td colspan="2" style="font-size: 12px; font-weight: normal;">
                    Price not include toll, parking, waiting time and any extra stops.<br />
                </td>
            </tr>
        </table>
    </div>
    <div style="background-color: #8B8B8B; width: 100%;">
        <table>
            <tr>
                <td colspan="2" align="center" class="LableBold">
                    <div style="font-size: 25px;">
                        Total Estimated Fare</div>
                    <asp:Label runat="server" ID="lblTotal" CssClass="LableBold" ClientIDMode="Static"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="text-align: center;">
        <asp:Button ID="butPayment" runat="server" Text="Continue to Payment" BackColor="#FEA716"
            CssClass="butContinue" OnClick="butPayment_Click" />
    </div>
</div>
<asp:HiddenField runat="server" ID="hfCarSeatValue" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hfInsidePickup" ClientIDMode="Static" />
