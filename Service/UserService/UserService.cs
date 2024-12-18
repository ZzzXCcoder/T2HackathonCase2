using Microsoft.AspNetCore.Mvc;
using T2HackathonCase2.Dtos;
using T2HackathonCase2.Repository.UserRepository;
using T2HackathonCase2.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;

namespace T2HackathonCase2.Service.UserService
{
    enum Places_by_preference
    {
        Parks,
        Museums,
        Zoos,
        Cafe,
        Restaurant,
        Cinema,
        Promenade,
        Bar,
        Club,
        CoffeeHouse,
        Fitnesscenters,
        Libraries,
        Attractions
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Register(TeleramUpdateResponce userRegisterDto)
        {
            return await _userRepository.Register(userRegisterDto);
        }

        public async Task<User> GetUser(long ChatId)
        {
            return await _userRepository.GetUser(ChatId);
        }
        public async Task<IResult> SetAtributeAsync(long ChatId, double Latitude, double Longtute)
        {
            return await _userRepository.SetAtributeAsync(ChatId, Latitude, Longtute);
        }

        public async Task<IResult> SetAtributeAsync(long ChatId, string Company)
        {
            return await _userRepository.SetAtributeAsync(ChatId, Company);
        }
        public async Task<IResult> SetAtributeAsync(long ChatId, int Duration)
        {
            return await _userRepository.SetAtributeAsync(ChatId, Duration);
        }

        public async Task<IResult> SetLocationForUser(long ChatId)
        {
            _userRepository.DeleteUserLocations(ChatId);
            var User = await _userRepository.GetUser(ChatId);
            if (User.Company == "for_loving_couple")
            {
                await _userRepository.SetLocationForUser(ChatId, Places_by_preference.Cinema.ToString(), (double)User.Latitude, (double) User.Longtute, (double)User.Duration*3000, (int)User.Duration);
                await _userRepository.SetLocationForUser(ChatId, Places_by_preference.Restaurant.ToString(), (double)User.Latitude, (double)User.Longtute, (double)User.Duration * 3000, (int)User.Duration);
                await _userRepository.SetLocationForUser(ChatId, Places_by_preference.Museums.ToString(), (double)User.Latitude, (double)User.Longtute, (double)User.Duration * 3000, (int)User.Duration);
                return Results.Ok();
            }
            if (User.Company == "for_family")
            {
                await _userRepository.SetLocationForUser(ChatId, Places_by_preference.Parks.ToString(), (double)User.Latitude, (double)User.Longtute, (double)User.Duration * 3000, (int)User.Duration);
                await _userRepository.SetLocationForUser(ChatId, Places_by_preference.Attractions.ToString(), (double)User.Latitude, (double)User.Longtute, (double)User.Duration * 3000, (int)User.Duration);
                await _userRepository.SetLocationForUser(ChatId, Places_by_preference.Cafe.ToString(), (double)User.Latitude, (double)User.Longtute, (double)User.Duration * 3000, (int)User.Duration);
                return Results.Ok();
            }

            if (User.Company == "for_group_of_friends")
            {
                await _userRepository.SetLocationForUser(ChatId, Places_by_preference.Bar.ToString(), (double)User.Latitude, (double)User.Longtute, (double)User.Duration * 3000, (int)User.Duration);
                await _userRepository.SetLocationForUser(ChatId, Places_by_preference.Club.ToString(), (double)User.Latitude, (double)User.Longtute, (double)User.Duration * 3000, (int)User.Duration);
                await _userRepository.SetLocationForUser(ChatId, Places_by_preference.Promenade.ToString(), (double)User.Latitude, (double)User.Longtute, (double)User.Duration * 3000, (int)User.Duration);
                return Results.Ok();
            }

            if (User.Company == "for_solo")
            {
                await _userRepository.SetLocationForUser(ChatId, Places_by_preference.Fitnesscenters.ToString(), (double)User.Latitude, (double)User.Longtute, (double)User.Duration * 3000, (int)User.Duration);
                await _userRepository.SetLocationForUser(ChatId, Places_by_preference.CoffeeHouse.ToString(), (double)User.Latitude, (double)User.Longtute, (double)User.Duration * 3000, (int)User.Duration);
                await _userRepository.SetLocationForUser(ChatId, Places_by_preference.Libraries.ToString(), (double)User.Latitude, (double)User.Longtute, (double)User.Duration * 3000, (int)User.Duration);
                await _userRepository.SetLocationForUser(ChatId, Places_by_preference.Attractions.ToString(), (double)User.Latitude, (double)User.Longtute, (double)User.Duration * 3000, (int)User.Duration);
                return Results.Ok();
            }
            return Results.NotFound();

        }
        public async Task<LocationDto> FindUserLocation(long ChatId, int Currentlocation = 0)
        {
            var User = await _userRepository.GetUser(ChatId);
            await _userRepository.ChangeUserCurrentLocation(ChatId, Currentlocation);
            return await _userRepository.FindUserLocation(ChatId,User.Currentlocation);
        }
    }

}
