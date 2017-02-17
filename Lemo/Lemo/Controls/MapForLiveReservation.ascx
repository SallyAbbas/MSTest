<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MapForLiveReservation.ascx.cs"
    Inherits="Lemo.Controls.MapForLiveReservation" %>
<script src="../Scripts/LimoScript.js" type="text/javascript"></script>
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?&sensor=false">
</script>
<script type="text/javascript">
var driverLength = 0;
var markersArray = [];
    Run(function () {
        google.maps.Map.prototype.clearMarkers = function() {
            for (var i = 0; i < this.markers.length; i++) {
                this.markers[i].setMap(null);
            }
            this.markers = new Array();
        };
    });
    function initialize() {
        var mapOptions = {
            center: new google.maps.LatLng(38, -97),
            zoom: 12,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("map-canvas"),
            mapOptions);
        var driverLocation = '<%= DriverLocationLatLan %>';
        var driverLocationList = driverLocation.split(',');
        driverLength = driverLocationList.length;
        for (var i = 0; i < driverLocationList.length; i++) {
            var latLng = driverLocationList[i].split('_');
            var lat = latLng[0];
            var lng = latLng[1];
            var imagePath = latLng[2];
            var carID = latLng[3];
            var isAvailable = latLng[4];
            var operatorBy = latLng[5];
            var carNumber = latLng[6];
            AddMarkerandInfoWindow_<%= this.ClientID %> (map,lat, lng, imagePath, carID, isAvailable,operatorBy,carNumber);
        }
         var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ address: '<%= FromAddress %>' }, function(results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                map.setCenter(results[0].geometry.location);
            }
        });
        window.setInterval(function() {
            UpdateMarker(map);
        }, 11000);
    }
    google.maps.event.addDomListener(window, 'load', initialize);
    
      
        function AddMarkerandInfoWindow_<%= this.ClientID %>(map,lat, lng, imagePath, carID, isAvailable,operatorBy,carNumber)
        {
             //loop for all driver  and driver status images
            //        var image = '../Image/logo.png';
            var url = "";
            var carLink = "";
//            var type = "";
            if (isAvailable == "false") {
                url = '../Image/OrangeCar.png';
                carLink = "Busy Now";
            }
            else {
                url = '../Image/BlueCar.png';
                carLink = " <a href='/Pages/CompleteReservation.aspx?id=" + carID + "'>Book Now</a>";
            }
            var image = {
                url: url
                // This marker is 20 pixels wide by 32 pixels tall.
                //,size: new google.maps.Size(40, 20)
            };
            var marker_<%=this.ClientID %> = new google.maps.Marker({
                position: new google.maps.LatLng(lat, lng),
                map: map,
                icon: image,
                title: ""
            });   
             markersArray.push(marker_<%=this.ClientID %>);
            var infowindow_<%=this.ClientID %> = new google.maps.InfoWindow({
                content: "<table><tr><td><img src='" + imagePath + "' width='120' height='50' /></td><td>Car Number <br /><span style='font-size:16px;color:#F58D3A;'>" + carNumber + "</span></td></tr><tr><td>" + carLink + "</td><td>" + operatorBy + "</td></tr></table>"
            });

            google.maps.event.addListener(marker_<%=this.ClientID %>, 'click', function () {
                infowindow_<%=this.ClientID %>.open(map, marker_<%=this.ClientID %>);
            });
        }
        
        function UpdateMarker(map)
        {
            var driverLocation = '';
            for (var ij = 0; ij < markersArray.length; ij++) {
                markersArray[ij].setMap(null);
            }
            markersArray = [];
            var urlHandler = '<%= VirtualPathUtility.ToAbsolute("~/Handlers/MapMarker.ashx") %>';
            $.ajax({
                    url: urlHandler + "?NOPassenger=" + '<%= NOPassenger %>',
                    success: function(result) {
                        driverLocation = result;
                        UpdateMarkerWithNewPosstion(driverLocation,map);
                    },
                    error: function(errormsg) {
                        
                    }
                });
        }
        
        function UpdateMarkerWithNewPosstion(driverLocation,map) {
             var driverLocationList = driverLocation.split(',');
            driverLength = driverLocationList.length;
            
            for (var i = 0; i < driverLocationList.length; i++) {
                var latLng = driverLocationList[i].split('_');
                var lat = latLng[0];
                var lng = latLng[1];
                var imagePath = latLng[2];
                var carID = latLng[3];
                var isAvailable = latLng[4];
                var operatorBy = latLng[5];
                var carNumber = latLng[6];
                AddMarkerandInfoWindow_<%= this.ClientID %> ( map, lat, lng, imagePath, carID, isAvailable, operatorBy, carNumber);
            }
        }
</script>
<div id="map-canvas" style="width: 100%; height: 100%;" />