<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookEngine.ascx.cs"
    Inherits="Lemo.Control.BookEngine" %>
<script type="text/javascript" src="/UI/jquery.ui.core.js"></script>
<script type="text/javascript" src="/UI/jquery.ui.widget.js"></script>
<script type="text/javascript" src="/UI/jquery.ui.datepicker.js"></script>
<script src="/Scripts/jquery.watermark.min.js" type="text/javascript"></script>
<%--<script src="http://maps.google.com/maps/api/js?sensor=false&language=" type="text/javascript"></script>--%>
<script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&libraries=places"
    type="text/javascript"></script>
<script src="/Scripts/ab-get-distance.js" type="text/javascript"></script>
<%--<link href="http://code.google.com/apis/maps/documentation/javascript/examples/default.css"
    rel="stylesheet" type="text/css" />--%>
<link href="/UI/demos.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">

    function initialize() {
        var input = document.getElementById("<%= txtSource.ClientID %>");
        var autocomplete = new google.maps.places.Autocomplete(input);

        //For source
        google.maps.event.addListener(autocomplete, 'place_changed', function () {
            infowindow.close();
            marker.setVisible(false);
            input.className = '';
            var place = autocomplete.getPlace();
            if (!place.geometry) {
                // Inform the user that the place was not found and return.                
                return;
            }

            var address = '';
            if (place.address_components) {
                address = [
              (place.address_components[0] && place.address_components[0].short_name || ''),
              (place.address_components[1] && place.address_components[1].short_name || ''),
              (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
            }

            infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);

        });

        var inputDest = document.getElementById("<%= txtDistenation.ClientID %>");
        var autocompleteDest = new google.maps.places.Autocomplete(inputDest);
        //for distination
        google.maps.event.addListener(autocompleteDest, 'place_changed', function () {
            infowindow.close();
            marker.setVisible(false);
            inputDest.className = '';
            var place = autocompleteDest.getPlace();
            if (!place.geometry) {
                // Inform the user that the place was not found and return.                
                return;
            }

            var address = '';
            if (place.address_components) {
                address = [
              (place.address_components[0] && place.address_components[0].short_name || ''),
              (place.address_components[1] && place.address_components[1].short_name || ''),
              (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
            }

            infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);

        });
    }
    google.maps.event.addDomListener(window, 'load', initialize);



    $(function () {
        var watermarkSource = 'Pickup Address, City or Zip code';

        var watermarkDest = 'Where Do you want to go ?';
        //init, set watermark text and class
        $("#<%= txtSource.ClientID %>").watermark(watermarkSource, { className: 'WatermarkedText' });

        //init, set watermark text and class
        $("#<%= txtDistenation.ClientID %>").watermark(watermarkDest, { className: 'WatermarkedText' });

        $("#<%= txtDatepicker.ClientID %>").datepicker({
            changeMonth: true,
            changeYear: true,
            minDate: -0
        });

        $("#<%= butCalc.ClientID %>").click(function (ev) {
            get_distance();
            setTimeout(func, 1000);
            return false;
        });

        $("#<%= butAddDespatch.ClientID %>").click(function (ev) {
            get_distance();
            setTimeout(func, 1000);
            return false;
        });

        $("#<%= btnSubmit.ClientID %>").css("display", "none");

        $("#<%= butAddDespatch.ClientID %>").click(function (ev) {
            getLatAndLng($("#<%= txtSource.ClientID %>").val());
            getLatAndLngTo($("#<%= txtDistenation.ClientID %>").val());
            setTimeout(funcbutAddDespatch, 1000);
            return false;
        });

        $("#<%= butCalc.ClientID %>").click(function (ev) {
            getLatAndLng($("#<%= txtSource.ClientID %>").val());
            getLatAndLngTo($("#<%= txtDistenation.ClientID %>").val());
            setTimeout(funcbutCalc, 1000);
            return false;
        });
    });
    var latLang = "";
    var latLangTo = "";
    function funcbutAddDespatch() {
        $("#hfLAtLng").val(latLang);
        $("#hfLAtLngTo").val(latLangTo);
        $("#<%= btnSubmit.ClientID %>").click();
    }

    function funcbutCalc() {
        $("#hfLAtLng").val(latLang);
        $("#hfLAtLngTo").val(latLangTo);
        $("#<%= btnSubmit.ClientID %>").click();
    }

    function get_distance() {
        //NEW YORK, NY, USA 
        from = $("#<%= txtSource.ClientID %>").val();
        to = $("#<%= txtDistenation.ClientID %>").val();
        calcRoute(from, to);
    }

    function func() {
        $("#hfDistance").val(ab_get_distance_distance);
        $("#hfDuration").val(ab_get_distance_duration);
        $("#<%= btnSubmit.ClientID %>").click();
    }

    $(function () {
        //$(".trDateTime").hide();
        $(".ReservationType").click(function () {
            if ($(this).attr("value") != "Ride Now") {
                $("#<%= hfBookType.ClientID %>").val("RideLater");
                $(".trDateTime").show();
            }
            else {
                $("#<%= hfBookType.ClientID %>").val("RideNow");
                $(".trDateTime").hide();
            }
        });
    });

    function getLatAndLng(addressFrom) {
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': addressFrom }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                latLang = results[0].geometry.location.lat() + "," + results[0].geometry.location.lng();
            } else {

            }
        });
    }
    function getLatAndLngTo(addressTo) {
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': addressTo }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                latLangTo = results[0].geometry.location.lat() + "," + results[0].geometry.location.lng();
            } else {

            }
        });
    }
</script>
<style>
    .EngineTitle, .EnginLable
    {
        color: #0085CC !important;
    }
    .engineFont
    {
        background-color: #0085CC;
        width: 130px;
        height: 30px;
        color: White !important;
        font-size: 24px;
        font-weight: bold;
    }
</style>
<div class="EnginUI">
    <table style="padding: 5px;">
        <tr id="trResrvationType" runat="server">
            <td class="EngineTitle" colspan="3" style="font-size: 28px;">
                <input type="button" value="Ride Later" class="ReservationType engineFont" title="Ride Later" />
                <input type="button" style="" value="Ride Now" class="ReservationType engineFont"
                    title="Ride Now" />
                <asp:HiddenField ID="hfBookType" runat="server" Value="RideLater" />
                <%--<asp:RadioButton ID="rbReservationLater" CssClass="ReservationType" runat="server" GroupName="ReservationType" Text="Ride Later" Checked="true" />
             <asp:RadioButton ID="rbLiveReservation" CssClass="ReservationType"  runat="server" GroupName="ReservationType" Text="Ride Now" ForeColor="#F58D3A" />--%>
            </td>
        </tr>
        <tr>
            <td class="EngineTitle" colspan="3" style="font-size: 40px;">
                Book Your Ride
            </td>
        </tr>
        <tr>
            <td colspan="3" class="paddingEngin EnginLable">
                I want to be picked up at:
                <br />
                <asp:TextBox runat="server" ID="txtSource" Width="310" Height="30" CssClass="BookEngintxtSourceDistenation"></asp:TextBox><asp:RequiredFieldValidator
                    ControlToValidate="txtSource" ErrorMessage="*" ID="RequiredFieldValidator6" ValidationGroup="vgCalculatePrise"
                    runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="paddingEngin EnginLable">
                ...and dropped off at:
                <br />
                <asp:TextBox runat="server" ID="txtDistenation" Width="310" Height="30" CssClass="BookEngintxtSourceDistenation"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtDistenation" ErrorMessage="*" ID="RequiredFieldValidator4"
                    ValidationGroup="vgCalculatePrise" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="trDateTime">
            <td class="paddingEngin">
                <br />
                <asp:Label runat="server" ID="lblSelectDate" Text="Select Date" CssClass="EnginLable"></asp:Label>
                <br />
                <asp:TextBox ID="txtDatepicker" runat="server" Width="80"></asp:TextBox><asp:RequiredFieldValidator
                    ControlToValidate="txtDatepicker" ErrorMessage="*" ID="RequiredFieldValidator5"
                    ValidationGroup="vgCalculatePrise" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td colspan="2" class="paddingEngin EnginLable">
                <br />
                <asp:Label runat="server" ID="lblPickUpTime" Text="Pick up Time" CssClass="EnginLable"></asp:Label>
                <br />
                <asp:DropDownList runat="server" ID="ddlHour">
                    <asp:ListItem Text="Hour" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="12 AM" Value="0"></asp:ListItem>
                    <asp:ListItem Text="01 AM" Value="1"></asp:ListItem>
                    <asp:ListItem Text="02 AM" Value="2"></asp:ListItem>
                    <asp:ListItem Text="03 AM" Value="3"></asp:ListItem>
                    <asp:ListItem Text="04 AM" Value="4"></asp:ListItem>
                    <asp:ListItem Text="05 AM" Value="5"></asp:ListItem>
                    <asp:ListItem Text="06 AM" Value="6"></asp:ListItem>
                    <asp:ListItem Text="07 AM" Value="7"></asp:ListItem>
                    <asp:ListItem Text="08 AM" Value="8"></asp:ListItem>
                    <asp:ListItem Text="09 AM" Value="9"></asp:ListItem>
                    <asp:ListItem Text="10 AM" Value="10"></asp:ListItem>
                    <asp:ListItem Text="11 AM" Value="11"></asp:ListItem>
                    <asp:ListItem Text="Noon" Value="12"></asp:ListItem>
                    <asp:ListItem Text="01 PM" Value="13"></asp:ListItem>
                    <asp:ListItem Text="02 PM" Value="14"></asp:ListItem>
                    <asp:ListItem Text="03 PM" Value="15"></asp:ListItem>
                    <asp:ListItem Text="04 PM" Value="16"></asp:ListItem>
                    <asp:ListItem Text="05 PM" Value="17"></asp:ListItem>
                    <asp:ListItem Text="06 PM" Value="18"></asp:ListItem>
                    <asp:ListItem Text="07 PM" Value="19"></asp:ListItem>
                    <asp:ListItem Text="08 PM" Value="20"></asp:ListItem>
                    <asp:ListItem Text="09 PM" Value="21"></asp:ListItem>
                    <asp:ListItem Text="10 PM" Value="22"></asp:ListItem>
                    <asp:ListItem Text="11 PM" Value="23"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ControlToValidate="ddlHour" ErrorMessage="*" InitialValue="-1"
                    ID="RequiredFieldValidator2" ValidationGroup="vgCalculatePrise" runat="server"
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:DropDownList runat="server" ID="ddlMinutes">
                    <asp:ListItem Text="Minutes" Value="-1"></asp:ListItem>
                    <asp:ListItem Text=":00" Value="0"></asp:ListItem>
                    <asp:ListItem Text=":15" Value="1"></asp:ListItem>
                    <asp:ListItem Text=":30" Value="2"></asp:ListItem>
                    <asp:ListItem Text=":45" Value="3"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ControlToValidate="ddlMinutes" ErrorMessage="*" InitialValue="-1"
                    ID="RequiredFieldValidator1" ValidationGroup="vgCalculatePrise" runat="server"
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="paddingEngin EnginLable">
                <br />
                <asp:Label runat="server" ID="lblNumberofPassengers" Text="Number of passengers"
                    CssClass="EnginLable"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlNOPassenger">
                    <asp:ListItem Text="01" Value="1"></asp:ListItem>
                    <asp:ListItem Text="02" Value="2"></asp:ListItem>
                    <asp:ListItem Text="03" Value="3"></asp:ListItem>
                    <asp:ListItem Text="04" Value="4"></asp:ListItem>
                    <asp:ListItem Text="05" Value="5"></asp:ListItem>
                    <asp:ListItem Text="06" Value="6"></asp:ListItem>
                    <asp:ListItem Text="07" Value="7"></asp:ListItem>
                    <asp:ListItem Text="08" Value="8"></asp:ListItem>
                    <asp:ListItem Text="09" Value="9"></asp:ListItem>
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
        </tr>
        <tr runat="server" id="trPassengerInformation" visible="false">
            <td>
                <asp:Label runat="server" ID="lblFirstName" Text="Passenger First Name"></asp:Label>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                    ControlToValidate="txtFirstName" ErrorMessage="*" ID="RequiredFieldValidator3"
                    ValidationGroup="vgCalculatePrise" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <br />
                <asp:Label runat="server" ID="lblLastName" Text="Passenger Last Name"></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                    ControlToValidate="txtLastName" ErrorMessage="*" ID="RequiredFieldValidator7"
                    ValidationGroup="vgCalculatePrise" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <br />
                <asp:Label runat="server" ID="lblTotalPrice" Text="Total Price"></asp:Label>
                <asp:TextBox ID="txtTotalPrice" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                    ControlToValidate="txtTotalPrice" ErrorMessage="*" ID="RequiredFieldValidator8"
                    ValidationGroup="vgCalculatePrise" runat="server" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <br />
                <asp:Label runat="server" ID="Label1" Text="Mobile Phone"></asp:Label>
                <asp:TextBox runat="server" ID="txtPassMobile"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtPassMobile" ErrorMessage="Required"
                    ID="RequiredFieldValidator9" ValidationGroup="vgPayment" runat="server" ForeColor="Red"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <br />
                <asp:Label runat="server" ID="Label2" Text="E-mail"></asp:Label>
                <br />
                <asp:TextBox runat="server" ID="txtPassEmail"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtPassEmail" ErrorMessage="Required"
                    ID="RequiredFieldValidator10" ValidationGroup="vgPayment" runat="server" ForeColor="Red"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="txtPassEmail" ValidationExpression="^((?:(?:(?:[a-zA-Z0-9][\.\-\+_]?)*)[a-zA-Z0-9])+)\@((?:(?:(?:[a-zA-Z0-9][\.\-_]?){0,62})[a-zA-Z0-9])+)\.([a-zA-Z0-9]{2,6})$"
                    ForeColor="Red" ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid email address"
                    ValidationGroup="vgPayment" Display="Dynamic"></asp:RegularExpressionValidator>
                    <br />
                    <asp:Label runat="server" ID="lblNote" Text="Note"></asp:Label>
                <br />
                <asp:TextBox runat="server" ID="txtNote"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="paddingEngin">
                <br>
                <%--<asp:ImageButton runat="server" ID="imgbutCalculatePrise" ImageUrl="~/Image/yellowbtn.png"
                    ValidationGroup="vgCalculatePrise" OnClick="imgbutCalculatePrise_Click" />--%>
                <asp:Button runat="server" ID="butCalc" Text="Calculate Price" ValidationGroup="vgCalculatePrise"
                    CssClass="engineFont" Width="200px" />
                <asp:Button runat="server" ID="butAddDespatch" Text="Add Despatch" ValidationGroup="vgCalculatePrise"
                    CssClass="engineFont" Width="200px" Visible="false" OnClick="butAddDespatch_Click" />
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField runat="server" ID="hfDistance" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hfDuration" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hfLAtLng" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hfLAtLngTo" ClientIDMode="Static" />
<div style="display: none;">
    <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" CssClass="btnSubmit" /></div>
