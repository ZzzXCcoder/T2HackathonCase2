using Newtonsoft.Json.Linq;
using T2HackathonCase2.Dtos;

namespace T2HackathonCase2.Service.HerePlaceService
{
    public class HerePlaceService : IHerePlaceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "drorUjjbNBuWmvh8DHXpNazf4CBQCn4owVsEzBGz0R8";
        private readonly string _accessKey = "A6M_vIuvsaWrasYYwEdZkkJU27YF1msn6ooeP0CKU3s";
        public HerePlaceService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<List<HerePlaceDto>> GetSuggestedPlacesAsync(string query, double latitude, double longitude, double radius,  int limit)
        {
            string RadiusString = Convert.ToString(radius).Replace(',', '.');
            string LatitudeString = Convert.ToString(latitude).Replace(',', '.');
            string Longitude = Convert.ToString(longitude).Replace(',', '.');

            var encodedQuery = Uri.EscapeDataString(query);
            var url = $"https://discover.search.hereapi.com/v1/discover?at={LatitudeString},{Longitude}&q={encodedQuery}&limit={limit}&lang=ru&apiKey={_apiKey}";

            var response = await _httpClient.GetStringAsync(url);

            var json = JObject.Parse(response);
            var places = json["items"].ToObject<List<HerePlaceDto>>();
            return places;

        }
        public async Task<List<HerePlaceDto>> GetImageUrlsForPlaceAsync(List<HerePlaceDto> herePlaceDtos, int limit = 1)
        {
            foreach (var place in herePlaceDtos)
            {
                var placeCategory = place.Categories.FirstOrDefault();
                var searchQuery = placeCategory.Name.Split(' ')[0]; // Используем категорию или название для поиска
                var url = $"https://api.unsplash.com/photos/random?query={searchQuery}&client_id={_accessKey}&count={limit}&order_by=relevant&content_filter=high&orientation=landscape";

                try
                {
                    var response = await _httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var jArray = JArray.Parse(json);

                        var imageUrl = jArray
                            .Where(item => item["urls"]?["regular"] != null)
                            .Select(item => item["urls"]["regular"].ToString())
                            .FirstOrDefault();

                        place.ImageUrl = imageUrl; // Добавляем ссылку на изображение
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка API Unsplash: {(int)response.StatusCode} {response.ReasonPhrase}");
                        place.ImageUrl = null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при запросе к Unsplash: {ex.Message}");
                    place.ImageUrl = null; // Если ошибка, картинка отсутствует
                }
            }

            return herePlaceDtos; // Возвращаем обновленный список с изображениями
        }
    }


}
