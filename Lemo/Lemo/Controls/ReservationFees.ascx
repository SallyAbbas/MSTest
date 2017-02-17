<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReservationFees.ascx.cs"
    Inherits="Lemo.Controls.ReservationFees" %>
<%@ Register Src="~/Controls/BookEngine.ascx" TagName="BookEngine" TagPrefix="uc1" %>
<script language="javascript" type="text/javascript">
    $(function () {
        $(".butContinue").css('background-color', '#0085CC');
        $(".butContinue").css('color', '#ffffff');
        $(".ResCarType").click(function () {
            $(this).addClass('selected').siblings().removeClass('selected');
            $(".selected input[type='radio']").prop('checked', true);
            $(".butContinue").removeClass('SelectedCar');
            $(".selected .butContinue").removeClass('Hidden').addClass('SelectedCar');
        });
    });
</script>
<style>
    .ResCarType
    {
        padding-right: 12px;
        padding-left: 13px;
        border-color: #0085CC;
        border-style: solid;
    }
</style>
<%--<div runat="server" id="divMain" clientidmode="Static" style="padding: 10px; clear: both; padding-left: 1%; padding-right: 1%;"></div>--%>
<%--<asp:HiddenField runat="server" ID="hfCarID" ClientIDMode="Static" />--%>
<asp:Button runat="server" Style="display: none" ID="btnNew" Text="Submit" OnClick="butContino_Click" />
<table width="100%" style="padding-top: 10px;">
    <tr>
        <td valign="top">
            <uc1:BookEngine ID="BookEngine1" runat="server" />
        </td>
        <td valign="top" align="center">
            <div style="text-align: center; font-size: 32px; font-family: Arial; font-weight: bold;
                color: #0085CC">
                Please select your car</div>
            <div style="overflow-x: auto; width: 960px;">
                <span runat="server" id="spanTopCompainies" style="text-align: center; font-size: 32px;
                    font-family: Arial; font-weight: bold; color: #0085CC">Top Companies</span>
                <div>
                    <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" Width="100%"
                        BorderStyle="Solid" BorderColor="#0085CC">
                        <ItemStyle CssClass="ResCarType" />
                        <ItemTemplate>
                            <%-- <div>--%>
                            <div class='TitleInnerPage'>
                                <input id='Radio1" + car.CarID + "' type='radio' name='rdSelectCar' />
                                <asp:Label runat="server" ID="lblName" Text='<%# Eval("CarName") %>'></asp:Label></div>
                            <div>
                                <asp:Image runat="server" ID="imgNoPassenger" ImageUrl='<%# "../Image/Passenger/" + Eval("NoPassengers") + ".png"%>'
                                    class='features' />
                                <img src='../Image/Passenger/Passenger.png' class='features'>
                                <img src='../Image/Passenger/Bag.png' class='features'></div>
                            <div class='Description' runat="server" id="divDescription">
                                <%# Eval("Description")%>
                            </div>
                            <asp:Image runat="server" ID="imgPhotoName" ImageUrl='<%# Eval("PhotoName") %>' Width='210'
                                Height='80' />
                            <div class='Price' id="divPrice" runat="server">
                                <%# "$" + Eval("TotalPrice")%>
                            </div>
                            <div class='Description' id="divOperator" runat="server">
                                <%# "Operator by " + Eval("CompanyName")%>
                            </div>
                            <asp:Button Text="Book" Width="100px" Font-Bold="true" Font-Size="Medium" runat="server"
                                ID="butEdit" CommandName='<%# Eval("CarID") %>' class='butContinue' OnClick="butEdit_onClick123" />
                            <%--</div>--%>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="10"
                    HeaderStyle-Height="0px" AllowPaging="True" CellPadding="4" GridLines="None"
                    OnPageIndexChanging="GridView1_PageIndexChanging" ForeColor="#333333">
                    <%--<AlternatingRowStyle BackColor="White" ForeColor="#284775" />--%>
                    <RowStyle CssClass="ResCarType" />
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderStyle CssClass="TitleInnerPage Description" ForeColor="White" Width="25%" />
                            <ItemStyle Width="25%" />
                            <ItemTemplate>
                                <div class='TitleInnerPage'>
                                    <input id='Radio1" + car.CarID + "' type='radio' name='rdSelectCar' />
                                    <asp:Label runat="server" ID="lblName" Text='<%# Eval("CarName") %>'></asp:Label></div>
                                <div>
                                    <asp:Image runat="server" ID="imgNoPassenger" ImageUrl='<%# "../Image/Passenger/" + Eval("NoPassengers") + ".png"%>'
                                        class='features' />
                                    <img src='../Image/Passenger/Passenger.png' class='features'>
                                    <img src='../Image/Passenger/Bag.png' class='features'></div>
                                <div class='Description' runat="server" id="divDescription">
                                    <%# Eval("Description")%>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <HeaderStyle CssClass="TitleInnerPage Description" ForeColor="White" Width="210px" />
                            <ItemStyle Width="210px" />
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgPhotoName" ImageUrl='<%# Eval("PhotoName") %>' Width='210'
                                    Height='80' />
                                <%--<asp:Image runat="server" ID="imgPhotoName" ImageUrl='http://limoallover.net/AppImages/company/cars/SEDANLX (1).png'
                    Width='210' Height='80' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <HeaderStyle CssClass="TitleInnerPage Description" ForeColor="White" Width="25%" />
                            <ItemStyle Width="25%" />
                            <ItemTemplate>
                                <%--<img src='<%= Eval("PhotoName") %>' width='210px' height='80px' /> --%>
                                <div class='Price' id="divPrice" runat="server">
                                    <%# "$" + Eval("TotalPrice")%>
                                </div>
                                <div class='Description' id="divOperator" runat="server">
                                    <%# "Operator by " + Eval("CompanyName")%>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <HeaderStyle CssClass="TitleInnerPage Description" ForeColor="White" />
                            <ItemTemplate>
                                <asp:Button Text="Book" Width="100px" Font-Bold="true" Font-Size="Medium" runat="server"
                                    ID="butEdit" CommandName='<%# Eval("CarID") %>' class='butContinue' OnClick="butEdit_onClick123" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#0085CC" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#0085CC" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#0085CC" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </td>
    </tr>
</table>
