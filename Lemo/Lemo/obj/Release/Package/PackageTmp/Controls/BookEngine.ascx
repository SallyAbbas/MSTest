<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookEngine.ascx.cs"
    Inherits="Lemo.Control.BookEngine" %>
<script type="text/javascript" src="../UI/jquery.ui.core.js"></script>
<script type="text/javascript" src="../UI/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../UI/jquery.ui.datepicker.js"></script>
<script src="../Scripts/jquery.watermark.min.js" type="text/javascript"></script>
<script src="http://maps.google.com/maps/api/js?sensor=false&language=" type="text/javascript"></script>
<script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&libraries=places" type="text/javascript"></script>
<script src="../Scripts/ab-get-distance.js" type="text/javascript"></script>

<link href="http://code.google.com/apis/maps/documentation/javascript/examples/default.css"
    rel="stylesheet" type="text/css" />
<link href="../UI/demos.css" rel="stylesheet" type="text/css" />
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

        $("#<%= imgbutCalculatePrise.ClientID %>").click(function (ev) {
            get_distance();
            setTimeout(func, 1000);
            return false;
        });
    });

    function get_distance() {
    //NEW YORK, NY, USA 
        from = $("#<%= txtSource.ClientID %>").val();
        to = $("#<%= txtDistenation.ClientID %>").val();
        calcRoute(from, to);
    }

    function func() {           
            $("#hfDistance").val(ab_get_distance_distance);
            $("#hfDuration").val(ab_get_distance_duration);                              
             <%= this.Page.GetPostBackEventReference(this,"LimoBookEngine")%>;           
    }   
    </script>
<div class="EnginUI">
    <table style="padding: 5px;">
        <tr>
            <td class="EngineTitle" colspan="3">
                Book your ride now
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
        <tr>
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
                </asp:DropDownList>                
            </td>
        </tr>
        <tr>
            <td colspan="3" class="paddingEngin">
                <br>
                <asp:ImageButton runat="server" ID="imgbutCalculatePrise" ImageUrl="~/Image/yellowbtn.png"
                    ValidationGroup="vgCalculatePrise" OnClick="imgbutCalculatePrise_Click" />
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField runat="server" ID="hfDistance" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hfDuration" ClientIDMode="Static" />
