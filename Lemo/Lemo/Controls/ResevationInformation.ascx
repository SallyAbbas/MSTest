<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResevationInformation.ascx.cs"
    Inherits="Lemo.Controls.ResevationInformation" %>
<div style="width: 95%; padding: 5px;">
    <fieldset>
        <legend class="TitleInnerPage">Reservation Information</legend>
        <table style="width: 100%;">
            <tr>
                <td class="TitleInnerPage">
                    From:
                </td>
                <td class="TitleInnerPage">
                    <asp:Label runat="server" ID="lblFrom" CssClass="lblReservation"></asp:Label>
                </td>
                <td class="TitleInnerPage">
                    Date:
                </td>
                <td class="TitleInnerPage">
                    <asp:Label runat="server" ID="lblDate" CssClass="lblReservation"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="TitleInnerPage">
                    To:
                </td>
                <td class="TitleInnerPage">
                    <asp:Label runat="server" ID="lblTo" CssClass="lblReservation"></asp:Label>
                </td>
                <td class="TitleInnerPage">
                    Time:
                </td>
                <td class="TitleInnerPage">
                    <asp:Label runat="server" ID="lblTime" CssClass="lblReservation"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="TitleInnerPage">
                    Estimated Duration:
                </td>
                <td class="TitleInnerPage">
                    <asp:Label runat="server" ID="lblDuration" CssClass="lblReservation"></asp:Label>
                </td>
                <td class="TitleInnerPage">
                    Passenger:
                </td>
                <td class="TitleInnerPage">
                    <asp:Label runat="server" ID="lblNOPassenger" CssClass="lblReservation"></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
