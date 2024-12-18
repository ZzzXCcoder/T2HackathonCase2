using Microsoft.AspNetCore.Mvc;
using T2HackathonCase2.Dtos;
using T2HackathonCase2.Entities;

namespace T2HackathonCase2.Service.UserService
{
    public interface IUserService
    {
        public Task<IActionResult> Register(TeleramUpdateResponce userRegisterDto);

        public Task<User> GetUser(long ChatId);
        public Task<IResult> SetAtributeAsync(long ChatId, double Latitude, double Longtute);
        public Task<IResult> SetAtributeAsync(long ChatId, string Company);
        public Task<IResult> SetAtributeAsync(long ChatId, int Duration);
        public Task<IResult> SetLocationForUser(long ChatId);
        public Task<LocationDto> FindUserLocation(long ChatId, int Currentlocation);

    }
}
