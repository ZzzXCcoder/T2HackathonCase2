using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2HackathonCase2.Data;
using T2HackathonCase2.Dtos;
using T2HackathonCase2.Entities;
using T2HackathonCase2.Repository.UserRepository;

namespace T2HackathonCase2.Repository.UsersRepository
{
    public class UserRepository : IUserRepository
    {
        WeekendWayDbContext _context { get; set; }

        UserRepository(WeekendWayDbContext context)
        {
            _context = context;
        }
        public async IActionResult Register(UserRegisterDto userRegisterDto)
        {
            if (await _context.Users.AnyAsync(u => u.TelegramId == userRegisterDto.TelegramId))
            {
                return new BadRequestObjectResult("User with this Id already exist");
            }
            var NewUser = new User
            {
                Id = new Guid(),
                TelegramId = userRegisterDto.TelegramId,
                CreatedAt = DateTime.UtcNow,
                Description = userRegisterDto.Description

            };
        }
    }
}
