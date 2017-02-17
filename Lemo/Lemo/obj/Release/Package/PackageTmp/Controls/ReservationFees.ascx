<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReservationFees.ascx.cs"
    Inherits="Lemo.Controls.ReservationFees" %>
<script language="javascript" type="text/javascript">
    $(function () {
        $(".butContinue").css('background-color', '#FEA716');
        $(".ResCarType").click(function () {
            $(this).addClass('selected').siblings().removeClass('selected');
            $(".selected input[type='radio']").prop('checked', true);
            $(".butContinue").addClass('Hidden').removeClass('SelectedCar');
            $(".selected .butContinue").removeClass('Hidden').addClass('SelectedCar');
        });
        //////        $(".butContinue").click(function () {
        //////            var carID = $(".SelectedCar").attr("carID");
        //////            $("#hfCarID").val(carID);
        //////            __doPostBack('<%=btnNew.UniqueID %>', '');
        //////        });
    });

    //    function butContino_Click() {
    //        var carID = $(".SelectedCar").attr("carID");
    //        $("#hfCarID").val(carID);
    //        <%= this.Page.GetPostBackEventReference(this,"ReservationFeesCarID")%>;
    //        return false;
    //    }
</script>
<%--<div runat="server" id="divMain" clientidmode="Static" style="padding: 10px; clear: both; padding-left: 1%; padding-right: 1%;"></div>--%>
<%--<asp:HiddenField runat="server" ID="hfCarID" ClientIDMode="Static" />--%>
<asp:Button runat="server" Style="display: none" ID="btnNew" Text="Submit" OnClick="butContino_Click" />
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="10" HeaderStyle-Height="0px"
    AllowPaging="True" CellPadding="4" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging"
    ForeColor="#333333">
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
                <asp:Image runat="server" ID="imgPhotoName" ImageUrl='<%# Eval("PhotoName") %>' width='210' height='80' />
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
                <asp:Button Text="Continue" runat="server" ID="butEdit" CommandName='<%# Eval("CarID") %>'
                    class='butContinue Hidden' OnClick="butEdit_onClick123" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EditRowStyle BackColor="#999999" />
    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#E9E7E2" />
    <SortedAscendingHeaderStyle BackColor="#506C8C" />
    <SortedDescendingCellStyle BackColor="#FFFDF8" />
    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
</asp:GridView>
