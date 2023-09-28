using System.Net.Http;
using TrybeHotel.Dto;
using TrybeHotel.Repository;

namespace TrybeHotel.Services
{
    public class GeoService : IGeoService
    {
        private readonly HttpClient _client;
        public GeoService(HttpClient client)
        {
            _client = client;
        }

        public async Task<object> GetGeoStatus()
        {
            var data = new HttpRequestMessage(HttpMethod.Get, "https://nominatim.openstreetmap.org/status.php?format=json");
            data.Headers.Add("Accept", "application/json");
            data.Headers.Add("User-Agent", "aspnet-user-agent");
            var response = await _client.SendAsync(data);

            return await response.Content.ReadFromJsonAsync<object>();
        }

        public async Task<GeoDtoResponse> GetGeoLocation(GeoDto geoDto)
        {
            var data = new HttpRequestMessage(HttpMethod.Get, $"https://nominatim.openstreetmap.org/search?street={geoDto.Address}&city={geoDto.City}&country=Brazil&state={geoDto.State}&format=json&limit=1");
            data.Headers.Add("Accept", "application/json");
            data.Headers.Add("User-Agent", "aspnet-user-agent");
            var response = await _client.SendAsync(data);

            var geoResponse = await response.Content.ReadFromJsonAsync<List<GeoDtoResponse>>();
            
            return new GeoDtoResponse {
                lat = geoResponse[0].lat,
                lon = geoResponse[0].lon
            };
        }

        public async Task<List<GeoDtoHotelResponse>> GetHotelsByGeo(GeoDto geoDto, IHotelRepository repository)
        {
            var hotels = new List<GeoDtoHotelResponse>();
            
            foreach(var hotel in repository.GetHotels())
            {
                var userPosition = await GetGeoLocation(geoDto);
                
                var hotelGeoDto = new GeoDto {
                    Address = hotel.Address,
                    City = hotel.CityName,
                    State = hotel.State,
                };
                
                var hotelPosition = await GetGeoLocation(hotelGeoDto);
                var distance = CalculateDistance(userPosition.lat, userPosition.lon, hotelPosition.lat, hotelPosition.lon);
                
                hotels.Add(new GeoDtoHotelResponse {
                    Address = hotel.Address,
                    CityName = hotel.CityName,
                    HotelId = hotel.HotelId,
                    Name = hotel.Name,
                    State = hotel.State,
                    Distance = distance
                });
            }

            return hotels.OrderBy(h => h.Distance).ToList();
        }

       

        public int CalculateDistance (string latitudeOrigin, string longitudeOrigin, string latitudeDestiny, string longitudeDestiny) {
            double latOrigin = double.Parse(latitudeOrigin.Replace('.',','));
            double lonOrigin = double.Parse(longitudeOrigin.Replace('.',','));
            double latDestiny = double.Parse(latitudeDestiny.Replace('.',','));
            double lonDestiny = double.Parse(longitudeDestiny.Replace('.',','));
            double R = 6371;
            double dLat = radiano(latDestiny - latOrigin);
            double dLon = radiano(lonDestiny - lonOrigin);
            double a = Math.Sin(dLat/2) * Math.Sin(dLat/2) + Math.Cos(radiano(latOrigin)) * Math.Cos(radiano(latDestiny)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            double distance = R * c;
            return int.Parse(Math.Round(distance,0).ToString());
        }

        public double radiano(double degree) {
            return degree * Math.PI / 180;
        }

    }
}