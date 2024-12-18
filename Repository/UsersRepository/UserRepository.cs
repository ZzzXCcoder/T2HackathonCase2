using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using T2HackathonCase2.Data;
using T2HackathonCase2.Dtos;
using T2HackathonCase2.Entities;
using T2HackathonCase2.Repository.UserRepository;
using T2HackathonCase2.Service.HerePlaceService;
using Telegram.Bot.Types;
using static T2HackathonCase2.Dtos.LocationDto;

namespace T2HackathonCase2.Repository.UsersRepository
{
    public class UserRepository : IUserRepository
    {
        WeekendWayDbContext _context { get; set; }
        IHerePlaceService _herePlaceService { get; set; }

        public UserRepository(WeekendWayDbContext context, IHerePlaceService herePlaceService)
        {
            _context = context;
            _herePlaceService = herePlaceService;
        }
        public async Task<IActionResult> Register(TeleramUpdateResponce userRegisterDto)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.ChatId == userRegisterDto.Message.Chat.Id))
                {
                    return new BadRequestObjectResult("User with this Id already exist");
                }
                var NewUser = new T2HackathonCase2.Entities.User
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
        public async Task<T2HackathonCase2.Entities.User> GetUser(long ChatId)
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
            public async Task<IResult> DeleteUserLocations(long ChatId)
            {
                var userLocations = await _context.UserWithLocations
                    .Where(uwl => uwl.UserId == ChatId)
                    .ToListAsync();
                if (userLocations.Any())
                {
                    _context.UserWithLocations.RemoveRange(userLocations);
                    await _context.SaveChangesAsync();
                    return Results.Ok();
                }
                return Results.Ok();
            }
        public async Task<IResult> ChangeUserCurrentLocation(long ChatId, int CurrentLocation)
        {
            var User = GetUser(ChatId);
            User.Result.Currentlocation = User.Result.Currentlocation + CurrentLocation;
            await _context.SaveChangesAsync();
            return Results.Ok();
        }

            
        public async Task<IResult> SetLocationForUser(long ChatId, string query, double latitude, double longitude, double radius, int limit)
        {
            var user = await GetUser(ChatId);
            if (user == null)
            {
                return Results.NotFound("User not found.");
            }

            user.Currentlocation = 0;
            // Получаем список мест из сервиса
            var suggestedPlaces = await _herePlaceService.GetSuggestedPlacesAsync(query, latitude, longitude, radius, limit);
            suggestedPlaces = await _herePlaceService.GetImageUrlsForPlaceAsync(suggestedPlaces, 1);

            if (suggestedPlaces == null)
            {
                return Results.NotFound("No suggested places found.");
             
            }


            foreach (var suggestedPlace in suggestedPlaces)
            {
                var existingLocation = await _context.Locations
                    .FirstOrDefaultAsync(l => l.Id == suggestedPlace.Id);
                if (existingLocation == null)
                {
                    var newLocation = new T2HackathonCase2.Entities.Location
                    {
                        Id = suggestedPlace.Id, // Уникальный идентификатор
                        Name = suggestedPlace.Title, // Название места
                        Latitude = suggestedPlace.Position.Lat, // Широта
                        Longitude = suggestedPlace.Position.Lng, // Долгота
                        Description = suggestedPlace.Address?.Label, // Полный адрес, если есть
                        Category = suggestedPlace.Categories?.FirstOrDefault()?.Name ?? "Unknown", // Первая категория или "Unknown"
                        ImageURL = suggestedPlace.ImageUrl ?? "" // Добавляем изображение
                    };
                    _context.Locations.AddAsync(newLocation);
                    var userWithLocation = new UserWithLocation
                    {
                        LocationId = suggestedPlace.Id,
                        UserId = user.Id
                    };
                    _context.UserWithLocations.AddAsync(userWithLocation);
                }
            }

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();
            return Results.Ok();
        }

        public async Task<LocationDto> FindUserLocation(long ChatId, int Currentlocation)
        {
            var user = await GetUser(ChatId);
            if (user == null)
            {
                return null;
            }

            var userLocation = await _context.UserWithLocations
                                  .Where(uwl => uwl.UserId == user.Id)
                                  .OrderBy(uwl => uwl.Id)
                                  .Skip(Currentlocation)  // Пропускаем предыдущие элементы
                                  .FirstOrDefaultAsync();  // Берём первый из оставшихся (или null, если не найдено)
            if (userLocation == null)
            {
                Console.WriteLine($"User with ID {user.Id} has no location at position {Currentlocation}");
                return null;
            }

            if (userLocation == null)
            {
                return null;
            }

            var LocationId = userLocation?.LocationId;

            var Location = _context.Locations.FirstOrDefaultAsync(l => l.Id == LocationId).Result;

            var locationDto = new LocationDto
            {
                Name = Location?.Name ?? "Unknown",
                Description = Location?.Description ?? "Unknown",
                Latitude = Location.Latitude,
                Longitude = Location.Longitude,
                Category = Location?.Category ?? "Unknown",
                ImageURL = Location?.ImageURL ?? "",
            };
            return locationDto;
        }
    }
}
