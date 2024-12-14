using Microsoft.AspNetCore.Mvc;
using T2HackathonCase2.Dtos;
using T2HackathonCase2.Repository.UserRepository;
using T2HackathonCase2.Entities;

namespace T2HackathonCase2.Service.UserService
{
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


    }

}
