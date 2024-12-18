using T2HackathonCase2.Dtos;

namespace T2HackathonCase2.Service.HerePlaceService
{
    public interface IHerePlaceService
    {
        public Task<List<HerePlaceDto>> GetSuggestedPlacesAsync(string query, double latitude, double longitude, double radius, int limit);

        public Task<List<HerePlaceDto>> GetImageUrlsForPlaceAsync(List<HerePlaceDto> herePlaceDtos, int limit);
    }
}
