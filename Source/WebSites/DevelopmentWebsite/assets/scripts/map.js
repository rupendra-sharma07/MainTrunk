///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.assets.scripts.map.js
///Author          : 
///Creation Date   : 
///Description     : This file is used by Google Maps
///                  The file is provided by Google Maps for integration with the site    
///Audit Trail     : Date of Modification  Modified By         Description

    var map = null;
    var geocoder = null;
	var directions = null;
	var currentAddress = null;

    function mapInit() {
		if (GBrowserIsCompatible()) {
			directionsPanel = document.getElementById("yt_MapDirections");
			map = new GMap2(document.getElementById("yt_MapArea"));
			directions = new GDirections(map, directionsPanel);
			//map.setCenter(new GLatLng(49.2632,-122.954779), 13);
			geocoder = new GClientGeocoder();
			map.addControl(new GSmallMapControl());
			map.addControl(new GMapTypeControl ());        
			GEvent.addListener(directions, "error", handleErrors);
		}
    }

    function showAddress(str, cty, st, ctr) {
		var address = str + ' ' + cty + ' ' + st + ' ' + ctr;
		currentAddress = address;
		var addressText = '			<div class="yt-MapAddress"><h3>Address:</h3>' + 
				str + ',<br />' + 
				cty + ', ' + st + ',<br />' + 
				ctr +'</div>';

		if (geocoder) {
		geocoder.getLatLng(
						
			address,
			function(point) {
				if (!point) {
					geocoder.getLocations(address, function(result) { 
						if (result.Status.code == G_GEO_SUCCESS) {
							var p = result.Placemark[0].Point.coordinates;
							map.setCenter(new GLatLng(p[1], p[0]), 13);
							var marker = new GMarker(new GLatLng(p[1], p[0]));
							map.addOverlay(marker);
							marker.openInfoWindowHtml(addressText);
						} else { 
							var reason="Code "+result.Status.code;
							/*if (reasons[result.Status.code]) {
								reason = reasons[result.Status.code];
							}
							alert('Could not find "'+address+ '" ' + reason); */
							alert('Address not found');
						}
					//alert(address + " not found");
					} );
				} else {
					map.setCenter(point, 13);
					var marker = new GMarker(point);
					map.addOverlay(marker);
					marker.openInfoWindowHtml(addressText);
				}
			});
		}
    }
	
	
	function showDirections() {		
		directions.load('from:'+$('yt_FromAddress').value + ' to:'+currentAddress);
	}
	
	
    function handleErrors(){
	   alert("Please make sure you've entered a valid address.");
	   
	}
	
	
	