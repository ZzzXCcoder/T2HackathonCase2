using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using T2HackathonCase2.Dtos;
using T2HackathonCase2.Entities;

namespace T2HackathonCase2.Repository.UserRepository
{
    public interface IUserRepository
    {
        public Task<IActionResult> Register(TeleramUpdateResponce userRegisterDto);
        public  Task<User> GetUser(long ChatId);
        public  Task<IResult> SetAtributeAsync(long ChatId, double Latitude, double Longtute);

        public  Task<IResult> SetAtributeAsync(long ChatId, string Company);

        public Task<IResult> SetAtributeAsync(long ChatId, int Duration);

        public Task<IResult> SetLocationForUser(long ChatId, string query, double latitude, double longitude, double radius, int limit);

        public Task<LocationDto> FindUserLocation(long ChatId, int Currentlocation);


        public Task<IResult> DeleteUserLocations(long ChatId);

        public Task<IResult> ChangeUserCurrentLocation(long ChatId, int CurrentLocation);

    }
}
