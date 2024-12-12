using Microsoft.AspNetCore.Mvc;
using T2HackathonCase2.Dtos;

namespace T2HackathonCase2.Repository.UserRepository
{
    public interface IUserRepository
    {
        public IActionResult Register(UserRegisterDto userRegisterDto);
    }
}
