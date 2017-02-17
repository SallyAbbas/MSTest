<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageAroundCities.ascx.cs"
    Inherits="Lemo.Admin.Controls.ManageAroundCities" %>
<script type="text/javascript" src="../../ui/jquery-1.8.3.js"></script>
<script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&libraries=places"
    type="text/jscript"></script>
<script type="text/javascript" language="javascript">
    var geocoder = new google.maps.Geocoder();
    var lat = "";
    var lng = "";
    var map;
    var cities = new Array();
    var infowindow;
    function getState(zipcode) {
        geocoder.geocode({ 'address': zipcode + "United States" }, function (result, status) {
            var state = "N/A";
            for (var component in result[0]['address_components']) {
                for (var i in result[0]['address_components'][component]['types']) {
                    if (result[0]['address_components'][component]['types'][i] == "administrative_area_level_1") {
                        state = result[0]['address_components'][component]['long_name'];
                        lat = result[0].geometry.location.lat();
                        lng = result[0].geometry.location.lng();
                        var pyrmont = new google.maps.LatLng(lat, lng);
                        map = new google.maps.Map(document.getElementById('map'), {
                            mapTypeId: google.maps.MapTypeId.ROADMAP,
                            center: pyrmont,
                            zoom: 15
                        });
                        var request = {
                            location: pyrmont,
                            radius: $('#ddlDistance').val(),          //91286, //32186,         //1609.3 * 20,
                            keyword: "starbucks"
                        };
                        infowindow = new google.maps.InfoWindow();
                        var service = new google.maps.places.PlacesService(map);
                        service.radarSearch(request, callback);
                    }
                }
            }
        });
        return;
    }

    function initialize() {
        getState(ZipCodeValue);
//        getState("07306");
    }

    function callback(results, status, pagination) {
        var x = "";
        if (status == google.maps.places.PlacesServiceStatus.OK) {
            for (var i = 0; i < results.length; i++) {
                lat = results[i].geometry.location.lat(); lng = results[i].geometry.location.lng();
                
                //jb
                if(typeof results[i].geometry.location.hb === "undifined" && results[i].geometry.location.hb != null) {
                    var lattt = results[i].geometry.location.hb.toString().substring(0, 5);
                    var temp = lattt + "," + results[i].geometry.location.ib.toString().substring(0, 5);
                }
                else
                {
                    var lattt = results[i].geometry.location.jb.toString().substring(0, 5);
                    var temp = results[i].geometry.location.ib.toString().substring(0, 5) + "," + lattt;
                }

                if (x.indexOf(temp) == -1)
                    x = x + temp + "&";
            }
//            alert("AA  " + x);
            $("#hfLatLon").val(x);
            <%= this.Page.GetPostBackEventReference(this,"GetCities")%>; 
        }
    }
    $(function () {
        $('#ddlDistance').live('change', function (e) {
            initialize();
        });
    });
    
</script>
<div id="map">
</div>
<div id="divMCitiesContent" style="padding: 5px; width: 90%;">
<div runat="server" id="divConfirmation" visible="false" style="font-weight: bold;
                        font-size: 30px; color: Green;">
                        Your cities have been saved.
                    </div>
  <div runat="server" id="divError" visible="false" style="font-weight: bold;
                        font-size: 30px; color: red;">
                        There are problem with your information.
                    </div>
    <fieldset>
        <legend>        
            <asp:Label runat="server" ID="lblTitle" Text="Selct Service Area" CssClass="TitleInnerPage"></asp:Label></legend>
        <asp:HiddenField ID="hfLatLon" runat="server" ClientIDMode="Static" />
        <asp:DropDownList ID="ddlDistance" runat="server" OnSelectedIndexChanged="ddlDistance_SelectedIndexChanged"
            ClientIDMode="Static">
            <asp:ListItem Value="0">Set the radius of your service area</asp:ListItem>
            <asp:ListItem Value="16093.44">10</asp:ListItem>
            <asp:ListItem Value="32186.88">20</asp:ListItem>
            <asp:ListItem Value="48280.32">30</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:RequiredFieldValidator ControlToValidate="ddlDistance" ErrorMessage="*" InitialValue="0"
            ID="RequiredFieldValidator2" ValidationGroup="vgCalculatePriseSelectDistance"
            runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" AllowPaging="true" PageSize="10"
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            GridLines="Vertical" onpageindexchanging="GridView1_PageIndexChanging">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField HeaderText="State">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblStateName" Text='<%# Eval("StateName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="City">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCityName" Text='<%# Eval("CityName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hfID" Value='<%# Eval("CompanyCityPriceID") %>' />
                        <asp:TextBox runat="server" ID="txtPrice" Text='<%# Bind("Price") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegExpVal" runat="server" ControlToValidate="txtPrice" ForeColor="Red"
                            ValidationExpression="^(?!0$|0\d)\d{1,3}(\.\d{1,2})?$|^$" ErrorMessage="Enter a valid Fee"
                            ValidationGroup="vgLogin">*</asp:RegularExpressionValidator>
                        <%--<asp:CompareValidator ID="val_cmp_Fee" runat="server" ErrorMessage="Fee can not be greater than 999.99"
                            ValueToCompare="999.99" ControlToValidate="txtPrice" Operator="LessThanEqual"
                            Type="Double" ValidationGroup="VGPlan">*</asp:CompareValidator>--%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
        <br />
        <div style="text-align: right;">
            <asp:ImageButton runat="server" ID="butLogin" ValidationGroup="vgLogin" ImageUrl="~/Image/Save.png"
                OnClick="butLogin_Click" />
        </div>
    </fieldset>
</div>
