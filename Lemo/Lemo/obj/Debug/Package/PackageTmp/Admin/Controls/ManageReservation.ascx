<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageReservation.ascx.cs"
    Inherits="Lemo.Admin.Controls.ManageReservation" %>
<%--<script type="text/javascript" src="../../ui/jquery-1.8.3.js"></script>
<style>
    .tooltip {
    background-color: #ECECEC;
    border: 1px dotted #226699;
    color: #333333;    
    opacity: 1;
    padding: 0;
    position: relative;    
    width: 300px;
    z-index: 3000;
}
</style>
    <script type="text/javascript" language="javascript">
        function showTooltip(divName, divToolTipName) {
            var pTop = $(divName).offset().top;
            var pLeft = $(divName).offset().left;
           
            var tooltip = $("<div id='" + divToolTipName + "' class='tooltip' style='cursor: pointer; z-index: 5000000;'>I'm the tooltip!</div>");
            tooltip.appendTo($(divName)); $("#divToolTipName").css({
                top: pTop,
                left: pLeft
            }).fadeIn();
        }

        function hideTooltip(divToolTipName) {
//            clearTimeout(tooltipTimeout);
            $(divToolTipName).fadeOut().remove();
        }
    </script>--%>
<script type="text/javascript" src="../../ui/jquery-1.8.3.js"></script>
<script type="text/javascript" src="../../UI/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../UI/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../UI/jquery.ui.datepicker.js"></script>
<link href="../../UI/demos.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">
    $(function () {
        $("#<%= txtDatepicker.ClientID %>").datepicker({
            changeMonth: true,
            changeYear: true
        });
    });
</script>
<div style="text-align: center; padding: 10px; width: 100%;">
    <table width="100%">
        <tr>
            <td>
            <asp:Label runat="server" ID="lblSelectedDate" Text="Selected Date"></asp:Label>
                <asp:TextBox ID="txtDatepicker" runat="server" Width="80" AutoPostBack="true"
                    ontextchanged="txtDatepicker_TextChanged"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ControlToValidate="txtDatepicker" ErrorMessage="*" ID="RequiredFieldValidator5"
                        ValidationGroup="vgCalculatePrise" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td align="center">
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow"
                    OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="True" BorderColor="Tan"
                    BorderWidth="1px" CellPadding="2" GridLines="None" OnRowDataBound="GridView1_RowDataBound"
                    ForeColor="Black">
                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    <Columns>
                        <asp:TemplateField HeaderText="Action">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <asp:Button Text="Accept" runat="server" ID="butAccept" CommandName='<%# Eval("JobID") %>'
                                    OnClick="butAccept_onClick" />
                                <asp:Button Text="Reject" runat="server" ID="butReject" CommandName='<%# Eval("JobID") %>'
                                    OnClick="butReject_onClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <asp:Button Text="Edit" runat="server" ID="butEdit" CommandName='<%# Eval("JobID") %>'
                                    OnClick="butEdit_onClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Limo Conf">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <%--<asp:Label runat="server" ID="lblLimoConfirmNumber" Text='<%# Eval("LimoConfirmNumber") %>'></asp:Label>--%>
                                <%# Eval("LimoConfirmNumber") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Affiliate Conf">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <%--<asp:Label runat="server" ID="lblComapnyConfirmNum" Text='<%# Eval("ComapnyConfirmNum") %>'></asp:Label>--%>
                                <%# Eval("ComapnyConfirmNum") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date/Time">
                            <ItemStyle Width="75px" />
                            <ItemTemplate>
                                <%--<asp:Label runat="server" ID="lblJobDate" Text='<%# Eval("JobDate") %>'></asp:Label>
                                    <br />
                                    <asp:Label runat="server" ID="lblJobTime" Text='<%# Eval("JobTime") %>'></asp:Label>--%>
                                <%# Eval("JobDate") %>
                                <br />
                                <%# Eval("JobTime") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemStyle Width="75px" />
                            <ItemTemplate>
                                <%--<asp:Label runat="server" ID="lblStatus" Text='<%# Eval("Status") %>'></asp:Label>  --%>
                                <%# Eval("Status") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Passenger">
                            <ItemStyle Width="150px" />
                            <ItemTemplate>
                                <%--<div title='<%# Eval("PassengerPhone") %>' runat="server" id="divPassengerEmail">--%>
                                <%--<asp:Label runat="server" ID="lblPassengerName" Text='<%# Eval("PassengerFullName") %>'></asp:Label><br />
                                        <asp:Label runat="server" ID="lblPassengerPhone" Text='<%# Eval("PassengerEmail") %>'></asp:Label>--%>
                                <%# Eval("PassengerFullName") %>
                                <%-- <br />
                                        <%# Eval("PassengerEmail") %>--%>
                                <%-- </div>--%>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="PU Location">
                            <ItemStyle Width="250px" />
                            <ItemTemplate>
                                <%# Eval("FromAddress")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DO Location">
                            <ItemStyle Width="250px" />
                            <ItemTemplate>
                                <%# Eval("ToAddress")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Driver">
                            <ItemStyle Width="150px" />
                            <ItemTemplate>
                                <%--<div title='<%# Eval("DriverPhone") %>' runat="server" id="divDriverEmail">--%>
                                <%--<asp:Label runat="server" ID="lblDriverName" Text='<%# Eval("DriverFullName") %>'></asp:Label><br />
                                    <asp:Label runat="server" ID="lblDriverPhone" Text='<%# Eval("DriverEmail") %>'></asp:Label>--%>
                                <%# Eval("DriverFullName") %>
                                <%-- <br />
                                        <%# Eval("DriverEmail") %>--%>
                                <%--</div>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Affiliate Name">
                            <ItemStyle Width="150px" />
                            <ItemTemplate>
                                <%-- <div id="divCompany" onmouseover="showTooltip('#divCompany','companyTooltib');" onmouseout="hideTooltip('#companyTooltib');">--%>
                                <%--<asp:Label runat="server" ID="lblCompanyName" Text='<%# Eval("CompanyName") %>'></asp:Label><br />'<%# Eval("CompanyPhone") %>'
                                    <asp:Label runat="server" ID="lblCompanyPhone" Text='<%# Eval("CompanyEmail") %>'></asp:Label>--%>
                                <%# Eval("CompanyName") %>
                                <%-- <br />
                                        <%# Eval("CompanyEmail") %>--%>
                                <%--</div>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Price">
                            <ItemStyle Width="75px" />
                            <ItemTemplate>
                                <%--<asp:Label runat="server" ID="lblTotalPrice" Text='<%# Eval("TotalPrice") %>'></asp:Label>--%>
                                <%# Eval("TotalPrice") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="Tan" />
                    <HeaderStyle BackColor="Tan" Font-Bold="True" />
                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
