<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="JobDetails.aspx.cs" Inherits="Lemo.Pages.JobDetails" %>

<%@ Register Src="../Controls/ReservationFees.ascx" TagName="ReservationFees" TagPrefix="uc1" %>
<%@ Register Src="../Controls/ResevationInformation.ascx" TagName="ResevationInformation"
    TagPrefix="uc2" %>
<%@ Register Src="~/Controls/BookEngine.ascx" TagName="BookEngine" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script language="javascript" type="text/javascript">
    $(function () {
        $(".BookEngintxtSourceDistenation").width(200);
        $(".EngineTitle").css("font-size", "20px");
        $(".EnginUI").width(260);
        $(".paddingEngin").css("padding", "0px");
        $(".paddingEngin").css("text-align", "left");
    });
</script>
    <div>
        <uc2:ResevationInformation ID="ResevationInformation1" runat="server" />
        <table width="100%">
            <tr>
                <td valign="top">
                    <uc1:BookEngine ID="BookEngine1" runat="server" />
                </td>
                <td valign="top" align="center">
                    <span style="text-align: center; font-size: 32px; font-family: Arial; font-weight: bold;
                        color: #0078BC">Please select your car</span>
                    <uc1:ReservationFees ID="ReservationFees1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
