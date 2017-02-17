// JavaScript Document
var directionDisplay;
var ab_get_distance_distance = "";
var ab_get_distance_duration = "";
  var directionsService = new google.maps.DirectionsService();
  var map;

  function initialize(lat,lng) {
    directionsDisplay = new google.maps.DirectionsRenderer();
    //var location = new google.maps.LatLng(9.93123, 76.26730);
	var location = new google.maps.LatLng(lat, lng);
    
    var zm =  parseInt(document.getElementById('map_zoom').value);

    var myOptions = {
 
      zoom: zm,
      mapTypeId: google.maps.MapTypeId.ROADMAP,
      center: location
    }
    
    map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
    directionsDisplay.setMap(map);

  }

  function calcRoute(from,to){
	var start = from;
    var end = to;
    var request = {
        origin:start,
        destination:end,
        travelMode: google.maps.DirectionsTravelMode.DRIVING,
	unitSystem: google.maps.DirectionsUnitSystem.METRIC
    };
    // function to round the decimal digits eg: round(123.456,2); gives 123.45
    function round(number,X) {
        X = (!X ? 2 : X);
        return Math.round(number*Math.pow(10,X))/Math.pow(10,X);
    }

    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            //directionsDisplay.setDirections(response);

            var distance = response.routes[0].legs[0].distance.text;
            var time_taken = response.routes[0].legs[0].duration.text;
            ab_get_distance_duration = time_taken;
            var calc_distance = response.routes[0].legs[0].distance.value;

            //to convert to KM to mile
            calc_distance = calc_distance / 1000;
            var ConverttoMile = 0.621371;
            calc_distance = calc_distance * ConverttoMile;
            calc_distance = round(calc_distance, 2);
            ab_get_distance_distance = calc_distance;
        }
        else {
            ab_get_distance_distance = "-1";
        }
    });
  }

//window.onload=function(){initialize();}