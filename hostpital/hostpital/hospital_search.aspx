<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hospital_search.aspx.cs" Inherits="hostpital.hospital_search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Hospital Search</title>
     <meta name="viewport" content="initial-scale=1.0, user-scalable=no">
    <meta charset="utf-8">
    <style>
 
      html, body{
        height: 100%;
        margin: 0px;
        padding: 0px
      }
      #map-canvas {
        height: 453px;
        padding: 0px;
       
       
        width: 446px;
      }

      table {
        font-size: 12px;
      }

      #listing {
        position: absolute;
        width: 200px;
        height: 470px;
        overflow: auto;
        left: 41%;
        top: 88px;
        cursor: pointer;
        overflow-x: hidden;
      }
      #findhospital {
        position: absolute;
        text-align: right;
        width: 100px;
        font-size: 14px;
        padding: 4px;
        z-index: 5;
        background-color: #fff;
        top: 94px;
                                                                  left: 5px;
                                                              }
      #locationField {
        position: absolute;
        width: 190px;
        height: 25px;
        left: 120px;
        top: 92px;
        z-index: 5;
        background-color: #fff;
      }
      #controls {
        position: absolute;
        left: 323px;
        width: 140px;
        top: 94px;
        z-index: 5;
        background-color: #fff;
      }
      #autocomplete {
        width: 100%;
        top: 250px;
      }
      #country {
        width: 100%;
      }
      .placeIcon {
        width: 20px;
        height: 34px;
        margin: 4px;
      }
      .hospitalIcon {
        width: 24px;
        height: 24px;
      }
      #resultsTable {
        border-collapse: collapse;
        width: 240px;
      }
      #rating {
        font-size: 13px;
        font-family: Arial Unicode MS;
      }
      .table_row {
        height: 18px;
      }
      .attribute_name {
        font-weight: bold;
        text-align: right;
      }
      .table_icon {
        text-align: right;
      }
                                      
    </style><script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=false&libraries=places,visualization"></script><script>
        // using the AUTOCOMPLETE FEATURE of the Google Places API.
        // It allows the user to find all hospitals in a given place, within a given
        // country. It then displays markers for all the hospitals searched,
        // with on-click details for each hospital.

        var map, places, infoWindow;
        var markers = [];
        var autocomplete;
        var countryRestrict = { 'country': 'in' };
        var MARKER_PATH = 'https://maps.gstatic.com/intl/en_us/mapfiles/marker_green';
        var hostnameRegexp = new RegExp('^https?://.+?/');

        var countries = {
            'au': {
                center: new google.maps.LatLng(-25.3, 133.8),
                zoom: 4
            },
            'br': {
                center: new google.maps.LatLng(-14.2, -51.9),
                zoom: 3
            },
            'ca': {
                center: new google.maps.LatLng(62, -110.0),
                zoom: 3
            },
            'fr': {
                center: new google.maps.LatLng(46.2, 2.2),
                zoom: 5
            },
            'de': {
                center: new google.maps.LatLng(51.2, 10.4),
                zoom: 5
            },
            'mx': {
                center: new google.maps.LatLng(23.6, -102.5),
                zoom: 4
            },
            'nz': {
                center: new google.maps.LatLng(-40.9, 174.9),
                zoom: 5
            },
            'it': {
                center: new google.maps.LatLng(41.9, 12.6),
                zoom: 5
            },
            'in': {
                center: new google.maps.LatLng(28.878106, 78.741213),
                zoom: 5
            },
            'za': {
                center: new google.maps.LatLng(-30.6, 22.9),
                zoom: 5
            },
            'es': {
                center: new google.maps.LatLng(40.5, -3.7),
                zoom: 5
            },
            'pt': {
                center: new google.maps.LatLng(39.4, -8.2),
                zoom: 6
            },
            'us': {
                center: new google.maps.LatLng(37.1, -95.7),
                zoom: 3
            },
            'uk': {
                center: new google.maps.LatLng(54.8, -4.6),
                zoom: 5
            }
        };

        //Setting the initial parameters
        function initialize() {
            var myOptions = {
                zoom: countries['in'].zoom,
                center: countries['in'].center,
                mapTypeControl: false,
                panControl: false,
                zoomControl: false,
                streetViewControl: false
            };

            map = new google.maps.Map(document.getElementById('map-canvas'), myOptions);

            infoWindow = new google.maps.InfoWindow({
                content: document.getElementById('info-content')
            });

            // Create the autocomplete object and associate it with the UI input control.
            // Restrict the search to the default country, and to place type "cities".

            autocomplete = new google.maps.places.Autocomplete(
                /** @type {HTMLInputElement} */(document.getElementById('autocomplete')),
                {
                    types: ['(cities)'],
                    componentRestrictions: countryRestrict
                });
            places = new google.maps.places.PlacesService(map);

            google.maps.event.addListener(autocomplete, 'place_changed', onPlaceChanged);

            // Add a DOM event listener to react when the user selects a country.

            google.maps.event.addDomListener(document.getElementById('country'), 'change',
                setAutocompleteCountry);
        }

        // When the user selects a city, get the place details for the city and
        // zoom the map in on the city.

        function onPlaceChanged() {
            var place = autocomplete.getPlace();
            if (place.geometry) {
                map.panTo(place.geometry.location);
                map.setZoom(15);
                search();
            } else {
                document.getElementById('autocomplete').placeholder = 'Enter a city';
            }

        }

        // Search for hospitals in the selected city, within the viewport of the map.

        function search() {
            var search = {
                bounds: map.getBounds(),
                types: ['hospital']
            };

            places.nearbySearch(search, function (results, status) {
                if (status == google.maps.places.PlacesServiceStatus.OK) {
                    clearResults();
                    clearMarkers();

                    // Create a marker for each hospital found, and
                    // assign a letter of the alphabetic to each marker icon.

                    for (var i = 0; i < results.length; i++) {
                        var markerLetter = String.fromCharCode('A'.charCodeAt(0) + i);
                        var markerIcon = MARKER_PATH + markerLetter + '.png';
                        // Use marker animation to drop the icons incrementally on the map.
                        markers[i] = new google.maps.Marker({
                            position: results[i].geometry.location,
                            animation: google.maps.Animation.DROP,
                            icon: markerIcon
                        });

                        // If the user clicks a hospital marker, show the details of that hospital
                        // in an info window.
                        markers[i].placeResult = results[i];
                        google.maps.event.addListener(markers[i], 'click', showInfoWindow);
                        setTimeout(dropMarker(i), i * 100);
                        addResult(results[i], i);
                    }
                }
            });
        }

        function clearMarkers() {
            for (var i = 0; i < markers.length; i++) {
                if (markers[i]) {
                    markers[i].setMap(null);
                }
            }
            markers = [];
        }


        // Set the country restriction based on user input.
        // Also center and zoom the map on the given country.

        function setAutocompleteCountry() {
            var country = document.getElementById('country').value;
            if (country == 'all') {
                autocomplete.setComponentRestrictions([]);
                map.setCenter(new google.maps.LatLng(15, 0));
                map.setZoom(2);
            } else {
                autocomplete.setComponentRestrictions({ 'country': country });
                map.setCenter(countries[country].center);
                map.setZoom(countries[country].zoom);
            }
            clearResults();
            clearMarkers();
        }

        //Display the results on the map.

        function dropMarker(i) {
            return function () {
                markers[i].setMap(map);
            };
        }

        function addResult(result, i) {
            var results = document.getElementById('results');
            var markerLetter = String.fromCharCode('A'.charCodeAt(0) + i);
            var markerIcon = MARKER_PATH + markerLetter + '.png';

            var tr = document.createElement('tr');
            tr.style.backgroundColor = (i % 2 == 0 ? '#F0F0F0' : '#FFFFFF');
            tr.onclick = function () {
                google.maps.event.trigger(markers[i], 'click');
            };

            var iconTd = document.createElement('td');
            var nameTd = document.createElement('td');
            var icon = document.createElement('img');
            icon.src = markerIcon;
            icon.setAttribute('class', 'placeIcon');
            icon.setAttribute('className', 'placeIcon');
            var name = document.createTextNode(result.name);
            iconTd.appendChild(icon);
            nameTd.appendChild(name);
            tr.appendChild(iconTd);
            tr.appendChild(nameTd);
            results.appendChild(tr);
        }

        function clearResults() {
            var results = document.getElementById('results');
            while (results.childNodes[0]) {
                results.removeChild(results.childNodes[0]);
            }
        }

        // Get the place details for a hospital. Show the information in an info window,
        // anchored on the marker for the hospital that the user selected.

        function showInfoWindow() {
            var marker = this;
            places.getDetails({ placeId: marker.placeResult.place_id },
                function (place, status) {
                    if (status != google.maps.places.PlacesServiceStatus.OK) {
                        return;
                    }
                    infoWindow.open(map, marker);
                    buildIWContent(place);
                });
        }

        // Load the place information into the HTML elements used by the info window.

        function buildIWContent(place) {
            document.getElementById('icon').innerHTML = '<img class="hospitalIcon" ' +
                'src="' + place.icon + '"/>';
            document.getElementById('url').innerHTML = '<b><a href="' + place.url +
                '">' + place.name + '</a></b>';
            document.getElementById('address').textContent = place.vicinity;

            if (place.formatted_phone_number) {
                document.getElementById('phone-row').style.display = '';
                document.getElementById('phone').textContent =
                    place.formatted_phone_number;
            } else {
                document.getElementById('phone-row').style.display = 'none';
            }


            // The regexp isolates the first part of the URL (domain plus subdomain)
            // to give a short URL for displaying in the info window.

            if (place.website) {
                var fullUrl = place.website;
                var website = hostnameRegexp.exec(place.website);
                if (website == null) {
                    website = 'http://' + place.website + '/';
                    fullUrl = website;
                }
                document.getElementById('website-row').style.display = '';
                document.getElementById('website').textContent = website;
            } else {
                document.getElementById('website-row').style.display = 'none';
            }
        }

    </script></head>
    <body style="margin:0px; padding:0px;background:url('image/images%20(2).jpg') center no-repeat;background-size:cover" onload="initialize()">
   
       <div id="findhospital">
      Find hospitals in:
    </div>

    <div id="locationField">
      <input id="autocomplete" placeholder="Enter a city" type="text" />
    </div>
    <div id="controls">
      &nbsp;</div>

    <div id="listing">
      <table id="resultsTable">
        <tbody id="results"></tbody>
      </table>
    </div>
        
    <div id="info-content">
 <table>
        <tr id="url-row" class="table_row">
          <td id="icon" class="table_icon"></td>
          <td id="url"></td>
        </tr>
        <tr id="address-row" class="table_row">
          <td class="attribute_name"></td>
          <td id="address"></td>
        </tr>
        <tr id="phone-row" class="table_row">

          <td id="phone"></td>
        </tr>
        <tr id="website-row" class="table_row">
          <td class="attribute_name"></td>
          <td id="website"></td>
        </tr>
      </table>
            <div id="controls">
      <select id="country" name="D1">
        <option value="all">All</option>
        <option value="au">Australia</option>
        <option value="br">Brazil</option>
        <option value="ca">Canada</option>
        <option value="fr">France</option>
        <option value="de">Germany</option>
        <option value="mx">Mexico</option>
        <option value="nz">New Zealand</option>
        <option value="it">Italy</option>
        <option value="in" selected>India</option>
        <option value="za">South Africa</option>
        <option value="es">Spain</option>
        <option value="pt">Portugal</option>
        <option value="us">U.S.A.</option>
        <option value="uk">United Kingdom</option>
      </select></div>
        </div>

    <div id="map-canvas" draggable="auto" style="top: 30px"></div>

    </body>
</html>
