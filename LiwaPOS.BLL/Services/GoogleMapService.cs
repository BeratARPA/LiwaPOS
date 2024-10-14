using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Services
{
    public class GoogleMapService : IGoogleMapService
    {
        public async Task<string> GetDirectionAsync(ShowGoogleMapsDirectionDTO showGoogleMapsDirectionDto)
        {
            string DirectionsApiUrl = $"https://maps.googleapis.com/maps/api/directions/json?origin={showGoogleMapsDirectionDto.OriginLat},{showGoogleMapsDirectionDto.OriginLat}&destination={showGoogleMapsDirectionDto.DestinationLat},{showGoogleMapsDirectionDto.DestinationLong}&units=metric&language=tr&key={showGoogleMapsDirectionDto.APIKey}";

            var client = new HttpClient();
            var response = await client.GetStringAsync(DirectionsApiUrl);
            var direction = JsonHelper.Deserialize<Direction>(response);

            string htmlContent = GenerateHtmlContent(showGoogleMapsDirectionDto.OriginLat, showGoogleMapsDirectionDto.OriginLong, showGoogleMapsDirectionDto.DestinationLat, showGoogleMapsDirectionDto.DestinationLong, showGoogleMapsDirectionDto.APIKey);

            return htmlContent;
        }

        private string GenerateHtmlContent(string lat1, string lng1, string lat2, string lng2, string apiKey)
        {
            return $@"
<!DOCTYPE html>
<html>
  <head>
    <title>Pentegrasyon</title>
    <style>
      * {{
        margin: 0;
        padding: 0;
        box-sizing: border-box;
      }}
      #map {{
        height: 100vh;
      }}
    </style>
  </head>
  <body>
    <div id=""map""></div>
    <script
      src=""https://maps.googleapis.com/maps/api/js?key={apiKey}&callback=initMap&v=beta&language=tr&loading=async&region=TR""
      defer
    ></script>
    <script>
      let map;
      let restaurantLocationLat = {lat1};
      let restaurantLocationLng = {lng1};
      let customerLocationLat = {lat2};
      let customerLocationLng = {lng2};

      function initMap() {{
        const directionsService = new google.maps.DirectionsService();
        const directionsRenderer = new google.maps.DirectionsRenderer({{
          suppressMarkers: true,
        }});
        map = new google.maps.Map(document.getElementById(""map""), {{
          zoom: 12,
        }});

        directionsRenderer.setMap(map);

        const originMarker = new google.maps.Marker({{
          position: {{ lat: restaurantLocationLat, lng: restaurantLocationLng }},
          map: map,
          title: ""Başlangıç Noktası"",
          icon: {{
            url: ""https://cdn-icons-png.freepik.com/512/12522/12522999.png"",
            scaledSize: new google.maps.Size(50, 50),
          }},
        }});

        const destinationMarker = new google.maps.Marker({{
          position: {{ lat: customerLocationLat, lng: customerLocationLng }},
          map: map,
          title: ""Bitiş Noktası"",
          icon: {{
            url: ""https://cdn-icons-png.flaticon.com/512/1189/1189458.png"",
            scaledSize: new google.maps.Size(50, 50),
          }},
        }});

        calculateAndDisplayRoute(directionsService, directionsRenderer);
      }}

      function calculateAndDisplayRoute(directionsService, directionsRenderer) {{
        directionsService
          .route({{
            origin: {{ lat: restaurantLocationLat, lng: restaurantLocationLng }},
            destination: {{ lat: customerLocationLat, lng: customerLocationLng }},
            travelMode: google.maps.TravelMode.DRIVING,
          }})
          .then((response) => {{
            directionsRenderer.setDirections(response);
          }})
          .catch((e) =>
            window.alert(""Yol tarifi isteği başarısız oldu: "" + status)
          );
      }}

      window.initMap = initMap;
    </script>
  </body>
</html>
";
        }
    }
}
