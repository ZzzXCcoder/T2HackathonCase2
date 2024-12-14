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

        public UserRepository(WeekendWayDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Register(TeleramUpdateResponce userRegisterDto)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.ChatId == userRegisterDto.Message.Chat.Id))
                {
                    return new BadRequestObjectResult("User with this Id already exist");
                }
                var NewUser = new User
                {
                    Id = (long)userRegisterDto.Message.From.Id,
                    ChatId = (long)userRegisterDto.Message.Chat.Id,
                    CreatedAt = DateTime.UtcNow,
                    UserName = userRegisterDto.Message.From.FirstName,
                    Company = ""
                };
                await _context.Users.AddAsync(NewUser);
                await _context.SaveChangesAsync();
                return new CreatedAtActionResult("GetUser", "User", new { id = NewUser.Id }, NewUser);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<User> GetUser(long ChatId)
        {
            try
            {
               var User = await _context.Users.FirstOrDefaultAsync(u => u.ChatId == ChatId);
               if (User != null)
               {
                    return User;
               }
               else
               {
                    return null;
               }
            }
            catch (Exception ex) 
            {
                throw new Exception("Произошла ошибка при получении пользователя", ex);
            }
          
            
        }
        public async Task<IResult> SetAtributeAsync(long ChatId, double Latitude, double Longtute)
        {
            var User = await GetUser(ChatId);
            if (User != null)
            {
                User.Latitude = Latitude;
                User.Longtute = Longtute;
                _context.SaveChangesAsync();
                return Results.Ok();
            }
            return Results.Ok();
            
        }

        public async Task<IResult> SetAtributeAsync(long ChatId, string Company)
        {
            var User = await GetUser(ChatId);
            if (User != null)
            {
                User.Company = Company;
                _context.SaveChangesAsync();
                return Results.Ok();
            }
            return Results.Ok();
        }

        public async Task<IResult> SetAtributeAsync(long ChatId, int Duration)
        {
            var User = await GetUser(ChatId);
            if (User != null)
            {
                User.Duration = Duration;
                _context.SaveChangesAsync();
                return Results.Ok();
            }
            return Results.Ok();
        }
    }
}
