using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace T2HackathonCase2.Dtos
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterDto : ControllerBase
    {
        public long TelegramId { get; set;}

        public string? UserName { get; set; }

        public string? Description { get; set; }
    }
}
